using UnityEngine;
using System.Collections;

//Falling platforms on city levels
public class CityFallingPlatforms : MonoBehaviour {

	#region Variables
	//variables
	public static GameMaster gm;
	private BunnyStats bunny;
	private float fallingSpeed = 0f; 
	private bool falling = false;
	private float fallingTime = 1.5f;
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
	
		//Get current position
		Vector3 temp = transform.position;

		//Start falling slowly, faster over time
		if (falling == true) {
			fallingSpeed += Time.deltaTime / 30;
			temp.y -= fallingSpeed;
		}

		//Apply new position
		transform.position = temp;
	}
	#endregion

	#region Collisions
	void OnCollisionEnter2D (Collision2D platformCollision) {
		
		//Start falling coroutine if bunny touches platform
		if (platformCollision.gameObject.tag == "Player" &&
		    falling == false) {
			StartCoroutine(StartFall());
		}
	}
	#endregion

	#region Methods
	public IEnumerator StartFall () {
		yield return new WaitForSeconds (fallingTime);
		falling = true;
	}
	#endregion
}
