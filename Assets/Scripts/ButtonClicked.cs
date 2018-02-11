using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClicked: MonoBehaviour {
	
	Text txt;
    bool clicked = false;

	
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text = "Record";

	}
	
	// Update is called once per frame
	void Update () {
		}

	public void Clicked (){

        clicked = !clicked;

        if (clicked)
        {
            txt.text = "Stop";
        }

        if (!clicked){
		txt.text = "Record";
			}
	
	}
}


