using UnityEngine;
using System.Collections;

// Class represents the knife projectile thrown by Boss2
public class Boss2Knife : MonoBehaviour {

	// Knife variables
	private static Gamemaster gm;
	private PlayerStats stats;
	private Rigidbody2D rb;
	private float knifeSpeed = 0.15f;
	private float knifeBoundary = -35.0f;
	private int knifeDamage = 25;

	// Get GM, player
	private void Awake () {

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}

		// Get PlayerStats
		stats = GameObject.Find("Player").GetComponent<PlayerStats>();
	}

	// Movement, Destroy offscreen
	private void Update () {

		// Only do the following while the game is not paused
		if (!gm.GetPause()) {

			// Get current position
			Vector3 knifePos = transform.position;

			// Dentures move left at denturesSpeed
			knifePos.x -= knifeSpeed;

			// Apply new position
			transform.position = knifePos;

			// Destroy dentures offscreen
			if (knifePos.x <= knifeBoundary) {
				Destroy(this.gameObject);
			}
		}
	}

	// If knife hit player, damage player
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			stats.damagePlayer(knifeDamage);
		}
	}
}
