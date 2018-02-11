using UnityEngine;
using System.Collections;

public class duplicateButtons : MonoBehaviour {

	public GameObject questionButton;
	float i = 1;
	float z;
	float y = 2;
	// Use this for initialization
	void Start () {

		//GameObject questionButtonDuplicate;

	
		}
	
	// Update is called once per frame
	void Update () {
	
		if (i < 7) {

			GameObject questionButtonDuplicate;
			questionButtonDuplicate =  Instantiate (questionButton) as GameObject;
			questionButtonDuplicate.transform.SetParent(gameObject.transform, false);
			//questionButtonDuplicate.transform.position = questionButton.transform.position;
			questionButtonDuplicate.transform.position = new Vector3 ((questionButton.transform.position.x + (i*2+(1/2))), questionButton.transform.position.y, questionButton.transform.position.z) ;
			questionButtonDuplicate.name = questionButton.name.Substring(0, questionButton.name.Length - 1) + y;
			
			y++;
			z+=3;
			i++;}
	

	}
}
