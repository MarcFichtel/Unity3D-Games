using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

	// General variables
	private static Gamemaster gm;
	private GameObject gateLeft;
	private GameObject gateRight;
	private float gateTimer = 0.0f;
	private float moveSpeed = 0.01f;
	private float startMoveTime = 4.0f;

	//Fadeout variables
	private SpriteRenderer gateLeftSprite;
	private SpriteRenderer gateRightSprite;
	private float alpha = 1.0f;
	private float fadeTime = 5.0f;
	private float fadeSpeed = 0.02f;

	// Get gate parts, gamemaster
	void Start () {

		// Get gate parts
		gateLeft = GameObject.Find("Gate Left");
		gateLeftSprite = GameObject.Find("Gate Left").GetComponent<SpriteRenderer>();
		gateRight = GameObject.Find("Gate Right");
		gateRightSprite = GameObject.Find("Gate Right").GetComponent<SpriteRenderer>();

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}
	}

	// Gate movement, Fadeout, Destroy once invisible
	void Update () {
	
		// Only do the following if the game is not paused
		if (!gm.GetPause()) {
			
			// Start the timer
			gateTimer += Time.deltaTime;

			// Get positions of gate parts
			Vector3 tempLeft = gateLeft.transform.position;
			Vector3 tempRight = gateRight.transform.position;

			// Gate slowly opens after a short time
			if (gateTimer >= startMoveTime) {
				tempLeft.x -= moveSpeed;
				tempRight.x += moveSpeed;

				// Apply new position
				gateLeft.transform.position = tempLeft;
				gateRight.transform.position = tempRight;
			}

			// Gate fades out after opening
			if (gateTimer >= fadeTime) {
				alpha -= fadeSpeed;
				gateLeftSprite.color = new Color (1f, 1f, 1f, alpha);
				gateRightSprite.color = new Color (1f, 1f, 1f, alpha);
			}

			// Destroy gate object after it has disappeared
			if (gateTimer >= 10.0f) {
				Destroy(this.gameObject);
			}
		}
	}
}
