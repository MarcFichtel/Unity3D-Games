using UnityEngine;
using System.Collections;

//Boss 3 Camera movement class
public class Boss3CameraMovement : MonoBehaviour {

	#region Variables
	//Boss 3 Camera variables
	public float camSpeedMax = 5f;
	public float camTimer = 0f;
	public float camMoveDelay = 500f;
	public float camStop = 78f;
	private LastTrackEnd lastTrackEnd;
	private float camSpeed = 0f;
	private AudioSource lastBossSong;
	private float songFadeoutSpeed = 0.003f;
	#endregion

	#region Start, Update
	void Start () {

		//Get track end
		lastTrackEnd = GameObject.Find ("Last Track End").GetComponent<LastTrackEnd> ();

		//Get audio source for fade out
		lastBossSong = GetComponent<AudioSource> ();

		//Set camera timer when level starts
		camTimer = 0f;
	}
	
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

		//Slowly fade song out once the final track end has been reached
		if (lastTrackEnd.goalReached == true) {
			lastBossSong.volume -= songFadeoutSpeed;
		}
	}
	#endregion
}
