using UnityEngine;
using System.Collections;

//Web projectile class
public class webProjectile : MonoBehaviour {

	#region Variables
	//Web projectile variables
	public static GameMaster gm;
	public BunnyStats bunny;
	private float webSpeed = 0.1f;
	private int webDamage = 50;
	private Vector3 webPos;
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
	
		//Get web position
		webPos = transform.position;

		//Move web
		webPos.x -= webSpeed;

		//Destroy offscreen
		if (transform.position.x <= Camera.main.transform.position.x - 10) {
			Destroy (this.gameObject);
		}

		//Apply movement
		transform.position = webPos;
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches a web
	void OnCollisionEnter2D (Collision2D touchBird) {
		if (touchBird.gameObject.tag == "Player") {
			bunny.DamageBunny (webDamage);
		}
	}
	#endregion
}
