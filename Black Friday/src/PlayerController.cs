using UnityEngine;
using System.Collections;

// Character Controller
public class PlayerController : MonoBehaviour
{
	private static Gamemaster gm;           // Initialize Gamemaster
	private PlayerStats stats;        		// Initialize PlayerStats

	// Clamping variables
	private Vector3 playerPos;				// Player's current position
    private float borderTop = 4.6f;			// Used for clamping
	private float borderBottom = -4.8f;     // Used for clamping
    private float borderLeft = -31.7f;      // Used for clamping
    private float borderRight = -26.5f;     // Used for clamping
	private float borderRightNew = -18.25f; // Used for clamping
	private float playerTimer = 0.0f;		// Used for clamping while gate is closed
	private float gateOpenTime = 4.0f;		// Used for clamping while gate is closed

	public GameObject Spoon;
	public GameObject Ball;
	public GameObject Club;
	public GameObject Gun;

	// Get gamemaster, playerstats
    void Start ()
    {
        // Get Gamemaster
        if (gm == null) {
            gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
        }

        // Get PlayerStats
        if (stats == null) {
            stats = GetComponent<PlayerStats>();
        }
    }

	// Movement Input, Attack Input
    void Update () {
		
		// Only do the following while the game is not paused
		if (!gm.GetPause()) {
			
			// Start the timer
			playerTimer += Time.deltaTime;

			#region Movement
			// Disable player controls when player is dead
			if (!stats.GetIsDead()) {
				// Reset right clamping edge after gate has opened
				if (playerTimer >= gateOpenTime) {
					borderRight = borderRightNew;
				}

				// Get current position
				playerPos = transform.position;

				// Move up
				if (transform.position.y <= borderTop) {
					if (Input.GetKey(KeyCode.W) ||
				    	Input.GetKey(KeyCode.UpArrow)) {
						playerPos.y += stats.playerSpeedVert;
					}
				}

				// Move down
				if (transform.position.y >= borderBottom) {
					if (Input.GetKey(KeyCode.S) ||
					    Input.GetKey(KeyCode.DownArrow)) {
						playerPos.y -= stats.playerSpeedVert;
					}
				}

				// Move right (forward)
				if (transform.position.x <= borderRight) {
					if (Input.GetKey(KeyCode.D) ||
					    Input.GetKey(KeyCode.RightArrow)) {
						playerPos.x += stats.playerSpeedForward;
					}
				}

				// Move left (backward)
				if (transform.position.x >= borderLeft) {
					if (Input.GetKey(KeyCode.A) ||
					    Input.GetKey(KeyCode.LeftArrow)) {
						playerPos.x += stats.playerSpeedBackward;
					}
				}

				// Is the player receiving movement input?
				stats.setMoving (Input.GetKey(KeyCode.W) ||
					Input.GetKey(KeyCode.A) ||
					Input.GetKey(KeyCode.S) ||
					Input.GetKey(KeyCode.D) ||
					Input.GetKey(KeyCode.UpArrow) ||
					Input.GetKey(KeyCode.DownArrow) ||
					Input.GetKey(KeyCode.LeftArrow) ||
					Input.GetKey(KeyCode.RightArrow));

				// Apply new position
				transform.position = playerPos;
			#endregion

			#region Attacking
		    // Attack, if Space or X is pressed
		    if (Input.GetKeyDown(KeyCode.Space) ||
				Input.GetKeyDown(KeyCode.X)) {
		            
				// Only one attack can be in progress at once
				if (!stats.getAttacking()) {
		                
					// Set attacking to true, and call attack coroutine from stats
					stats.setAttacking (true);
		            StartCoroutine(stats.playerAttack());
		        }
		    }

			// Switch weapons w/ numKeys 1-4, if their respective weapons are owned and not unequipped
			// Press 1: Equip Spoon
			if (Input.GetKeyDown(KeyCode.Keypad1) ||
				Input.GetKeyDown(KeyCode.Alpha1)) {
					stats.setWeapon (Spoon);
			}

			// Press 2: Equip Ball
			if ((Input.GetKeyDown(KeyCode.Keypad2) ||
				Input.GetKeyDown(KeyCode.Alpha2)) &&
				stats.getOwnedWeapons()[1] == "Ball") {
					stats.setWeapon (Ball);
			}

			// Press 3: Equip Club
			if ((Input.GetKeyDown(KeyCode.Keypad3) ||
				Input.GetKeyDown(KeyCode.Alpha3)) &&
				stats.getOwnedWeapons()[2] == "Club") {
					stats.setWeapon (Club);
			}

			// Press 4: Equip Gun
			if ((Input.GetKeyDown(KeyCode.Keypad4) ||
				Input.GetKeyDown(KeyCode.Alpha4)) &&
				stats.getOwnedWeapons()[3] == "Gun") {
					stats.setWeapon (Gun);
				}
			}
			#endregion
		}
	}
}
