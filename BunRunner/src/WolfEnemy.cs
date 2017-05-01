using UnityEngine;
using System.Collections;

//Wolf enemy class
public class WolfEnemy : MonoBehaviour {

	#region Variables
	//Wolf variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public int wolfDamage = 20;
	public Transform wolfBoundaryRight;
	public Transform wolfBoundaryLeft;
	public float wolfSpeed = -0.1f;
	private bool facingRight;
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

		//Move wolf
		Vector3 temp = transform.position;
		if (!gm.isPaused) {
			temp.x += wolfSpeed;
		}
		transform.position = temp;

		//Make wolf pace in boundary range
		if (transform.position.x >= wolfBoundaryRight.position.x) {
			wolfSpeed *= -1;
			Flip ();
		} else if (transform.position.x <= wolfBoundaryLeft.position.x) {
			wolfSpeed *= -1;
			Flip ();
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches a wolf
	void OnCollisionEnter2D (Collision2D touchWolf) {
		if (touchWolf.gameObject.tag == "Player") {
			bunny.DamageBunny (wolfDamage);
		}
	}
	#endregion

	#region Methods
	//Flip wolf sprite 
	private void Flip() {

		//Switch the way the wolf is labelled as facing.
		facingRight = !facingRight;
		
		//Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	#endregion
}
