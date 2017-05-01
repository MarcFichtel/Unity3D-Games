using UnityEngine;
using System.Collections;

//Waterfall enemy class
public class WaterfallEnemy : MonoBehaviour {

	#region Variables
	//Waterfall variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public int waterfallDamage = 50;
	public float waterfallSpeed = 0.13f;
	public float cameraWaterfallOffset = 2.6f;
	private bool waterfallSoundPlaying = false;
	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}
	#endregion

	#region Update
	void Update() {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Waterfall starts moving at specified camera offset
		if (Camera.main.transform.position.x >= transform.position.x - cameraWaterfallOffset) {
			Vector3 temp = transform.position;
			temp.y -= waterfallSpeed;
			transform.position = temp;
		}

		//Destroy waterfall offscreen
		if (transform.position.x <= (Camera.main.transform.position.x - 10)) {
			Destroy (this.gameObject);
		}

		//Play waterfall sound only once when it starts moving
		if (waterfallSoundPlaying == false &&
		    Camera.main.transform.position.x >= transform.position.x - cameraWaterfallOffset) {
			StartCoroutine(playSound());
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches the waterfall
	void OnCollisionEnter2D (Collision2D touchWaterfall) {
		if (touchWaterfall.gameObject.tag == "Player") {
			bunny.DamageBunny (waterfallDamage);
		}
	}
	#endregion

	#region Methods
	//Play sound once and wait 
	private IEnumerator playSound () {
		gm.waterfallSound.Play();
		waterfallSoundPlaying = true;
		yield return new WaitForSeconds (1);
	}
	#endregion
}

