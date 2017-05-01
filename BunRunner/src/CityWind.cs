using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//City level 1 Wind class
public class CityWind : MonoBehaviour {

	#region Variables
	//Wind variables
	public static GameMaster gm;
	private BunnyStats bunny;
	private Vector2 windForceInAir = new Vector2 (-100f, 0);
	private Vector2 windForceOnGround = new Vector2 (-50f, 0);
	public bool insideWindzone = false;
	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ();
		}
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate() {
			
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Apply air wind force, if bunny is inside wind zone and jumping, or ground wind force, if bunny is grounded
		if (insideWindzone &&
		    bunny != null &&
			!bunny.grounded) {
			bunny.bunnyRB.AddForce (windForceInAir);
		} else if (insideWindzone &&
		    bunny != null &&
			bunny.grounded) {
			bunny.bunnyRB.AddForce (windForceOnGround);
		}
	}
	#endregion

	#region Collisions
	//Bunny enters windzone
	void OnTriggerEnter2D(Collider2D enterWindzone) {
		if (enterWindzone.gameObject.tag == "Player") {
			insideWindzone = true;
		}
	}

	//Bunny exits windzone
	void OnTriggerExit2D(Collider2D exitWindzone) {
		if (exitWindzone.gameObject.tag == "Player") {
			insideWindzone = false;
		}
	}
	#endregion
}
