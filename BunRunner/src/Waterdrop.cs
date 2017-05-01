using UnityEngine;
using System.Collections;

//Waterdrop enemy / projectile class
public class Waterdrop : MonoBehaviour {

	#region Variables
	//Waterdrop variables
	public static GameMaster gm;
	public BunnyStats bunny;
	private int dropDamage = 50;
	public float cameraDropOffset = 4f;
	private float dropSpeed = 0.2f;
	public Transform dropBoundaryBottom;
	public float soundTimer = 1f;
	private bool dropSoundOneDone = false;
	private bool dropSoundTwoDone = false;

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

		//Drop starts moving at specified camera offset, drop speed is slowed the higher its y position is
		if (Camera.main.transform.position.x >= transform.position.x - cameraDropOffset) {
			Vector3 temp = transform.position;
			temp.y += dropSpeed ;
			dropSpeed -= 0.002f;
			transform.position = temp;
		}

		//Make drop bounce up and down between boundaries, reset sounds
		if (transform.position.y <= dropBoundaryBottom.position.y) {
			Destroy (this.gameObject);
		}

		//Make drop face up or down depending on drop speed
		Vector3 theScale = transform.localScale;
		if (dropSpeed >= 0) {
			theScale.y = -0.5f;
		} else if (dropSpeed < 0) {
			theScale.y = 0.5f;
		}
		transform.localScale = theScale;

		//Play sounds
		if (!dropSoundOneDone &&
		    transform.position.y >= -4.6) {
			dropSoundOneDone = true;
			gm.dropSoundOne.Play();
		}
		if (dropSoundOneDone &&
			!dropSoundTwoDone && 
		    transform.position.y <= -4.6) {
			dropSoundTwoDone = true;
			gm.dropSoundTwo.Play();
		}
	}
	#endregion

	#region Collisions
	//Hurt bunny if it touches the drop
	void OnCollisionEnter2D (Collision2D touchDrop) {
		if (touchDrop.gameObject.tag == "Player") {
			bunny.DamageBunny (dropDamage);
		}
	}
	#endregion
}
