using UnityEngine;
using System.Collections;

//Spikes class
public class Spikes : MonoBehaviour {

	#region Variables
	//Spikes variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public int spikesDamage = 9999;
	#endregion

	#region Start
	void Start () {

		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate() {

		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}
	#endregion

	#region Collisions
	//Kill bunny if it touches spikes
	void OnCollisionEnter2D (Collision2D touchSpikes) {
		if (touchSpikes.gameObject.tag == "Player") {
			bunny.DamageBunny (spikesDamage);
		}
	}
	#endregion
}