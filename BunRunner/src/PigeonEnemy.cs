using UnityEngine;
using System.Collections;

//Pigeon enemy class
public class PigeonEnemy : MonoBehaviour {

	#region Variables
	//Pigeon variables
	public static GameMaster gm;
	public GameObject poopPrefab;
	private int pigeonDamage = 50;
	private float pigeonSpeed;
	private float descendingSpeed = 0.05f;
	private BunnyStats bunny;
	private bool pooping = false;
	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		if (Application.loadedLevel == 8 ||
			Application.loadedLevel == 9) {
			pigeonSpeed = 0.1f;
		} else {
			pigeonSpeed = 0.2f;
		}
	}
	#endregion

	#region Update, FixedUpdate
	void FixedUpdate() {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}
	
	void Update () {

		//Get current position
		Vector3 temp = transform.position;

		//Move pigeon
		temp.x += pigeonSpeed;

		//Avoid annoying offscreen pidgeons by clamping them at the top of the camera
		if (temp.y > Camera.main.transform.position.y + 4) {
			temp.y -= descendingSpeed;
		}

		//Apply new position
		transform.position = temp;

		//Shoot da poop!
		if (pooping == false) {
			pooping = true;
			StartCoroutine(shootPoop());
		}

		//Destroy pigeon offscreen
		if (transform.position.x >= (Camera.main.transform.position.x + 15)) {
		Destroy (this.gameObject);
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches a pigeon
	void OnCollisionEnter2D (Collision2D touchPigeon) {
		if (touchPigeon.gameObject.tag == "Player") {
			bunny.DamageBunny (pigeonDamage);
		}
	}
	#endregion

	#region Methods
	private IEnumerator shootPoop () {
		Instantiate (poopPrefab, this.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (Random.Range (1, 4));
		pooping = false;
	}
	#endregion
}
