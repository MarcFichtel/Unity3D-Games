using UnityEngine;
using System.Collections;

// Class represents the coin pickup object
public class CoinPickup : MonoBehaviour {

	// Variables
	private static Gamemaster gm;		// Declare Gamemaster
	private static SFX sfx;				// Declare Audiomaster
	private PlayerStats stats;			// Declare player
	private UIMaster ui;				// Declare User Interface object
	private int moneyUp = 10;	 		// Pickup gives this much health
	private float speed = 0.1f;			// Pickup move speed
	private float boundary = -35.0f;	// Destroy offscreen boundary
	private Color pickupColor = 
		new Color (1.0f, 1.0f, 0f, 1.0f);// Money display brifly takes on this color on pickup

	// Get GM, AM, stats, UI
	private void Awake () {

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}

		// Get Audiomaster
		if (sfx == null) {
			sfx = GameObject.Find("Audiomaster").GetComponent<SFX>();
		}

		// Get PlayerStats
		stats = GameObject.Find("Player").GetComponent<PlayerStats>();

		// Get UI
		ui = GameObject.Find("Canvas: Game UI").GetComponent<UIMaster>();
	}

	// Movement, Destroy offscreen
	private void Update () {

		if (!gm.GetPause ()) {
			// Move pickup
			Vector3 temp = transform.position;
			temp.x -= speed;
			transform.position = temp;

			// Destroy offscreen
			if (temp.x <= boundary) {
				Destroy (this.gameObject);
			}
		}
	}

	// If pickup hits player, add money, destroy pickup
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			gm.SetMoney(moneyUp); 				// Increase health
			sfx.PlaySound(sfx.coinPickup); 		// Play pickup sound
			ui.SetMoneyBGColor(pickupColor); 	// Color money BG yellow
			Destroy(this.gameObject); 			// Destroy pickup
		}
	}
}
