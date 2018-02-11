using UnityEngine;
using System.Collections;

public class playAudio : MonoBehaviour {


	AudioSource aud;
	MovieTexture movie;
	public GameObject highLight;
	bool muted;

	// Use this for initialization
	void Start () {
		
		movie = GetComponent<Renderer> ().material.mainTexture as MovieTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseOver() {

        aud = GetComponent<AudioSource>();
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        highLight.SetActive (true);
		if (aud.mute == true && !muted && Input.GetKeyDown (KeyCode.S)) {

			//Debug.Log ("audio.Play");
			movie.Stop();
			movie.Play();
			aud.Play ();
			aud.mute = false;
			//Debug.Log (gameObject.transform.GetChild (0).gameObject.name);
			gameObject.transform.GetChild (0).gameObject.SetActive (true);
			muted = true;

		}


		if (aud.mute == false && !muted && Input.GetKeyDown (KeyCode.S)) {

			//Debug.Log ("audio.Stop");
			aud.Stop ();
			aud.mute = true;
			gameObject.transform.GetChild (0).gameObject.SetActive (false);


		}

		if (aud.mute == false && muted)
			muted = false;

	



	}

	void OnMouseExit() {


		highLight.SetActive (false);



	}
}

