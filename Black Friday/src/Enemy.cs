using UnityEngine;
using System.Collections;

// Class represents basic enemy objects
public class Enemy : MonoBehaviour {

	#region Variables
	// Variables
	private static Gamemaster gm;		// Declare Gamemaster
	private static SFX sfx;				// Declare Audiomaster
	private PlayerStats stats;			// Declare Player
	private Rigidbody2D rb;				// Declare Rigidbody2D component
	private Animator animator;			// Enemy animator
	private bool defeated;				// Is the enemy defeated?
	private int damage = 10; 			// Enemy damage
	private float speed;				// Enemy movement speed
	private float speedMin = 0.1f;		// Enemy movement speed min
	private float speedMax = 0.35f;		// Enemy movement speed max
	private float boundary = -35.0f; 	// Destroy offscreen boundary
	#endregion

	#region Methods
	// Get Gamemaster, PlayerStats, Set semi-random speed
	private void Awake () {

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}

		// Get PlayerStats
		stats = GameObject.Find("Player").GetComponent<PlayerStats>();

		// Set speed
		speed = Random.Range(speedMin, speedMax);

		// Get animator
		animator = GetComponent<Animator>();
	}
	
	// Movement, Destroy offscreen
	private void Update () {

		if (!gm.GetPause ()) {
			// Update animator
			animator.SetBool ("defeated", defeated);
			animator.speed = 1.0f;

			// Enemy moves
			Vector3 temp = transform.position;
			temp.x -= speed;
			transform.position = temp;

			// Destroy offscreen
			if (temp.x <= boundary) {
				Destroy (this.gameObject);
			}

			// Destroy when defeated
			if (defeated) {
				Destroy (this.gameObject, 1.5f);
			}
		} else {
			// Stop enemy animation while game is paused
			animator.speed = 0f;
		}
	}

	// If enemy hits player, call damagePlayer with damage
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			stats.damagePlayer(damage);
		}
	}
	#endregion

	#region Getters & Setters
	// Defeated Setter (no getter required)
	public void SetDefeated (bool defeatStatus) {
		defeated = defeatStatus;
	}

	// Speed Setter (no getter required)
	public void SetSpeed (float speedValue) {
		speed = speedValue;
	}
	#endregion
}
