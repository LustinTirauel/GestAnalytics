using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class taskTextChange : MonoBehaviour {

	Text taskName;
	public MoviePlay mainScript;

	// Use this for initialization
	void Start () {
		taskName = gameObject.GetComponent<Text> ();
	

	
	}


	public void taskNameChange () {

		taskName.text = mainScript.taskNames [mainScript.taskNumber];
	}
}
