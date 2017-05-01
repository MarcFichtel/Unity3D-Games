using UnityEngine;
using System.Collections;

//Bird Enemy class
public class BirdEnemy : MonoBehaviour {

	#region Variables
	//Bird variables
	public static GameMaster gm;
	public int birdDamage = 20;
	public float birdSpeed = -0.08f;
	private BunnyStats bunny;
	#endregion

	#region Start, Update, FixedUpdate
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}
	
	void Update () {

		if (Application.loadedLevel == 2 ||
		    Application.loadedLevel == 3) {

		//Move bird
		Vector3 temp = transform.position;
		if (!gm.isPaused) {
			temp.x += birdSpeed;
		}
		transform.position = temp;
		
		//Destroy birds offscreen
		if (transform.position.x <= (Camera.main.transform.position.x - 10)) {
			Destroy (this.gameObject);
		}
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
	//Hurt bunny if it touches a bird
	void OnCollisionEnter2D (Collision2D touchBird) {
		if (touchBird.gameObject.tag == "Player") {
			bunny.DamageBunny (birdDamage);
		}
	}
	#endregion
}
