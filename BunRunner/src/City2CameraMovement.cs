using UnityEngine;
using System.Collections;

//City level 2 Camera movement class
public class City2CameraMovement : MonoBehaviour {

	#region Variables
	//City 2 camera movement variables
	public static GameMaster gm;
	private BunnyStats bunny;
	private float camStop = -21f;
	#endregion

	#region Start, FixedUpdate
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}
	}
	
	void FixedUpdate () {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}
	#endregion

	#region Update
	void Update () {

		//Get current position
		Vector3 camPos = transform.position;

		//Get bunny for camPos
		if (bunny != null) {
			camPos.x = bunny.transform.position.x;
			camPos.y = bunny.transform.position.y + 2.5f;
		}
		
		//Camera doesn't exceed end of level 
		if (camPos.x <= camStop) {
			camPos.x = camStop;
		}
		
		//Apply movement
		transform.position = camPos;
	}
	#endregion
}
