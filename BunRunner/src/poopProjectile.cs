using UnityEngine;
using System.Collections;

//Pigeon Poop class
public class poopProjectile : MonoBehaviour {

	#region Variables
	//Poop variables
	public static GameMaster gm;
	public BunnyStats bunny;
	private int poopDamage = 50;
	private float poopSpeed = 0.08f;
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
	void Update () {
	
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Poop falls
		Vector3 temp = transform.position;
		temp.y -= poopSpeed;
		transform.position = temp;

		//Destroy stalagtite offscreen
		if (transform.position.y <= -10) {
			Destroy (this.gameObject);
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches the poop
	void OnCollisionEnter2D (Collision2D touchPoop) {
		if (touchPoop.gameObject.tag == "Player") {
			bunny.DamageBunny (poopDamage);
		}
	}
	#endregion
}
