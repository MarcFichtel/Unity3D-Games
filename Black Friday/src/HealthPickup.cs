using UnityEngine;
using System.Collections;

// Class represents the health pickup object
public class HealthPickup : MonoBehaviour {

	// Variables
	private static Gamemaster gm;		// Declare Gamemaster
	private static SFX sfx;				// Declare Audiomaster
	private PlayerStats stats;			// Declare player
	private UIMaster ui;				// Declare User Interface object
	private float healthMax = 100.0f;	// Maximum health allowed
	private float healthUp = 10.0f; 	// Pickup gives this much health
	private float speed = 0.1f;			// Pickup move speed
	private float boundary = -35.0f;	// Destroy offscreen boundary
	private Color pickupColor = 
		new Color (0f, 0.75f, 0f, 1.0f);// Health display brifly takes on this color on pickup

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

	// If pickup hits player, add health (max. 100), destroy health pickup
	private void OnTriggerEnter2D (Collider2D other) {

		// If player has at most 90 health, increase it by healthUp
		if (other.gameObject.tag == "Player" &&
			stats.getHealth() <= 90) {
			float playerHealth = stats.getHealth(); 	// Get player health
			stats.setHealth(playerHealth + healthUp); 	// Increase health
			sfx.PlaySound(sfx.healthPickup); 			// Play pickup sound
			ui.SetHealthBGColor(pickupColor); 			// Color health BG red
			Destroy(this.gameObject); 					// Destroy pickup

		// If player has 90 or more health, it becomes 100 on pickup
		} else if (other.gameObject.tag == "Player") {
			stats.setHealth(healthMax);					// Max health
			sfx.PlaySound(sfx.healthPickup);			// Play pickup sound
			ui.SetHealthBGColor(pickupColor);			// Color health BG red
			Destroy(this.gameObject);					// Destroy pickup
		}
	}
}
