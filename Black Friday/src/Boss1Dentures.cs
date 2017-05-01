using UnityEngine;
using System.Collections;

// Class represents the dentures projectile thrown by Boss1
public class Boss1Dentures : MonoBehaviour {

	// Dentures variables
	private static Gamemaster gm;
	private PlayerStats stats;
	private Rigidbody2D rb;
	private float denturesSpeed = 0.1f;
	private float denturesBoundary = -35.0f;
	private int denturesDamage = 20;

	// Get Gamemaster, PlayerStats
	private void Awake () {

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}

		// Get PlayerStats
		stats = GameObject.Find("Player").GetComponent<PlayerStats>();
	}

	// Dentures movement & rotation, Destroy offscreen
	private void Update () {
        
		// Only do the following while the game is not paused
		if (!gm.GetPause()) {

			// Get current position
	        Vector3 denturesPos = transform.position;

	        // Dentures move left at denturesSpeed
	        denturesPos.x -= denturesSpeed;

	        // Apply new position
	        transform.position = denturesPos;
			transform.Rotate(new Vector3(0f, 0f, 10.0f));

	        // Destroy dentures offscreen
	        if (denturesPos.x <= denturesBoundary) {
	            Destroy(this.gameObject);
	        }
		}
	}

	// If dentures hit player, call damagePlayer with denturesDamage
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			stats.damagePlayer(denturesDamage);
		}
	}
}
