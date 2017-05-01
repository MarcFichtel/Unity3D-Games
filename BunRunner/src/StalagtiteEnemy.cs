using UnityEngine;
using System.Collections;

//Stalagtite enemy / projectile class
public class StalagtiteEnemy : MonoBehaviour {

	#region Variables
	//Stalagtite variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public int stalagtiteDamage = 50;
	public float stalagtiteSpeed = 0.1f;
	public float cameraStalagtiteOffset = 2f;
	private bool stalagtiteSoundPlaying = false;
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

		//Stalagtite starts moving at specified camera offset
		if (Camera.main.transform.position.x >= transform.position.x - cameraStalagtiteOffset) {
			Vector3 temp = transform.position;
			temp.y -= stalagtiteSpeed;
			transform.position = temp;
		}

		//Destroy stalagtite offscreen
		if (transform.position.x <= (Camera.main.transform.position.x - 10)) {
			Destroy (this.gameObject);
		}

		//Play stalagtite sound only once when it starts moving
		if (Application.loadedLevel != 7 &&
			stalagtiteSoundPlaying == false &&
			transform.position.y <= Camera.main.transform.position.y + 5 &&
			Camera.main.transform.position.x >= transform.position.x - cameraStalagtiteOffset) {
			StartCoroutine (playSound ());
		} else if (Application.loadedLevel == 7 &&
			stalagtiteSoundPlaying == false &&
			transform.position.y <= Camera.main.transform.position.y + 10 &&
			Camera.main.transform.position.x >= transform.position.x - cameraStalagtiteOffset) {
			StartCoroutine (playSound ());
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches the stalagtite
	void OnCollisionEnter2D (Collision2D touchStalagtite) {
		if (touchStalagtite.gameObject.tag == "Player") {
			bunny.DamageBunny (stalagtiteDamage);
		}
	}
	#endregion

	#region Methods
	//Play sound once and wait 
	private IEnumerator playSound () {
		gm.stalagtiteSound.Play();
		stalagtiteSoundPlaying = true;
		yield return new WaitForSeconds (1);
	}
	#endregion
}
