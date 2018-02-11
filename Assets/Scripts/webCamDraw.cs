using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class webCamDraw : MonoBehaviour {

    WebCamDevice[] devices;
    private WebCamTexture texture;
	public AVProMovieCaptureFromTexture capture;
	public AVProMovieCaptureGUI gui;
	public MoviePlay mainScript;
    public getButtonName participantName;
    public GameObject nextButton;
    public getButtonName phaseNumber;
    public Dropdown camNumberInput;
    public int camNumber = 0;
	float staticPositionX; // position x when the mouse button is released for first cam
	float staticPositionY; // position y when the mouse button is released for first cam
	float staticPositionSecX; // position x when the mouse button is released for second cam
	float staticPositionSecY; // position y when the mouse button is released for second cam
	float camPosX = Screen.width - 520;
	float camPosY = Screen.height - 400;
	float secCamPosX = 0;
	float secCamPosY = 0;
	bool onClicked; // bool for dragging the camera
	bool onClickedsecCAM; // bool for dragging the 2nd camera
	bool firsTime = true; // bool for defining the position of camera when it is first appeared
	bool recording;
	bool recorded;

    int posX;

	// Use this for initialization
	void Start () {
        
		capture._autoFilenamePrefix = "Demo4Webcam-";
        devices = WebCamTexture.devices;

        for (int i = 0; i<devices.Length; i++)
        {
            camNumberInput.options.Add(new Dropdown.OptionData(devices[i].name));
        }

		texture = new WebCamTexture (devices[camNumber].name, 640, 480, 30);


		}
			

    public void changeCam ()
    {
        

        texture = new WebCamTexture(camNumberInput.options[camNumberInput.value].text, 640, 480, 30);
        //Debug.Log("camName " + camNumberInput.options[camNumberInput.value].text);

    }

	// Update is called once per frame
	void Update () {


		// firstCAM webcam dragging fucntions

		if (firsTime  && Input.mousePosition.x >= camPosX && 
            Input.mousePosition.x <= camPosX + texture.width && 
            Input.mousePosition.y <= -camPosY + Screen.height && 
            Input.mousePosition.y>= -camPosY + Screen.height - texture.height && 
            Input.GetMouseButton(0))
			onClicked = true;

		if (Input.GetMouseButtonUp(0))
			onClicked = false;

		if (!firsTime && 
            Input.mousePosition.x >= staticPositionX && 
            Input.mousePosition.x <= staticPositionX + texture.width &&  
            Screen.height - Input.mousePosition.y  >= staticPositionY &&
			Screen.height - Input.mousePosition.y   <= staticPositionY + texture.height && 
            Input.GetMouseButton (0)) {
			Debug.Log ("burdayım");
			onClicked = true;
		}
		// firstCAM dragging functions ends here

	}

    public void webCamVideoSavePathNameChange()
    {

        //output folder of the exported video
        capture._outputFolderPath = "ExportedData/" + participantName.participantName + "_" + System.DateTime.Now.ToString("yyyyMMddHHmm");


    }

	public void startWebcam () {
			
    //Naming of the exported video
    if (phaseNumber.phase == 1)
	capture._forceFilename = mainScript.videoNames[mainScript.taskNumber-1] +".mpeg4";

    if (phaseNumber.phase == 2)
    capture._forceFilename = mainScript.videoNames[mainScript.taskNumber - 1] + "_innovative.avi";
 

	if (!texture.isPlaying) {

			nextButton.SetActive(false);
			texture.Play ();
			recording = false;
			if (texture.isPlaying) {
				bool requiresPOT = (SystemInfo.npotSupport == NPOTSupport.None);
				if (requiresPOT) {
					// WebCamTexture actually uses a power of 2 texture so we need to only grab a region of it
					float p2Width = Mathf.NextPowerOfTwo (texture.width);
					float p2Height = Mathf.NextPowerOfTwo (texture.height);			
					capture.SetSourceTextureRegion (texture, new Rect (0, 0, texture.width / p2Width, texture.height / p2Height));

				} 

				else {
					capture.SetSourceTexture (texture);

				}

				if (!recording)
					gui.StartCaptureButton ();

			}
		}

		//this indicates when record button pushed when the recording is enabled. 
		//Meaning that when the recording is stopped this functions will be called.
		if (recording){

			capture.StopCapture();
			texture.Stop ();
			nextButton.SetActive(true);

		}
		
		recording = true;

	}



    void OnGUI()
    {

        if (texture.isPlaying) { 

        //webcam dragging functions
        if (firsTime) {
            GUILayout.BeginArea(new Rect(camPosX, camPosY, 480, 360));
            //Debug.Log ("firstTime");

        }

        if (onClicked) {
            GUILayout.BeginArea(new Rect(Input.mousePosition.x, -Input.mousePosition.y + Screen.height, 480, 360));
            staticPositionX = Input.mousePosition.x;
            staticPositionY = -Input.mousePosition.y + Screen.height;
            //Debug.Log ("mousePositionX= " + Input.mousePosition.x + "mousePositionY= " + Input.mousePosition.y + "staticPositionX= " + staticPositionX + "staticPositionY= " + staticPositionY);
            firsTime = false;
        }

        if (!onClicked && !firsTime) {
            //Debug.Log ("staticPositionX= " + staticPositionX + "staticPositionY= " + staticPositionY);
            GUILayout.BeginArea(new Rect(staticPositionX, staticPositionY, 480, 360));
            //Debug.Log ("staticTime");

        }
    }

		//dragging functions end here
		
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal (GUILayout.ExpandWidth (true));
		if (texture != null) {
			Rect camRect = GUILayoutUtility.GetRect (texture.width, texture.height);
			if(texture.isPlaying)	
			GUI.DrawTexture (camRect, texture);
		}
			
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
		GUILayout.EndArea ();


	

	}


		
}
