using UnityEngine;
using System.Collections;

//Swinging chain saw class
public class SwingingChainsawEnemy : MonoBehaviour {

	#region Variables
	//Chain saw variables
	public static GameMaster gm;
	private BunnyStats bunny;
	private float rotationSpeed = 5f;
	private int sawDamage = 50;
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

	#region Update
	void Update () {

		//Rotate the saw
		transform.Rotate (0, 0, rotationSpeed);
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches the chain or saw
	void OnCollisionEnter2D (Collision2D touchSaw) {
		if (touchSaw.gameObject.tag == "Player") {
			bunny.DamageBunny (sawDamage);
		}
	}
	#endregion
}
