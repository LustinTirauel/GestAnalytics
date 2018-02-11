using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class participantIndicator : MonoBehaviour {

	// Use this for initialization
	void Start () {


        nameChange();


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void nameChange()
    {
        string[] tagElements = gameObject.transform.parent.transform.parent.name.Split(' ');
        string tagName = "";
        int subStringLength;

        if (tagElements[0].Length > 6)
            subStringLength = 6;

        else
            subStringLength = tagElements[0].Length;

        tagName = tagElements[0].Substring(0, subStringLength) + " " + tagElements[1];
        gameObject.GetComponent<Text>().text = tagName;



    }
}
