using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class filteration : MonoBehaviour {

    string filePath;
    string videoFilePath;

    // Use this for initialization
    void Start () {

        fileNames();
        filterVideos();

    }
	
	// Update is called once per frame

	void Update () {

        
   

	}

    void fileNames()
    {


        filePath = Application.dataPath + "/RequiredData/" + "tagFiltration.csv";
        Debug.Log("test file path" + filePath);


    }


    void filterVideos()
    {

        string[] filterationList = File.ReadAllLines(filePath); // 0 - TaskName, 1 - ParticipantName, 2 - Innovative, 3 - tagName

        for (int i = 0; i < filterationList.Length; i++)
        {
       
            Debug.Log("filterationListItems " + filterationList[i].Split(';')[1]);
        
        }



    }
}
