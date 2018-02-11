using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;    
using System.Collections.Generic;
using System.IO;


public class duplicateCameraPlane : MonoBehaviour
{
    
    public List<FilteredTags> filteredTagsList = new List<FilteredTags>();
    public List<fCheckedTag> fCheckedTagList = new List<fCheckedTag>();
    public List<fCheckedParticipant> fCheckedParticipantList = new List<fCheckedParticipant>();
    public List<Participants> participantList;

    public GameObject mainCamera;
    public GameObject cameraPlane; //plane for duplications
    public InputField videoNameInput; //videoName in input name
    public InputField gridCountInput; //gridCount in input name
    public Toggle intuitiveToggle; //toggle for intuitive bool.
    public Toggle innovativeToggle; //toggle for intuitive bool.
    public Toggle listToggle; //toggle for intuitive bool.
    public Toggle gridToggle; //toggle for intuitive bool.
    public Toggle andToggle;
    public static bool and;
    public static bool or;
    public int grid = 7; //grid column count.
    public bool filterMode;
    public bool filtered;
    public bool taskMode;

    int gridCount; //operetional integer for grid function
    int subStringHelper;

    float positionIncreaseX; //operetional integer the plane's position
    float positionIncreaseY; //operetional integer the plane's position

    MovieTexture movie;
    GameObject[] planes;
    AudioSource aud;
    Renderer r;
    WWW www;

    public Material cameraMaterial;


    string[] filePath;
    string fileURL;
    string fileURLControl;
    string videoName;


    public bool intuitive = true;
    public bool innovative;
    public bool list;

