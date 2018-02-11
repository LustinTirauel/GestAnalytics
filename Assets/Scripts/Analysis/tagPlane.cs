using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class tagPlane : MonoBehaviour {

    public Toggle tag;
    public List<Tags> tagPool;
    public GameObject mainCamera;

    // Use this for initialization
    void Start () {
        StartCoroutine(createTags());
        StopCoroutine(createTags());
    }
	
	// Update is called once per frame
	void Update () {
	
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
        GameObject[] filtrationTags = GameObject.FindGameObjectsWithTag("filtrationTags"); 

        for (int i =0 ; i<filtrationTags.Length; i++)
        {
            DestroyObject(filtrationTags[i]);
        }

        tagPool = mainCamera.GetComponent<lists>().tagPool;
        //Resources.UnloadUnusedAssets(); //delete all unused objects
        Debug.Log("tagPoolCount " + tagPool.Count);

        for (int j = 0; j < tagPool.Count; j++)
        {
            float positionX = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.position.x; //equilizing the position to main object
            float positionY = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.position.y; //equilizing the position to main object
            newTag = Instantiate(tag) as Toggle;                                                                                 //instantiating the object
            newTag.transform.SetParent(gameObject.transform.GetChild(1).transform.GetChild(0).transform, false);                 //setting this cameraplane as parent
            newTag.transform.GetChild(1).gameObject.GetComponent<Text>().text = tagPool[j].name;                                 //setting the text of the tag getting data from the tagpool
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
}
