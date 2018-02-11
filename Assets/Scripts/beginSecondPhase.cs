using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class beginSecondPhase : MonoBehaviour {

    public Button nextPhase;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.N)))
        {
            nextPhase.onClick.Invoke();
           
        }

        if ((Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.LeftAlt)))
        {

            nextPhase.onClick.Invoke();
        }



    }
}
