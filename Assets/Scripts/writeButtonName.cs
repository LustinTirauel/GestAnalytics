using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class writeButtonName : MonoBehaviour {

	public getButtonName write;
	bool buttonClicked;
	bool secondBool; //this is my little dirty method that uses two bool. So this bool is just for using this method
	int x; //question number
	string questionNumber;
	public GameObject nextButton;
	//public Toggle[] toggles;


	// Use this for initialization
	void Start () {
		questionNumber = gameObject.name.Substring(3,1);
		int.TryParse(questionNumber, out x);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//in every click, the name of the button will be stored in the list.
	public void clicked(){



		if (!buttonClicked) {
			secondBool = false;

			}

		          
		if (!buttonClicked && !secondBool) {
			write.answer.Add (new Answers ((gameObject.name.Substring(0,2)), x));
			write.answer.Sort();
			/*foreach (Answers button in write.answer) {
				print (button.name+ " " + button.number);
			}*/
			buttonClicked = true;
		}

		if (secondBool) {
			buttonClicked = false;
		if(write.answer.Exists(x => x.name.Contains(gameObject.name.Substring(0,2)))){
			//Debug.Log (write.answer.FindIndex (x => x.name.Contains(gameObject.name.Substring(0,2))));
			write.answer.RemoveAt (write.answer.FindIndex (x => x.name.Contains(gameObject.name.Substring(0,2))));
		}
	}


		//Debug.Log ("List Length" + write.answer.Count);
		
		if (write.answer.Count == 4) {
			nextButton.SetActive (true);
		} 
		
		if (write.answer.Count < 4) {
			nextButton.SetActive (false);
		}

		secondBool = true;
}

	//this is for clearing the marked ovals for the next question.
	public void ClearToggles (){
		//Debug.Log ("so-called clear toggles is running");

		Toggle[] toggles = FindObjectsOfType<Toggle>() as Toggle[];
		//Debug.Log ("toggles lenghth" + toggles.Length);
		foreach (Toggle toggle in toggles) {
			toggle.isOn = false;
		}

		}


}


	




