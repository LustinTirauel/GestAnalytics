using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

public class databaseActivity : MonoBehaviour {

    int taskIndex = 0;
    int participantIndex = 0;
    string filePath;
    string filePath2;
    string filePath3;
    string filePath4;
    public GameObject listHolder;


    public List<Participants> participantList = new List<Participants>();
    public List<Tasks> taskList = new List<Tasks>();
    public List<CheckedTags> checkedTagList = new List<CheckedTags>();

    // Use this for initialization

    void Awake()
    {
        fileNames();

        if (!File.Exists(filePath))
            File.Create(filePath2);
            File.Create(filePath3);
            File.Create(filePath4);
    }
    void Start () {

        

        makeListFromFile();
        //makeListFromFile2();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void makeListFromFile2()
    {
        fileNames();

        string[] participantNames;
        string[] taskNames;
        string[] checkedTags;
        string taskName = "";
        string participantName = "";
        string taskNameControl = "";
        string participantNameControl = "";

        participantNames = Directory.GetDirectories(Application.streamingAssetsPath + "/AnalysisVideos");
        taskNames = File.ReadAllLines(filePath);
        checkedTags = File.ReadAllLines(filePath2);

        for (int i = 0; i < participantNames.Length; i++)
        {
            participantNames[i] = participantNames[i].Substring(Application.streamingAssetsPath.Length + 16);
            participantList.Add(new Participants(participantNames[i], new List<Tasks>()));
        }


        for (int i = 0; i < taskNames.Length; i++)
        {
            taskList.Add(new Tasks(taskNames[i].Split(';')[1], new List<CheckedTags>()));
        }
   
        for (int z = 0; z < taskNames.Length; z++) //get task names from the file at filePath and add it to the every instance of participantList
           {
               for (int i = 0; i < participantNames.Length; i++) { 
               participantList[i].tasks.Add(new Tasks(taskNames[z].Split(';')[1], new List<CheckedTags>()));
           }
           }

           /// 
           for (int i = 0; i < checkedTags.Length; i++) //get task names from the file at filePath
           {



               taskNameControl = taskName; //define a new variable called taskNameControl to understand if the taskName has changed in the document
               participantNameControl = participantName; //define a new variable called participantNameControl to understand if the taskName has changed in the document

               taskName = checkedTags[i].Split(';')[0]; //define the task name by getting the filtrationTags as a source
               participantName = checkedTags[i].Split(';')[1];  //define the participant name by getting the filtrationTags as a source

               if (checkedTags[i].Split(';')[2] == "innovative") //if the video is the innovative section, than add innovative suffix to end.
                   participantName = checkedTags[i].Split(';')[1] + "_innovative";

               if (taskList.Exists(x => x.task == taskName)) //in every loop define the taskindex to add checkedtags info!
                   taskIndex = taskList.FindIndex(x => x.task == taskName);

               if (participantList.Exists(x => x.participant == participantName)) //in every loop define the participantIndex to add checkedtags info!
                   participantIndex = participantList.FindIndex(x => x.participant == participantName);


               participantList[participantIndex].tasks[taskIndex].checkedTags.Add(new CheckedTags(checkedTags[i].Split(';')[3]));  //add the tag sourced from the filtrationTag file.

               if (taskName != taskNameControl && taskList.Exists(x => x.task == taskNameControl))  //when passing to a new task, clear the list to make a fresh start
               {
                   checkedTagList.Clear();
               }

               if ((participantName != participantNameControl && participantList.Exists(x => x.participant == participantNameControl))) //when passing to a new participant, clear the list to make a fresh start

               {
                   checkedTagList.Clear();
               }

           }



           writeListToFile();


    }

    public void makeListFromFile()
    {

        string[] participantNames;
        string[] taskNames;
        string taskName = "";
        string participantName = "";

        participantNames = Directory.GetDirectories(Application.streamingAssetsPath + "/AnalysisVideos");
        taskNames = File.ReadAllLines(filePath);
            


        string[] taskSnippets;
        string[] check;
        string checkedTagName;
        string[] tagsCheckedList = File.ReadAllLines(filePath3);

        for (int i = 0; i < participantNames.Length; i++)
        {
            participantNames[i] = participantNames[i].Substring(Application.streamingAssetsPath.Length + 16);
            participantList.Add(new Participants(participantNames[i], new List<Tasks>()));
            for(int j = 0; j < taskNames.Length; j++)
            {
                participantList[i].tasks.Add(new Tasks(taskNames[j].Split(';')[1], new List<CheckedTags>()));
            }
        }

        for (int i = 0; i < tagsCheckedList.Length; i++)
        {
            participantName = tagsCheckedList[i].Split(';')[0];
            taskSnippets = tagsCheckedList[i].Split(';')[1].Split(',');

            for (int j = 0; j < taskSnippets.Length; j++)
            {

                taskName = taskSnippets[j].Split('|')[0];
                check = taskSnippets[j].Split('|')[1].Split('.');

                if (!participantList[i].tasks.Exists (x => x.task == taskName))
                participantList[i].tasks.Add(new Tasks(taskName, new List<CheckedTags>()));

                for (int z = 0; z < check.Length; z++)
                {
                    checkedTagName = check[z];
                    if (!participantList[i].tasks[j].checkedTags.Exists(x => x.tag == checkedTagName))
                    participantList[i].tasks[j].checkedTags.Add(new CheckedTags(checkedTagName));
                }

                Array.Clear(check, 0, check.Length);
                Array.Resize<string>(ref check, 0);
            }


        }

        writeListToFile();



        /*  Debug.Log("participantName " + participantName);
          taskName = File.ReadAllLines(filePath3)[0].Split(';')[1].Split('|')[0];
          Debug.Log("taskName " + taskName);

          for (int i = 0; i < File.ReadAllLines(filePath3)[0].Split(';')[1].Split('|')[1].Split('.').Length-1; i++)
          {
              Debug.Log("participantName " + participantName + " taskName " + taskName + " tagName " + File.ReadAllLines(filePath3)[0].Split(';')[1].Split('|')[1].Split('.')[i]);

          }*/



    }

    public void writeListToFile()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < participantList.Count; i++)
        {
            sb.Append(participantList[i].participant + ";");
            for (int j = 0; j < participantList[i].tasks.Count; j++)
            {
                if (j == 0)
                    sb.Append(participantList[i].tasks[j].task + "|");
                else
                {
                    sb.Append("," + participantList[i].tasks[j].task + "|");
                }

                for (int z = 0; z < participantList[i].tasks[j].checkedTags.Count; z++)
                {
                    if (z == participantList[i].tasks[j].checkedTags.Count-1)
                        sb.Append(participantList[i].tasks[j].checkedTags[z].tag);

                    else
                    {
                        sb.Append(participantList[i].tasks[j].checkedTags[z].tag + ".");
                    }

                }
            }
            sb.AppendLine();
        }


        File.WriteAllText(filePath3, sb.ToString());
    }

    public void exportRegulatedExcel()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < participantList.Count; i++)
        {
            for (int j = 0; j < participantList[i].tasks.Count; j++)
            {
                for (int z = 0; z < participantList[i].tasks[j].checkedTags.Count; z++)
                {
                    sb.Append(participantList[i].tasks[j].task + ";");
                    sb.Append(participantList[i].participant.Substring(0, 3) +";");
                    if (participantList[i].participant.Contains("innovative"))
                            sb.Append("innovative;");

                    if (!participantList[i].participant.Contains("innovative"))
                            sb.Append("intuitive;");
                    sb.Append(participantList[i].tasks[j].checkedTags[z].tag);
                    sb.AppendLine();
                    
                }
            }
            

        }

        File.WriteAllText(filePath4, sb.ToString());
    }

    void fileNames()
    {
        filePath = Application.dataPath + "/RequiredData/taskData.csv";
        filePath2 = Application.dataPath + "/RequiredData/tagFiltration.csv";
        filePath3 = Application.dataPath + "/RequiredData/tagsChecked.csv";
        filePath4 = Application.dataPath + "/RequiredData/tagsRegulated.csv";
    }
}
