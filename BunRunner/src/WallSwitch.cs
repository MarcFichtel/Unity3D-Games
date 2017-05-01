using UnityEngine;
using System.Collections;

//Detroy-Wall switches class
public class WallSwitch : MonoBehaviour {

	#region Variables
	//Wall switch variables
	public static GameMaster gm;
	private BunnyStats bunny;
	public GameObject Wall;
	public GameObject Switch;
	public bool switchPressed = false;
	public float pressedSwitchPos;
	public BoxCollider2D boxcol;
	#endregion

	#region Start
	void Start () {
	
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}

		//Set positions for pressed switch offsets
		if (this.gameObject.tag == "Switch1") {
			pressedSwitchPos = 1f;
		} else if (this.gameObject.tag == "Switch2") {
			pressedSwitchPos = 17.9f;
		} else if (this.gameObject.tag == "Switch3") {
			pressedSwitchPos = 6.12f;
		} else if (this.gameObject.tag == "Switch4") {
			pressedSwitchPos = 26.46f;
		}

		//Get collider for lower position if button was pressed
		boxcol = GetComponent<BoxCollider2D> ();
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate () {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Get current position
		Vector3 switchPos = Switch.transform.position;
		Vector2 boxcolOffset = boxcol.offset;

		//Move switch down a bit, if it was pressed
		if (switchPressed == true) {
			switchPos.y = pressedSwitchPos;
			boxcolOffset.y -= 0.45f;
		}

		//Apply new position
		Switch.transform.position = switchPos;
		boxcol.offset = boxcolOffset;
	}
	#endregion

	#region Collisions
	//Collision method
	void OnCollisionEnter2D (Collision2D touchSwitch) {

		//Destroy wall, if switch is pressed
		if (touchSwitch.gameObject.tag == "Player" &&
			switchPressed == false) {
			switchPressed = true;
			bunny.bunnyRB.velocity = bunny.enemyForce;
			Destroy (Wall);
			gm.switchSound.Play();
		}
	}
	#endregion
}
