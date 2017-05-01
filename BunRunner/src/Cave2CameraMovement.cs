using UnityEngine;
using System.Collections;

//Cave 2 Camera movement class
public class Cave2CameraMovement : MonoBehaviour {

	#region Variables
	//Camera variables
	private float camSpeedMax = 3f;
	private float camTimer = 0f;
	private float camMoveDelay = 3f;
	public float camStop = 28f;
	private float camSpeed = 0f;
	private bool movingRight = true;
	private bool movingUp = true;
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

		//Speed up camera movement progressively after camMoveDelay
		if (camTimer >= camMoveDelay) {
			camSpeed += Time.deltaTime;
		}

		//Limit camera speed
		if (camSpeed > camSpeedMax) {
			camSpeed = camSpeedMax;
		}

		//Move camera right
		if (camTimer >= camMoveDelay &&
			Camera.main.transform.position.x < camStop &&
			movingRight == true) {
			transform.Translate (camSpeed * Time.deltaTime, 0f, 0f);
		
		//Move camera down
		} else  if (camTimer >= camMoveDelay &&
		            Camera.main.transform.position.x < camStop &&
		            movingUp == false &&
		            movingRight == false) {
			transform.Translate (0f, (camSpeed * Time.deltaTime) * -1, 0f);
		
		//Move camera up
		} else if (camTimer >= camMoveDelay &&
		           Camera.main.transform.position.x < camStop &&
		           movingUp == true &&
		           movingRight == false) {
			transform.Translate (0f, camSpeed * Time.deltaTime, 0f);
		}
	}
	#endregion

	#region Collisions
	//Change direction if a switch is touched (via a simple 1*2 matrix of movingRight and movingUp)
	void OnCollisionEnter2D (Collision2D touchSwitch) {
		if (touchSwitch.gameObject.tag == "Switch1") {
			movingRight = false;
			movingUp = false;
		} else if (touchSwitch.gameObject.tag == "Switch2") {
			movingRight = true;
		} else if (touchSwitch.gameObject.tag == "Switch3") {
			movingRight = false;
			movingUp = true;
		} else if (touchSwitch.gameObject.tag == "Switch4") {
			movingRight = true;
		}
	}
	#endregion
}
