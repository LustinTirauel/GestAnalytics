using UnityEngine;
using System.Collections;

public class cameraFocus : MonoBehaviour {

	public float dampTime = 1f;
	private Vector3 velocity = Vector3.zero;
	private float zoomVelocity = 0f;
	public Camera camera;
	float cameraSize;
	bool zoomed;
	bool clicked;

	void Start() {

		//camera = GetComponent<Camera>();
	}



	// Update is called once per frame
	void Update () 
	{

		if (clicked) {

			Vector3 point = camera.WorldToViewportPoint(gameObject.transform.position);
			Vector3 delta = gameObject.transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = camera.transform.position + delta;
			camera.transform.position = Vector3.SmoothDamp (camera.transform.position, destination, ref velocity, dampTime);
			//Debug.Log ("camera.transform.position=" + camera.transform.position + " destination=" + destination);
			//Debug.Log ("Mathf.Abs (camera.orthographicSize - 1)=" + Mathf.Abs (camera.orthographicSize - 1));
			//Debug.Log ("Mathf.Abs (camera.orthographicSize - 6)=" + Mathf.Abs (camera.orthographicSize - 6));
			//Debug.Log ("zoomed=" + zoomed);

			if (!zoomed) {

				if (Mathf.Abs (camera.orthographicSize - 1) > 0.05) {
					cameraSize = Mathf.SmoothDamp (camera.orthographicSize, 1, ref zoomVelocity, 0.15f);
					camera.orthographicSize = cameraSize;
				}
				//Debug.Log ("zooming");
				//Debug.Log ("cameraSize=" + camera.orthographicSize);


			}

			if (zoomed) {
				
				//Debug.Log ("zoomingOut");

				if (Mathf.Abs (camera.orthographicSize - 6) > 0.05) {
					cameraSize = Mathf.SmoothDamp (camera.orthographicSize, 6, ref zoomVelocity, 0.15f);
					camera.orthographicSize = cameraSize;
					//Debug.Log ("cameraSizeOut=" + camera.orthographicSize);
				}




			}



			if ( (Mathf.Abs(camera.transform.position.x - destination.x) < 0.01) && ( ((Mathf.Abs(cameraSize - 1) < 0.05)) || ((Mathf.Abs(cameraSize - 6) < 0.05)) ) ) {
				clicked = false;
				//Debug.Log ("clicked=" + clicked);
				//Debug.Log ("camera.transform.position == destination");
			}

			Debug.Log ("clicked=" + clicked);

		}


	}


	public void OnMouseOver() {

		//Debug.Log ("clicked");

		if (Input.GetMouseButtonDown (2)) {
		
			clicked = true;

			if ((Mathf.Abs (camera.orthographicSize - 1) > 0.05)) {
				zoomed = false;
				Debug.Log ("zoomed=false");

			}

			if ((Mathf.Abs (camera.orthographicSize - 1) < 0.05)) {
				zoomed = true;
				Debug.Log ("zoomed=true");

			}

		}
	
		}

	}
