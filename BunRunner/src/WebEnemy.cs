using UnityEngine;
using System.Collections;

//Web enemy (not projectile) class
public class WebEnemy : MonoBehaviour {

	#region Variables
	//web variables
	public static GameMaster gm;
	public BunnyStats bunny;
	private int webDamage = 50;
	#endregion

	#region Start, FixedUpdate
	void Start () {
	
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	void FixedUpdate() {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches a web
	void OnCollisionEnter2D (Collision2D touchWeb) {
		if (touchWeb.gameObject.tag == "Player") {
			bunny.DamageBunny (webDamage);
		}
	}
	#endregion
}
