using UnityEngine;
using System.Collections;

//Basic Camera Movement class
public class CameraMovement : MonoBehaviour {

	#region Variables
	//Camera variables
	public float camSpeedMax = 3f;
	public float camTimer = 0f;
	public float camMoveDelay = 500f;
	public float camStop = 78f;
	private float camSpeed = 0f;
	#endregion

	#region Start
	void Start () {

		//Set camera timer when level starts
		camTimer = 0f;
		}
	#endregion

	#region Update
	void Update () {

		//Update camera timer
		camTimer += Time.deltaTime;

		if (camTimer >= camMoveDelay) {
			camSpeed += Time.deltaTime;
		}
		if (camSpeed > camSpeedMax) {
			camSpeed = camSpeedMax;
		}

		//Move camera
		if (camTimer >= camMoveDelay &&
		    Camera.main.transform.position.x < camStop) {
			transform.Translate (camSpeed * Time.deltaTime, 0f, 0f);
		}
	}
	#endregion
}
