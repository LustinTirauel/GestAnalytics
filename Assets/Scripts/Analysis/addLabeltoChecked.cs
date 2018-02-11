using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class addLabeltoChecked : MonoBehaviour {

    public List<Participants> participantList = new List<Participants>();
    public List<Tasks> taskList = new List<Tasks>();
    public List<CheckedTags> checkedTagList = new List<CheckedTags>();
    public GameObject mainCamera;
    public static string[] videoNameElements;
    public static string taskName;
    public static string participantName;
    public Transform toggle;
    public GameObject plane;
    public InputField addTagName;
    int participantIndex;
    int taskIndex;
    string tagName;

    // Use this for initialization
    void Start () {

        videoNameElements = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name.Split(' '); // 0-participant name 1-taskName
        taskName = videoNameElements[1];
        participantName = videoNameElements[0];

        participantList = mainCamera.GetComponent<databaseActivity>().participantList;
        taskList = mainCamera.GetComponent<databaseActivity>().taskList;
        checkedTagList = mainCamera.GetComponent<databaseActivity>().checkedTagList;
        toggle = gameObject.transform.parent;

        participantIndex = participantList.FindIndex(x => x.participant == participantName);
        taskIndex = participantList[participantIndex].tasks.FindIndex(x => x.task == taskName);

        tagName = gameObject.GetComponent<Text>().text;

        EventManager.changeName += updateNames;
        EventManager.changeName += highLightCheckedTag;

        highLightCheckedTag();
    }

    void OnDestroy()
    {
        EventManager.changeName -= updateNames;
        EventManager.changeName -= highLightCheckedTag;
    }

    public void addLabel()
    {
        if (!participantList[participantIndex].tasks[taskIndex].checkedTags.Exists(x => x.tag == tagName))
        {
            participantList[participantIndex].tasks[taskIndex].checkedTags.Add(new CheckedTags(tagName));
            mainCamera.GetComponent<databaseActivity>().writeListToFile();
        }

        if ( !toggle.GetComponent<Toggle>().isOn && participantList[participantIndex].tasks[taskIndex].checkedTags.Exists(x => x.tag == tagName))
        {
            int index = 0;
            index = participantList[participantIndex].tasks[taskIndex].checkedTags.FindIndex(x => x.tag == tagName);
            participantList[participantIndex].tasks[taskIndex].checkedTags.RemoveAt(index);
            mainCamera.GetComponent<databaseActivity>().writeListToFile();
        }
    }

    // Update is called once per frame

    public void highLightCheckedTag()
    {
        if (participantList[participantIndex].tasks[taskIndex].checkedTags.Count > 0)
        {

            taskIndex = (participantList[participantIndex].tasks.FindIndex(x => x.task == taskName));
            //Debug.Log("pIndex " + participantIndex + " taskIndex " + taskIndex + " taskName " + taskName + " taskCount " + taskList.Count);

            if (participantList[participantIndex].tasks[taskIndex].checkedTags.Count > 0)
            {
                if (participantList[participantIndex].tasks[taskIndex].checkedTags.Exists(x => x.tag == tagName))
                {
                    toggle.GetComponent<Toggle>().isOn = true;
                }

                if (!participantList[participantIndex].tasks[taskIndex].checkedTags.Exists(x => x.tag == tagName))
                {
                    toggle.GetComponent<Toggle>().isOn = false;
                }
            }
        }
    }

    void updateNames()
    {
        videoNameElements = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name.Split(' '); // 0-participant name 1-taskName
        taskName = videoNameElements[1];
        participantName = videoNameElements[0];
    }

    public void deleteTag()
    {
        int index;
        index = mainCamera.GetComponent<lists>().tagPool.FindIndex(x => x.name == tagName);
        mainCamera.GetComponent<lists>().tagPool.RemoveAt(index);
        mainCamera.GetComponent<lists>().SaveTags();

        for (int i = 0; i < participantList.Count; i++)
        {
            for (int j = 0; j < participantList[i].tasks.Count; j++)
            {
                participantList[i].tasks[j].checkedTags.RemoveAll(x => x.tag == tagName);
            }
        }

        mainCamera.GetComponent<databaseActivity>().writeListToFile();
        mainCamera.GetComponent<duplicateCameraPlane>().startDuplicate();

    }

    public void OnMouseOver()
    {

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Delete))
        {
            toggle.GetComponent<Toggle>().isOn = false;
            deleteTag();

        }
    }

    public void addTag()
    {

        string tagPoolName = addTagName.transform.GetChild(2).GetComponent<Text>().text;

        if (mainCamera.GetComponent<lists>().tagPool.Exists(x => x.name == tagPoolName) == false)
        {

            mainCamera.GetComponent<lists>().tagPool.Add(new Tags(tagPoolName));
            mainCamera.GetComponent<newTagAddBroadCaster>().addTag();

        }

        mainCamera.GetComponent<lists>().SaveTags();


    }


    void Update () {
	
	}
}
