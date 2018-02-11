using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class lists : MonoBehaviour
{

    public List<TagList> tags = new List<TagList>();
    public List<Tags> tagPool = new List<Tags>();
    public List<Check> checkList = new List<Check>();
    public bool sthDeleted;
	public int order;
	public string deletedTagName;
	public InputField taskNameInput;
	public GameObject createTags;


    string[] tagNames;

	string filePath;
    string filePath2;


	bool backup;

	int taskNameEnd;



    void Start() {
		fileNames ();
        LoadTags();
    }



    void fileNames () {


    filePath = Application.dataPath + "/RequiredData/" + "tags.csv";
    filePath2 = Application.dataPath + "/SavedData/BackUp" + "tags.csv";


    }



    public void SaveTags()
    {


		StringBuilder tagSb = new StringBuilder();


        foreach (Tags tagName in tagPool)
        {
            tagSb.AppendLine(tagName.name);
        }


        File.WriteAllText(filePath, tagSb.ToString());

		if (backup)
			File.WriteAllText(filePath2, tagSb.ToString());
    }

    public void LoadTags()
    {

        if (!File.Exists(filePath))
        SaveTags();

        tagNames = new string[File.ReadAllLines(Application.dataPath + "/RequiredData/" + "tags.csv").Length];
        //Debug.Log(tagNames.Length);


        for (int i = 0; i < tagNames.Length; i++)
        {
            string[] namesSplit = File.ReadAllLines(Application.dataPath + "/RequiredData/" + "tags.csv");
            if (namesSplit[i].Length > 0)
            tagPool.Add(new Tags(namesSplit[i]));
        }

    }

	public void BackUp()
	{

		filePath = Application.dataPath + "/SavedData/BackUp/" + System.DateTime.Now.ToString("yyyyMMddHHmm") + "_tags.csv";


		backup = true;
		SaveTags ();
		backup = false;

		/*File.WriteAllText(filePath4, sb.ToString());
		File.WriteAllText(filePath5, tagSb.ToString());
		File.WriteAllText(filePath6, checkSb.ToString());*/
		

	}

	





}


