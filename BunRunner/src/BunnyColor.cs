using UnityEngine;
using System.Collections;

//Bunny color class
public class BunnyColor : MonoBehaviour {

	#region Variables
	//Bunny color variables
	public static GameMaster gm;
	public BunnyStats bunny;
	private SpriteRenderer spriteRenderer;
	private Color bunnyColor;
	private Color hurtColor;
	private bool blinking = false;
	private float blinkTime = 0.01f;
	#endregion

	#region Start
	void Start () {

		//Get Gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}

		//Set spriteRenderer and colors
		spriteRenderer = GetComponent <SpriteRenderer> ();
		bunnyColor = new Color (1f, 1f, 1f, 1f);
		hurtColor = new Color (1f, 0, 0, 1f);
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

		//Start hurtFlicker if its not already in progress and bunny is hurt, else revert to normal color
		if (bunny != null &&
			bunny.isHurt == true &&
		    blinking == false) {
			blinking = true;
			StartCoroutine(hurtFlicker());
		} else {
			spriteRenderer.color = bunnyColor;
		}
	}
	#endregion

	#region Methods
	//Change color rapidly between hurtColor and bunnyColor
	private IEnumerator hurtFlicker () {
		spriteRenderer.color = hurtColor;
		yield return new WaitForSeconds (blinkTime);
		spriteRenderer.color = bunnyColor;
		yield return new WaitForSeconds (blinkTime);

		//Set blinking to false to let coroutine start anew or be done
		blinking = false;
	}
	#endregion
}
