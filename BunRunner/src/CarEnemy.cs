using UnityEngine;
using System.Collections;

//Car enemy class
public class CarEnemy : MonoBehaviour {

	#region Variables
	//Car variables
	public static GameMaster gm;
	private BunnyStats bunny;
	private int carDamage = 50;
	private float carSpeed = 0.1f;
	private PolygonCollider2D carCol;
	#endregion

	#region Start
	void Start () {
	
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		//Get collider
		carCol = GetComponent<PolygonCollider2D> ();
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate() {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Destroy car if goal has been reached
		if (gm.trackEnd.goalReached) {
			carCol.enabled = false;
		}
	}
	#endregion

	#region Update
	void Update () {
	
		//Get position and scale
		Vector3 position = transform.position;
		Vector3 scale = transform.localScale;

		//Move car left or right, flip x-scale if car is moving left
		if (gm.carMovingRight) {
			position.x += carSpeed;
			scale.x = 4;
		} else if (!gm.carMovingRight) {
			position.x -= carSpeed;
			scale.x = -4;
		}

		//Apply new position and scale
		transform.position = position;
		transform.localScale = scale;

		//Destroy car offscreen
		if (transform.position.x >= (Camera.main.transform.position.x + 15) ||
		    transform.position.x <= (Camera.main.transform.position.x - 15)) {
			Destroy (this.gameObject);
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches a car
	void OnCollisionEnter2D (Collision2D touchCar) {
		if (touchCar.gameObject.tag == "Player") {
			bunny.DamageBunny (carDamage);
		}
	}
	#endregion
}
