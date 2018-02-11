using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class micStart : MonoBehaviour
{
    string[] devices;
    AudioSource aud;
    //int length = 1000;
	AudioClip microphone;
    bool recorded;
    public Dropdown micName;
    string microphoneName;
   

    void Start()
    {

        aud = GetComponent<AudioSource>();
        
        devices = Microphone.devices;

        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("mic " + Microphone.devices[i]);
            micName.options.Add(new Dropdown.OptionData (devices[i].ToString()));
        }
        //microphone = Microphone.Start("", false, 100, 44100);
        //aud.clip = microphone;
        //Microphone.End ("");
        /* aud = GetComponent<AudioSource>();
         aud.clip = Microphone.Start("", false, length, 44100);
         aud.Play();*/
        //aud.Play ();
        //audioRecord();
    }


	void Update () {

		//Debug.Log ("aud is playing " + aud.isPlaying);
		}

	
    public void changeMicName()
    {


        microphoneName = micName.options[micName.value].text;


    }

    public void audioRecord()
    {


        if (aud.isPlaying)
        {
            Debug.Log("audioStop");
            aud.Stop();
			DestroyObject (aud.clip);
			Microphone.End (microphoneName);

        }
            

        if (!aud.isPlaying && !recorded) {
            Debug.Log("audioStart");
			aud.clip = Microphone.Start(microphoneName, true, 999, 44100);
            while (!(Microphone.GetPosition(microphoneName) > 0)) { }
            Debug.Log(aud.clip);
            aud.Play();
            recorded = true;
        }

        if (!aud.isPlaying && recorded)
            recorded = false;






    }

}


