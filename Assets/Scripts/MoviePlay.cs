using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Text;
using System.IO;

public class MoviePlay : MonoBehaviour {
	public int taskNumber; //this will be increased when we pass a question.

    //videoNames, taskText, exportedInfo will read here.
    public string[] videoNames = new string[5];
    public string[] taskNames = new string[5];
	public string participantName;
	public Text taskText;
    public Text adminText;
	//this will be manually entered by the experimentor
	int participantNumber = 1;
    public getButtonName taskList;
    public bool randomizer;
    bool secondaryRandomizer;

    //these are defined for playing the movie
    Renderer r; 
	MovieTexture movie;
    


    void Start() {
        r = GetComponent<Renderer>();
        //movie = Resources.Load("QuestionVideos/" + videoNames[taskNumber]) as MovieTexture;
        participantName = "P" + participantNumber;
        taskNames = new string[File.ReadAllLines(Application.dataPath + "/taskData.csv").Length - 1];
        videoNames = new string[File.ReadAllLines(Application.dataPath + "/taskData.csv").Length - 1];




        for (int i = 0; i < taskNames.Length; i++) {
            String[] namesSplit = File.ReadAllLines(Application.dataPath + "/taskData.csv")[i + 1].Split(';');
            videoNames[i] = namesSplit[1];
            taskNames[i] = namesSplit[0];
            //Debug.Log ("videoName " + videoNames [i-1]);
            //Debug.Log ("taskName " + taskNames [i-1]);
        }

        if (randomizer) {
        for (int i = taskNames.Length - 1; i > 0; i--)
        {
            int y = UnityEngine.Random.Range(0, i);
            var tmp = taskNames[i];
            var tmpV = videoNames[i];
            videoNames[i] = videoNames[y];
            taskNames[i] = taskNames[y];
            taskNames[y] = tmp;
            videoNames[y] = tmpV;
        }
    }

        taskList.createTaskList();
        videoName();



	}

    public void randomizeActive()
    {
        if (!randomizer && !secondaryRandomizer) {
            randomizer = true;
            secondaryRandomizer = true;

        }

        if (randomizer && !secondaryRandomizer)
        {
            randomizer = false;

        }

        if (randomizer && secondaryRandomizer)
        {
            secondaryRandomizer = false;

        }
    }


    public void videoName () {

		Resources.UnloadUnusedAssets ();
        if (taskNumber < videoNames.Length) { 
        movie = Resources.Load("QuestionVideos/" + videoNames[taskNumber]) as MovieTexture; //load the movie with the matching name in csv file
		taskText.text = (taskNumber+1).ToString() + "." + " " + taskNames [taskNumber];
		//participantName = "P" + participantNumber;
		movie.Stop();
        Debug.Log("taskNumber " + taskNumber + " videoNames.Length " + videoNames.Length);
        taskNumber++;
        }



    }

    public void reset()
    {
        taskNumber = 0;
    }

    public void taskNumberDefine()
    {
        string adminNumber = adminText.text;
        int.TryParse(adminNumber, out taskNumber);
        taskNumber--; //this is since new function will increase it +1
        Debug.Log("taskNumber " + taskNumber);
    }


    public void exit()
    {
        Application.Quit();
    }

    void Update () {
	
		
			/*Renderer r = GetComponent<Renderer>();
			MovieTexture movie = Resources.Load(movieNames[i]) as MovieTexture;
			r.material.mainTexture = movie;*/
			r.material.mainTexture = movie;
			movie.loop = true;
			//Debug.Log (movie.name);
			movie.Play();
		}
	}
