using UnityEngine;
using System.Collections;

//Boss 2 Camera movement class
public class Boss2CameraMovement : MonoBehaviour {

	#region Variables
	//Boss 2 camera movement variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public Vector3 camPos;
	#endregion

	#region Start
	void Start () {
	
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}

		//Set camera position y and z
		camPos = new Vector3 (-60.5f, 7.5f, -10f);
	}
	#endregion

	#region Update, FixedUpdate
	void FixedUpdate () {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}
	
	void Update () {
	
		//Get bunny for camPos
		if (bunny != null) {
			camPos.x = bunny.transform.position.x;
		}

		//Camera doesn't exceed end of level 
		if (camPos.x >= 66.5f) {
			camPos.x = 66.5f;
		}

		//Apply movement
		if (transform.position.x <= camPos.x) {
			transform.position = camPos;
		}
	}
	#endregion
}
