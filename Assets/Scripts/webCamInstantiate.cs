using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class webCamInstantiate : MonoBehaviour {

    WebCamTexture webcam;
    public AVProMovieCaptureFromTexture capture;
    WebCamDevice[] devices;
    int deviceNumber;


    // Use this for initialization
    void Start() {

        deviceNumber = int.Parse(gameObject.name.Split('_')[1]);
        devices = WebCamTexture.devices;
        Debug.Log("deviceNumber " + deviceNumber);
        Debug.Log("deviceName " + devices[deviceNumber].name);
        webcam = new WebCamTexture(devices[deviceNumber].name,640,480,30);
        //webcam = new WebCamTexture("", 1920, 1080, 30);
        webCamPlay();

	}
	
	// Update is called once per frame
	void Update () {
	}

    public void webCamPlay()
    {
        Renderer r = new Renderer();
        r = gameObject.GetComponent<Renderer>();
        r.material.mainTexture = webcam;
        webcam.Play();

        capture._outputFolderPath = "ExportedData/" + gameObject.name + "_" + System.DateTime.Now.ToString("yyyyMMddHHmm");
        capture._forceFilename = gameObject.name + ".mpeg4";

        if (webcam.isPlaying)
        {
            bool requiresPOT = (SystemInfo.npotSupport == NPOTSupport.None);
            if (requiresPOT)
            {
                // WebCamTexture actually uses a power of 2 texture so we need to only grab a region of it
                float p2Width = Mathf.NextPowerOfTwo(webcam.width);
                float p2Height = Mathf.NextPowerOfTwo(webcam.height);
                capture.SetSourceTextureRegion(webcam, new Rect(0, 0, webcam.width / p2Width, webcam.height / p2Height));
            }

            else {
                capture.SetSourceTexture(webcam);
            }
        }
    }
}
