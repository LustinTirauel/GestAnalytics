using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class cameraPlane : MonoBehaviour {

    public List<Participants> participantList = new List<Participants>();
    public List<Tasks> taskList = new List<Tasks>();
    public List<CheckedTags> checkedTagList = new List<CheckedTags>();
    public List<FilteredTags> filteredTagsList = new List<FilteredTags>(); //for filtering the videos according to tag
    public List<fCheckedTag> fCheckedTagList = new List<fCheckedTag>();
    string[] videoNameElements;
    string taskName;
    string participantName;
    public List<Tags> tagPool;

    public GameObject mainCamera;
    public GameObject participantIndicator;
    public Toggle tag;
    public Slider slider;
    public double countTag;
    public InputField videoNameInput;
    public WWW www;
    MovieTexture movie;
    Renderer r;

    bool tagsBackgroundVisible;
    bool onMouseCreated;


    public GameObject highLight;
    bool muted;
    AudioSource aud;



    // Use this for initialization


    void Start () {

        videoNameElements = gameObject.name.Split(' '); // 0-participant name 1-taskName
        taskName = videoNameElements[1];
        participantName = videoNameElements[0];
       

        participantList = mainCamera.GetComponent<databaseActivity>().participantList;
        taskList = mainCamera.GetComponent<databaseActivity>().taskList;
        checkedTagList = mainCamera.GetComponent<databaseActivity>().checkedTagList;
        tagPool = mainCamera.GetComponent<lists>().tagPool;

        makeTagsInvisible.madeInvisible += tagBackgroundInvisible;
        makeTagsInvisible.createTags += startCreatingTags;
        newTagAddBroadCaster.addTagBroadcast += addNewTagListener;
        EventManager.changeName += changePlaneNames;
        r = gameObject.GetComponent<Renderer>();
        aud = gameObject.GetComponent<AudioSource>();

        //moviePlay();
        //startCreatingTags();

    }
	
    void OnDestroy()
    {
        makeTagsInvisible.madeInvisible -= tagBackgroundInvisible;
        newTagAddBroadCaster.addTagBroadcast -= addNewTagListener;
        EventManager.changeName -= changePlaneNames;
        makeTagsInvisible.createTags -= startCreatingTags;
    }

	// Update is called once per frame
	void Update () {
    }

    void OnBecameVisible()
    {
       // Debug.Log("Renderer in camera " + participantName + " " + taskName);
        if (movie == null)
            moviePlay();
    }

    void OnBecameInvisible()
    {
        //Debug.Log("Renderer is invisible " + participantName + " " + taskName);
        DestroyObject(movie);
    }

    public void addCheckedTag(string tagName)
    {
        int participantIndex = participantList.FindIndex(x => x.participant == participantName);
        int taskIndex = taskList.FindIndex(x => x.task == taskName);

        participantList[participantIndex].tasks[taskIndex].checkedTags.Add(new CheckedTags(tagName));


    }

    public void moviePlay ()
    {
        videoNameElements = gameObject.name.Split(' '); // 0-participant name 1-taskName
        participantName = videoNameElements[0];
        taskName = videoNameElements[1];
        string fileURL = "";

        if (gameObject.name.Contains("innovative") == false) {
            fileURL = "file:///" + Application.streamingAssetsPath + "/AnalysisVideos/" + participantName + "/" + taskName + ".ogv";
        }

        if (gameObject.name.Contains("innovative") == true) {
            fileURL = "file:///" + Application.streamingAssetsPath + "/AnalysisVideos/" + participantName + "/" + taskName + "_innovative.ogv";
        }


        www = new WWW(fileURL);

        while (www.isDone == false)
        {
           // Debug.Log("loading");
        }

        movie = www.movie as MovieTexture;
        r.material.mainTexture = movie;
        aud.clip = movie.audioClip;


        //Make the movie loopable and play it.
        movie.loop = true;
        movie.Play();
        aud.mute = true;
    }

    public void startCreatingTags()
    {
        if (!mainCamera.GetComponent<duplicateCameraPlane>().filterMode && !onMouseCreated)
        { 
            StartCoroutine(createTags());
            makeTagsInvisible.createTags -= startCreatingTags;
            StopCoroutine(createTags());
        }
    }

    void OnMouseOver()
    {
        if (!onMouseCreated)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                    tagsBackgroundVisible = false;
                    StartCoroutine(createTags());
                    StopCoroutine(createTags());
                    onMouseCreated = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);

            }
        }



        if (Input.GetKeyDown(KeyCode.T))
        {

            if (tagsBackgroundVisible)
                gameObject.transform.GetChild(1).gameObject.SetActive(false);


            if (!tagsBackgroundVisible)
                gameObject.transform.GetChild(1).gameObject.SetActive(true);

            tagsBackgroundVisible = !tagsBackgroundVisible;
        }


            AudioSource aud = GetComponent<AudioSource>();
            movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;

            highLight.SetActive(true);

        if (aud.mute == true && !muted && Input.GetKeyDown(KeyCode.S))
        {

            //Debug.Log ("audio.Play");
            movie.Stop();
            movie.Play();
            aud.Play();
            aud.mute = false;
            //Debug.Log (gameObject.transform.GetChild (0).gameObject.name);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            muted = true;

        }


        if (aud.mute == false && !muted && Input.GetKeyDown(KeyCode.S))
        {

            //Debug.Log ("audio.Stop");
            aud.Stop();
            aud.mute = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);


        }

        if (aud.mute == false && muted)
            muted = false;




    }

    void OnMouseExit()
    {
        highLight.SetActive(false);
    }

    private IEnumerator createTags()
    {

        Toggle newTag; //defining a toggle to duplicate
        Color color; //color of unchecked tag
        Color colorC; //color of chechked tag
        float positionIncreaseX = 0; //positions of tags
        float positionIncreaseY = 0; //positions of tags
        countTag = 0;
        onMouseCreated = true;
        Resources.UnloadUnusedAssets(); //delete all unused objects


        for (int j = 0; j < tagPool.Count; j++)
        {
                float positionX = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.position.x; //equilizing the position to main object
                float positionY = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.position.y; //equilizing the position to main object
                newTag = Instantiate(tag) as Toggle;                                                                                 //instantiating the object
                newTag.transform.SetParent(gameObject.transform.GetChild(1).transform.GetChild(0).transform, false);                 //setting this cameraplane as parent
                newTag.transform.GetChild(1).gameObject.GetComponent<Text>().text = tagPool[j].name;                                 //setting the text of the tag getting data from the tagpool
                color = newTag.transform.GetChild(0).GetComponent<Image>().color;                                                    //getting the color of the tag to bind its alpha to slider
                colorC = newTag.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color;                             //getting the color of the tag to bind its alpha to slider
                color.a = slider.value;                                                                                                  //equalizing the alpha to slider
                colorC.a = slider.value + 0.1f;                         
                newTag.transform.GetChild(0).gameObject.GetComponent<Image>().color = color;                                         //setting the color with new alpha
                newTag.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = colorC;

                if (positionIncreaseX >= 2)                                                                                          //arrange the position of new tag
                {
                    positionIncreaseX = 0;
                    positionIncreaseY += 0.2f;
                }

                newTag.transform.position = new Vector3(positionX + positionIncreaseX, positionY - positionIncreaseY, tag.transform.position.z); //put the tag to the new position
                newTag.gameObject.SetActive(true);

            positionIncreaseX += 0.5f;
            countTag++;
            
        }
        yield return null;
        positionIncreaseX = 0;  //reset the position for the new tag
        positionIncreaseY = 0;


        if (mainCamera.GetComponent<openConsole>().tagsInvisible)
            gameObject.transform.GetChild(1).gameObject.SetActive(false);

        if (!mainCamera.GetComponent<openConsole>().tagsInvisible)
            gameObject.transform.GetChild(1).gameObject.SetActive(true);

        if (tagsBackgroundVisible)
            gameObject.transform.GetChild(1).gameObject.SetActive(true);

    }

    void tagBackgroundInvisible()
    {

        if (mainCamera.GetComponent<openConsole>().tagsInvisible)
        gameObject.transform.GetChild(1).gameObject.SetActive(false);

        if (!mainCamera.GetComponent<openConsole>().tagsInvisible)
            gameObject.transform.GetChild(1).gameObject.SetActive(true);


    }

    void addNewTagListener()
    {
        countTag = tagPool.Count - 1;

        Toggle newTag;
        double positionIncreaseX = (countTag % 4d) * 0.5d;
        double positionIncreaseY = (Math.Floor(countTag / 4d)) * 0.2d;
        Color color; //color of unchecked tag
        Color colorC; //color of chechked tag
        float positionX = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.position.x; //equilizing the position to main object
        float positionY = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.position.y;

        newTag = Instantiate(tag) as Toggle;
        newTag.transform.SetParent(gameObject.transform.GetChild(1).transform.GetChild(0).transform, false);                 //setting this cameraplane as parent
        newTag.transform.GetChild(1).gameObject.GetComponent<Text>().text = tagPool[tagPool.Count - 1].name;                 //setting the text of the tag getting data from the tagpool
        color = newTag.transform.GetChild(0).GetComponent<Image>().color;                                                    //getting the color of the tag to bind its alpha to slider
        colorC = newTag.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color;                             //getting the color of the tag to bind its alpha to slider
        color.a = slider.value;                                                                                              //equalizing the alpha to slider
        colorC.a = slider.value + 0.1f;
        newTag.transform.GetChild(0).gameObject.GetComponent<Image>().color = color;                                         //setting the color with new alpha
        newTag.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = colorC;


        newTag.transform.position = new Vector3(positionX + (float)positionIncreaseX, positionY - (float)positionIncreaseY, tag.transform.position.z); //put the tag to the new position
        newTag.gameObject.SetActive(true);

    }

    void changePlaneNames()
    {
        if (!mainCamera.GetComponent<duplicateCameraPlane>().filtered)
        {
            gameObject.name = videoNameElements[0] + " " + videoNameInput.transform.GetChild(2).gameObject.GetComponent<Text>().text;
            DestroyObject(movie);
            moviePlay();
        }

        else {

            mainCamera.GetComponent<duplicateCameraPlane>().startDuplicate();
            mainCamera.GetComponent<duplicateCameraPlane>().filtered = false;
        }

        participantIndicator.GetComponent<participantIndicator>().nameChange();



    }
}

