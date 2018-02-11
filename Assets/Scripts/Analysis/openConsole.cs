using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class openConsole : MonoBehaviour {


	public GameObject console;
    public GameObject filterMenu;
	public GameObject header;
	public GameObject createTags;
	public GameObject mainCamera;
	public Slider slider;
	GameObject[] panels;
	bool active;
	bool headerMissionComplete;
    public bool tagsInvisible = true;
    bool tagsActive; //switch bool

	// Use this for initialization
	void Start () {
        tagsInvisible = true;
        tagsActive = false;
	}

    // Update is called once per frame
    void Update() {



        if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.A)))
        {

            if (!console.active && !active) {
                console.SetActive(true);
                filterMenu.SetActive(true);
                header.SetActive(true);
                active = true;
                panels = GameObject.FindGameObjectsWithTag("tagBackground");
                headerMissionComplete = true;

            }


            if (console.active && !active)
            {
                console.SetActive(false);
                filterMenu.SetActive(false);
                header.SetActive(false);
                panels = GameObject.FindGameObjectsWithTag("tagBackground");
            }
                if (console.active && active) {
                    active = false;
                }
            }
  

        if ((Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftAlt)))
        {
            if (!console.active && !active)
            {
                console.SetActive(true);
                filterMenu.SetActive(true);
                header.SetActive(true);
                active = true;
                panels = GameObject.FindGameObjectsWithTag("tagBackground");
                headerMissionComplete = true;
            }


            if (console.active && !active)
            {
                console.SetActive(false);
                filterMenu.SetActive(false);
                header.SetActive(false);
                panels = GameObject.FindGameObjectsWithTag("tagBackground");

            }
                if (console.active && active)
                {

                    active = false;
                }

            }


        if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.T)))
        {

            if (tagsInvisible && !tagsActive)
            {
                tagsInvisible = false;
            }

            if (!tagsInvisible && tagsActive)
            {
                tagsInvisible = true;
                tagsActive = false;
            }



            mainCamera.GetComponent<makeTagsInvisible>().makeInvisible();
            mainCamera.GetComponent<makeTagsInvisible>().tagCreation();

            if (!tagsInvisible && !tagsActive)
                tagsActive = true;

        }


        if ((Input.GetKeyDown(KeyCode.T) && Input.GetKey(KeyCode.LeftAlt)))
        {
            if (tagsInvisible && !tagsActive)
            {
                tagsInvisible = false;
            }

            if (!tagsInvisible && tagsActive)
            {
                tagsInvisible = true;
                tagsActive = false;
            }

            

            mainCamera.GetComponent<makeTagsInvisible>().makeInvisible();
            mainCamera.GetComponent<makeTagsInvisible>().tagCreation();

            if (!tagsInvisible && !tagsActive)
            tagsActive = true;

        }


        if ((Input.GetKeyDown(KeyCode.F)))
		{

			mainCamera.transform.position = new Vector3(0,1,-10);
			mainCamera.GetComponent<Camera>().orthographicSize = 6;
	
		}

		if (mainCamera.transform.position.x != 0 && mainCamera.transform.position.y != 1 && !headerMissionComplete)
		{
			
			header.SetActive(false);
			headerMissionComplete = true;
			
		}

}

	public void arrangeTransparency(){

		GameObject[] tags;
		Color color;
		Color colorC;
		tags = GameObject.FindGameObjectsWithTag ("tags");

		for (int i = 0; i<tags.Length; i++) {
			color = tags [i].transform.GetChild (0).GetComponent<Image> ().color;
			colorC = tags [i].transform.GetChild (0).transform.GetChild (0).GetComponent<Image> ().color;
			color.a = slider.value;
			colorC.a = slider.value + 0.1f;
			//Debug.Log (color);
			tags [i].transform.GetChild (0).GetComponent<Image> ().color = color;
			tags [i].transform.GetChild (0).transform.GetChild (0).GetComponent<Image> ().color = colorC;
		}


	}

}
