using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;


public class addTagFiltration : MonoBehaviour {

    public GameObject mainCamera;
    public List<fCheckedTag> fCheckedTagList;
    public List<fCheckedParticipant> fCheckedParticipantList;
    // Use this for initialization
    void Start () {
        fCheckedTagList = mainCamera.GetComponent<duplicateCameraPlane>().fCheckedTagList;
        fCheckedParticipantList = mainCamera.GetComponent<duplicateCameraPlane>().fCheckedParticipantList;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addFilteredTag()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            fCheckedTagList.Add(new fCheckedTag(gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text));
        }

        if (!gameObject.GetComponent<Toggle>().isOn)
        {
            fCheckedTagList.RemoveAll(x => x.name == gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text) ;
        }
    }


    public void addFilteredParticipant()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            fCheckedParticipantList.Add(new fCheckedParticipant(gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text));
        }

        if (!gameObject.GetComponent<Toggle>().isOn)
        {
            fCheckedParticipantList.RemoveAll(x => x.participantName == gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
        }
    }
}
