using UnityEngine;
using System.Collections;

public class dragCamera : MonoBehaviour
{
	public float dragSpeed = 2;
	private Vector3 dragOrigin;

	public bool cameraDragging = true;

	public float outerLeft = -10f;
	public float outerRight = 10f;
	public float outerUp = -10f;
	public float outerDown = 10f;

	private Vector3 velocity = Vector3.zero;
	Vector3 targetPosition;
	Vector3 pos;
	Vector3 move;
	public float smoothTime = 1f;

	float zoom;


	void Start() {

		targetPosition = transform.position;

	}

	void Update()
	{

		float cameraSize = GetComponent<Camera> ().orthographicSize;

		Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		float right = Screen.width * 0.2f;
		float left = Screen.width - (Screen.width * 0.2f);
		float up = Screen.height * 0.2f;
		float down = Screen.height - (Screen.width * 0.2f);


		zoom = Input.GetAxis ("Mouse ScrollWheel");
		//Debug.Log ("zoom " + zoom);

		if (cameraSize > 1 && zoom>0) {
			cameraSize -= 0.8f; 
			GetComponent<Camera> ().orthographicSize = cameraSize;
		}

		if (cameraSize < 8 && zoom<0) {
			cameraSize += 0.8f; 
			GetComponent<Camera> ().orthographicSize = cameraSize;
		}






		if (Input.GetMouseButton (1) ) {

	
			if (mousePosition.x < left) {
				cameraDragging = true;
				targetPosition = new Vector3 (transform.position.x - 0.2f, transform.position.y, transform.position.z);
			} else if (mousePosition.x > right) {
				cameraDragging = true;
				targetPosition = new Vector3 (transform.position.x + 0.2f, transform.position.y, transform.position.z);
			}

			if (mousePosition.y < up) {
				cameraDragging = true;
				targetPosition = new Vector3 (transform.position.x, transform.position.y - 0.2f, transform.position.z);
			} else if (mousePosition.y > down) {
				cameraDragging = true;
				targetPosition = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z);
			}

			pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - dragOrigin);
			move = new Vector3 (pos.x * dragSpeed, pos.y * dragSpeed, 0);
		}





		if (cameraDragging) {

			if (Input.GetMouseButtonDown (1)) {
				//Debug.Log ("here");
				dragOrigin = Input.mousePosition;
			}






			if (!Input.GetMouseButton (1)) {
				//return;
			}

			if (move.x > 0f  || move.x < 0f || move.y > 0f  || move.y < 0f) {

				//Debug.Log ("move= " + move);

					if (this.transform.position.x < outerRight || this.transform.position.x < outerUp) {
					
						transform.Translate (move, Space.World);
						move = Vector3.SmoothDamp(move, Vector3.zero, ref velocity, smoothTime);
					}

				} else {
					if (this.transform.position.x > outerLeft || this.transform.position.x > outerDown) {
					
						transform.Translate (move, Space.World);
						move = Vector3.SmoothDamp(move, Vector3.zero, ref velocity, smoothTime);
					}
				}
			}

	}


}