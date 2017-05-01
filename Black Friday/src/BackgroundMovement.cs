using UnityEngine;
using System.Collections;

// Class controls background and creates parallax effect
public class BackgroundMovement : MonoBehaviour {

	private static Gamemaster gm;
	private float moveSpeed;            // Speed at which the BG moves
	private float posRight = -96.0f;    // Point at which element should be rset
	private float posReset;				// Point to which element should be reset
	private float BGTimer = 0.0f; 		// Timer used for moveTime
	private float moveTime = 4.0f; 		// Time at which BG starts moving

	// Set BG layer speeds, Get Gamemaster
	private void Start ()
    {
		// Set move speed & reset position for all background elements
		if (this.name == "Background Tile") {
			moveSpeed = 0.05f;
			posReset = 2 * 78.25f;
		}
		if (this.name == "Pillars Tile") {
			moveSpeed = 0.075f;
			posReset = 2 * 76.73f;
		}
		if (this.name == "Ground Tile") {
			moveSpeed = 0.1f;
			posReset = 2 * 78.34f;
		}

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}
	}

	// Move BG layers at different speeds to create a parallax effect
	private void Update () {

		// Only do the following while the game is not paused
		if (!gm.GetPause()) {

			// Start timer
			BGTimer += Time.deltaTime;

			// Move BG after starting sequence is over
			if (BGTimer >= moveTime) {

				// Get current position
				Vector3 pos = transform.position;

				// Move background to the left with moveSpeed
				pos.x -= moveSpeed;

				// Jump background tile to posLeft once posRight has been reached
				if (pos.x <= posRight) {
					pos.x += posReset;
				}

				// Apply new position
				transform.position = pos;
			}
		}
	}
}