    // Use this for initialization
    void Start()
    {


        //filePath = AssetDatabase.GetSubFolders ("Assets/Resources/AnalysisVideos")
        and = true;
        startDuplicate();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startDuplicate()
    {


        if (gridToggle.isOn)
        {
            list = false;
        }

        if (listToggle.isOn)
        {
            list = true;
        }

        if (innovativeToggle.isOn)
            innovative = true;

        if (!innovativeToggle.isOn)
            innovative = false;

        if (intuitiveToggle.isOn)
            intuitive = true;

        if (!intuitiveToggle.isOn)
            intuitive = false;

        int.TryParse(gridCountInput.text, out grid);
        //Debug.Log ("grid " + grid);

        if (list)
        { //making a 1 column grid makes a list view.
            grid = 1;
            Debug.Log("list " + grid);
        }

        planes = GameObject.FindGameObjectsWithTag("planes");

        for (int i = 0; i < planes.Length; i++)
        {
            Destroy(planes[i]);
        }


        positionIncreaseX = 0;
        positionIncreaseY = 0;
        gridCount = 0;
        Resources.UnloadUnusedAssets();
        Debug.Log("startDuplicate");

        StopCoroutine(duplicate());
        StartCoroutine(duplicate());
    }

    public void startFiltrate()
    {

        if (gridToggle.isOn)
        {
            list = false;
        }

        if (listToggle.isOn)
        {
            list = true;
        }

        if (innovativeToggle.isOn)
            innovative = true;

        if (!innovativeToggle.isOn)
            innovative = false;

        if (intuitiveToggle.isOn)
            intuitive = true;

        if (!intuitiveToggle.isOn)
            intuitive = false;

        int.TryParse(gridCountInput.text, out grid);
        //Debug.Log ("grid " + grid);

        if (list)
        { //making a 1 column grid makes a list view.
            grid = 1;
            Debug.Log("list " + grid);
        }

        planes = GameObject.FindGameObjectsWithTag("planes");

        for (int i = 0; i < planes.Length; i++)
        {
            Destroy(planes[i]);
        }


        positionIncreaseX = 0;  
        positionIncreaseY = 0;
        gridCount = 0;
        Resources.UnloadUnusedAssets();
        Debug.Log("startDuplicate");
        filtered = true;


        StartCoroutine(filtrate());
        StopCoroutine(filtrate());
    }

    public void setAndOr()
    {
        if (andToggle.isOn)
        {
            and = true;
            or = false;
        }

        if (!andToggle.isOn)
        {
            and = false;
            or = true;
        }



    }

    public void setTaskMode()
    {
        taskMode = !taskMode;
    }

    private IEnumerator filtrate()
    {

        string planeName;
        participantList = mainCamera.GetComponent<databaseActivity>().participantList;
        bool exists = false;
        filteredTagsList.Clear();

        if (or) //filter tags using the or operator
        {

            for (int z = 0; z < fCheckedTagList.Count; z++) //create a loop for each tag checked
            {
                for (int i = 0; i < participantList.Count; i++) //create a loop for each participant
                {
                    for (int j = 0; j < participantList[i].tasks.Count; j++) //create a loop for each task in participant[i]
                    {
                        if ((participantList[i].tasks[j].checkedTags.Exists(x => x.tag == fCheckedTagList[z].name)) && //if the checked tag exists in the current list add it to the filteredTagsList
                            (!(filteredTagsList.Exists(x => x.participantName == participantList[i].participant && x.taskName == participantList[i].tasks[j].task))) //if the current video is already there pass
                            )
                        {
                            if (fCheckedParticipantList.Count > 0) //if the user add participant names for filtration go in this function
                            {
                                for (int y = 0; y < fCheckedParticipantList.Count; y++) //check if the addded participants correspond with the intended tag
                                {
                                    if (fCheckedParticipantList[y].participantName == participantList[i].participant)
                                    {
                                        if (taskMode) //if taskmode is active bring the task and all of these things
                                        {
                                            Debug.Log("taskMode");
                                            if (participantList[i].tasks[j].task == videoNameInput.transform.GetChild(2).gameObject.GetComponent<Text>().text) //if the taskName of the video equals to the input area for task
                                                filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant));
                                        }

                                        else { //if task mode is not true, than look only for participants

                                            filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant));
                                        } 
                                    }
                                }
                            }

                            if (taskMode && fCheckedParticipantList.Count < 1)  //if participantTags were not checked look only for the task which intersect with tags 
                            {
            
                                if (participantList[i].tasks[j].task == videoNameInput.transform.GetChild(2).gameObject.GetComponent<Text>().text)
                                filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant));
                            }

                            if (fCheckedParticipantList.Count < 1 && !taskMode)
                            {
                                filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant)); //if it is not either participant or task mode just add the videos
                            }
                        }
                            
                    }
                }
            }
        }


        if (and) //filter the tags using the and operator
        {


            for (int i = 0; i < participantList.Count; i++) //create a for loop for checking each participant
            {
                for (int j = 0; j < participantList[i].tasks.Count; j++) //create a for loop for checking each task in participant[i]
                {
                    for (int z = 0; z < fCheckedTagList.Count; z++) //create a for loop for adding each checked tag
                    {

                        if (participantList[i].tasks[j].checkedTags.Exists(x => x.tag == fCheckedTagList[z].name)) //if the checked tag exists in current list, than continue adding
                        {
                            exists = true;
                        }

                        if (!participantList[i].tasks[j].checkedTags.Exists(x => x.tag == fCheckedTagList[z].name)) //if the checked tag does not exists in current list, break out from this for loop
                        {
                            exists = false;
                            break;
                        }
                    }

                    if (exists &&                                                                                                                              //if each tag exists in this task, add it to the filtered tagList
                        (!filteredTagsList.Exists(x => x.participantName == participantList[i].participant && x.taskName == participantList[i].tasks[j].task))) //if filtered tag list already has the current video, pass it
                    {
                        if (fCheckedParticipantList.Count > 0) //if the user add participant names for filtration go in this function
                        {
                            for (int y = 0; y < fCheckedParticipantList.Count; y++) //check if the addded participants correspond with the intended tag
                            {   
                                if (fCheckedParticipantList[y].participantName == participantList[i].participant)
                                {
                                    if (taskMode) //if task mode just add the entries with the corresponding task
                                    {
                                        if (participantList[i].tasks[j].task == videoNameInput.transform.GetChild(2).gameObject.GetComponent<Text>().text)
                                            filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant));
                                    }

                                    else //if not task mode go ahead with all matching participants
                                    { 

                                        filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant));
                                    }
                                }
                            }
                        }

                        if (taskMode && fCheckedParticipantList.Count < 1) //if it is task mode but not participant behave accordingly (meaning that add tags which intersect with the current task)
                        {
                            if (participantList[i].tasks[j].task == videoNameInput.transform.GetChild(2).gameObject.GetComponent<Text>().text)
                                filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant));
                        }

                        if (!taskMode && fCheckedParticipantList.Count < 1)//if not participant preference and task mode add the tag directly
                        {
                            filteredTagsList.Add(new FilteredTags(participantList[i].tasks[j].task, participantList[i].participant));
                        }
                    }
                }
            }

        }


        if (fCheckedParticipantList.Count > 0 && fCheckedTagList.Count < 1) //if user only choose participants then come here (this is really a dirty code, I think something is wrong!)
        {
            {

                for (int j = 0; j < fCheckedParticipantList.Count; j++) //check each checked participant
                {
                    int index = participantList.FindIndex(x => x.participant == fCheckedParticipantList[j].participantName); //find index for these particpants

                    for (int z = 0; z < participantList[index].tasks.Count; z++) //check eack task in this participant index
                    {
                        if (taskMode)
                        {
    
                            if (participantList[index].tasks[z].task == videoNameInput.transform.GetChild(2).gameObject.GetComponent<Text>().text && //if task mode is active bring participants only in this task (meaning that only just only one little participant!
                                !(filteredTagsList.Exists(x => x.participantName == participantList[index].participant && x.taskName == participantList[index].tasks[z].task)))
                                filteredTagsList.Add(new FilteredTags(participantList[index].tasks[z].task, participantList[index].participant));
                        }

                        if (!taskMode) //if task mode is not active which is logicle if you look for only participants than everything is normal
                        { 

                            if (!(filteredTagsList.Exists(x => x.participantName == participantList[index].participant && x.taskName == participantList[index].tasks[z].task)))
                            filteredTagsList.Add(new FilteredTags(participantList[index].tasks[z].task, participantList[index].participant));
                        }
                    }
                }

            }
        }



        GameObject planeDuplicate = cameraPlane;
        if (filteredTagsList.Count > 40)
        {
            filterMode = true;
        }

        if (filteredTagsList.Count <= 40)
        {
            filterMode = false;
        }

        //Instantiate the planes and name them.
        for (int videoNameCount = 0; videoNameCount < filteredTagsList.Count; videoNameCount++)
        {
            planeName = filteredTagsList[videoNameCount].participantName + " " + filteredTagsList[videoNameCount].taskName;
            planeDuplicate = Instantiate(cameraPlane) as GameObject;
            planeDuplicate.name = planeName; //participantName
            planeDuplicate.SetActive(true);
            planeDuplicate.transform.position = new Vector3(cameraPlane.transform.position.x + positionIncreaseX, cameraPlane.transform.position.y - positionIncreaseY, cameraPlane.transform.position.z);
            positionIncreaseX += 2.3f;
            gridCount++;
            if (gridCount % grid == 0)
            {
                positionIncreaseX = 0;
                positionIncreaseY += 2.3f;
                //gridCount = 0; 
            }
        

            yield return new WaitForSeconds(0f);

        }

    }

    private IEnumerator duplicate()
    {
        subStringHelper = Application.streamingAssetsPath.Length;
        filePath = Directory.GetDirectories(Application.streamingAssetsPath + "/AnalysisVideos");
        string planeName;
        bool addGrid; // because of innovative and intuitive confliction, time to time a grid gap will be created without adding the plane. With this bool, it will be prevented.

        GameObject planeDuplicate = cameraPlane;

        //Instantiate the planes and name them.
        for (int videoNameCount = 0; videoNameCount < filePath.Length; videoNameCount++)
        {
            planeName = filePath[videoNameCount].Substring(subStringHelper + 16) + " " + videoNameInput.text;

            addGrid = false;

            if (intuitive)
            {
                if (planeName.Contains("innovative") == false)
                {
                    planeDuplicate = Instantiate(cameraPlane) as GameObject;
                    planeDuplicate.name = planeName; //participantName
                    planeDuplicate.SetActive(true);
                    addGrid = true;
                }
            }

            if (innovative)
            {
                if (planeName.Contains("innovative") == true)
                {
                    planeDuplicate = Instantiate(cameraPlane) as GameObject;
                    planeDuplicate.name = planeName; //participantName
                    planeDuplicate.SetActive(true);
                    addGrid = true;
                }
            }

            if (addGrid)
            {
                planeDuplicate.transform.position = new Vector3(cameraPlane.transform.position.x + positionIncreaseX, cameraPlane.transform.position.y - positionIncreaseY, cameraPlane.transform.position.z);
                positionIncreaseX += 2.3f;
                gridCount++;
                if (gridCount % grid == 0)
                {
                    positionIncreaseX = 0;
                    positionIncreaseY += 2.3f;
                    //gridCount = 0; 
                }
            }

            yield return new WaitForSeconds(0f);

        }


    }


}   

