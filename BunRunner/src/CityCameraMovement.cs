using UnityEngine;
using System.Collections;

//City level 1 Camera movement class
public class CityCameraMovement : MonoBehaviour {

	#region Variables
	//Camera variables
	public static GameMaster gm;
	private BunnyStats bunny;
	private float camSpeedMax = 1.5f;
	private float camTimer = 0;
	private float camMoveDelay = 3f;
	public float camStop = -100f;
	private float camSpeed = 0f;
	private CityWind windzone;
	#endregion

	#region Start
	void Start () {

		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		if (windzone == null) {
			windzone = GameObject.FindGameObjectWithTag ("windzone").GetComponent<CityWind> ();
		}
	}
	#endregion

	#region Update, FixedUpdate
	void FixedUpdate() {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}

	void Update () {

		//Get current position
		Vector3 temp = transform.position;

		//Camera y position follows bunny above ground height
		if (bunny != null &&
		    bunny.transform.position.y >= 0 &&
		    !windzone.insideWindzone) {
			temp.y = bunny.transform.position.y;
		}

		//Apply new position
		transform.position = temp;

		//Update camera timer
		camTimer += Time.deltaTime;

		//Camera starts moving after the initial delay
		if (camTimer >= camMoveDelay) {
			camSpeed += Time.deltaTime;
		}

		//Limit camera speed
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
