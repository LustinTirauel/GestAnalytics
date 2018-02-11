using UnityEngine;
using System.Collections;

public class openAdminPanel : MonoBehaviour {
    public GameObject adminPanel;
    bool notActive;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.R)))
        {

            if (!adminPanel.active && !notActive)
            {
                adminPanel.SetActive(true);
                notActive = true;
            }

            if (adminPanel.active && !notActive)
            {
                adminPanel.SetActive(false);

            }

            if (adminPanel.active && notActive)
            {
                notActive = false;

            }
        }

        if ((Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftAlt)))
        {

            if (!adminPanel.active && !notActive)
            {
                adminPanel.SetActive(true);
                notActive = true;
            }

            if (adminPanel.active && !notActive)
            {
                adminPanel.SetActive(false);

            }

            if (adminPanel.active && notActive)
            {
                notActive = false;

            }
        }
        


    }


}
