using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class participantPlane : MonoBehaviour {

    public Toggle tag;
    public GameObject mainCamera;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(createTags());
        StopCoroutine(createTags());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startCreatingTags()
    {
        StartCoroutine(createTags());
        StopCoroutine(createTags());
    }

    private IEnumerator createTags()
    {
        Toggle newTag; //defining a toggle to duplicate
        float positionIncreaseX = 0; //positions of tags
        float positionIncreaseY = 0; //positions of tags
        GameObject[] filtrationTags = GameObject.FindGameObjectsWithTag("participantTags");
        string[] filePath = Directory.GetDirectories(Application.streamingAssetsPath + "/AnalysisVideos");
        int subStringHelper = Application.streamingAssetsPath.Length;

        for (int i = 0; i < filtrationTags.Length; i++)
        {
            DestroyObject(filtrationTags[i]);
        }

        for (int j = 0; j < filePath.Length; j++)
        {
            float positionX = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.position.x; //equilizing the position to main object
            float positionY = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.position.y; //equilizing the position to main object
            newTag = Instantiate(tag) as Toggle;                                                                                 //instantiating the object
            newTag.transform.SetParent(gameObject.transform.GetChild(0).transform.GetChild(0).transform, false);                 //setting this cameraplane as parent
            newTag.transform.GetChild(1).gameObject.GetComponent<Text>().text = filePath[j].Substring(subStringHelper + 16);                               //setting the text of the tag getting data from fileStructure
            newTag.gameObject.SetActive(true);


            if (positionIncreaseX >= 300f)                                                                                          //arrange the position of new tag
            {
                positionIncreaseX = 0;
                positionIncreaseY += 30f;
            }

            newTag.transform.position = new Vector3(positionX + positionIncreaseX, positionY - positionIncreaseY, tag.transform.position.z); //put the tag to the new position
            newTag.gameObject.SetActive(true);

            positionIncreaseX += 60f;

            
        }

        positionIncreaseX = 0;  //reset the position for the new tag
        positionIncreaseY = 0;
        yield return null;

    }

    public void InnovativeSwitch()
    {
        GameObject[] innovativeToggles;
        innovativeToggles = GameObject.FindGameObjectsWithTag("participantTags");
        int falseCount = 0;


        for (int i = 0; i <innovativeToggles.Length; i++)
        {
            if (innovativeToggles[i].GetComponent<Toggle>().isOn == true && innovativeToggles[i].transform.GetChild(1).gameObject.GetComponent<Text>().text.Contains("innovative"))
            {
                falseCount++;
            }

            if (innovativeToggles[i].transform.GetChild(1).gameObject.GetComponent<Text>().text.Contains("innovative") && innovativeToggles[i].GetComponent<Toggle>().isOn == false)
            innovativeToggles[i].GetComponent<Toggle>().isOn = !innovativeToggles[i].GetComponent<Toggle>().isOn;

           
        }


        Debug.Log(falseCount + " " + innovativeToggles.Length / 2);

        if (falseCount >= innovativeToggles.Length / 2)
        {
            for (int i = 0; i < innovativeToggles.Length; i++)
            {
                if (innovativeToggles[i].transform.GetChild(1).gameObject.GetComponent<Text>().text.Contains("innovative"))
                    innovativeToggles[i].GetComponent<Toggle>().isOn = !innovativeToggles[i].GetComponent<Toggle>().isOn;
            }

        }

    } 

    public void IntuitiveSwitch()
    {
        GameObject[] innovativeToggles;
        innovativeToggles = GameObject.FindGameObjectsWithTag("participantTags");
        int falseCount = 0;


        for (int i = 0; i < innovativeToggles.Length; i++)
        {
            if (innovativeToggles[i].GetComponent<Toggle>().isOn == true && !innovativeToggles[i].transform.GetChild(1).gameObject.GetComponent<Text>().text.Contains("innovative"))
            {
                falseCount++;
            }

            if (!innovativeToggles[i].transform.GetChild(1).gameObject.GetComponent<Text>().text.Contains("innovative") && innovativeToggles[i].GetComponent<Toggle>().isOn == false)
                innovativeToggles[i].GetComponent<Toggle>().isOn = !innovativeToggles[i].GetComponent<Toggle>().isOn;


        }


        Debug.Log(falseCount + " " + innovativeToggles.Length / 2);

        if (falseCount >= innovativeToggles.Length / 2)
        {
            for (int i = 0; i < innovativeToggles.Length; i++)
            {
                if (!innovativeToggles[i].transform.GetChild(1).gameObject.GetComponent<Text>().text.Contains("innovative"))
                    innovativeToggles[i].GetComponent<Toggle>().isOn = !innovativeToggles[i].GetComponent<Toggle>().isOn;
            }

        }


    }
}
