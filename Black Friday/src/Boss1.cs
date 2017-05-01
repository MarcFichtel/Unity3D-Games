using UnityEngine;
using System.Collections;

// Class represents the first boss
public class Boss1 : MonoBehaviour {

	// Variables
	public Transform denturesSpawnPoint;    // Used to instantiate dentures
	public GameObject denturesPrefab;       // Dentures projectile prefab
	private static Gamemaster gm;           // Initialize Gamemaster
	private static SFX sfx;					// Declare audiomaster
	private Animator bossAnimator; 			// Boss animator
	private FadeInAndOut blackCurtain;		// Curtain for fading

	// Health
	private float boss1Health = 100.0f;     // Boss health
	private bool hurt = false;				// Is the boss hurt?
	private float hurtTime = 0.4f;			// Duration of boss hurt status
	private bool defeated = false;			// Is the boss dead?

	// Movement
    private float boss1Speed = 0.05f;       // Boss speed (vertical)
    private float borderHoriz = -16.0f;     // Used for entry
    private float borderTop = 4.2f;         // Used for clamping
    private float borderBottom = -5.4f;     // Used for clamping
    private float boss1EntrySpeed = 0.01f;  // Speed at which Boss enters
    private float boss1EntryTime = 60.0f;   // Time at which Boss enters
    private float boss1EntryDone = 70.0f;   // Time at which Boss is done entering
	private float moveDelay = 2.0f;         // Boss waits this long before moving again
	private bool moving = true;             // Is Boss currently moving or not?
	private bool entryDone = false;			// Is the boss done entering?

	// Attacking
    private float attackDelay = 2.0f;       // Boss waits this long before attacking again
    private bool attacking = false;         // Is Boss currently attacking or not?
	private bool attackDone; 				// Used for animator

	// Get Gamemaster, SFX, Animator, Curtain
	private void Start () {
        
		// Get Gamemaster
        if (gm == null) {
            gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
        }

		// Get Audiomaster
		if (sfx == null) {
			sfx = GameObject.Find("Audiomaster").GetComponent<SFX>();
		}

		// Get Animator
		bossAnimator = GetComponent<Animator>();

		// Get fading curtain
		blackCurtain = GameObject.Find("Black Curtain").GetComponent<FadeInAndOut>();
    }

	// Boss1 Entry, Movement, Attacking
	private void Update () {
        
		// Only do the following while the game is not paused
		if (!gm.GetPause() &&
			!defeated) {

			// Update animator bools
			bossAnimator.SetBool("boss1_attacking", attackDone);
			bossAnimator.SetBool("boss1_hurt", hurt);
			bossAnimator.SetBool("boss1_dead", defeated);

			// Set defeated to true, if helath falls to or below zero
			if (boss1Health <= 0) {
				defeated = true;
			}

			// Get current position
	        Vector3 bossPos = transform.position;

	        // Boss enters after specified delay
			if (gm.GetTimer() >= boss1EntryTime) {

				// Boss enters until borderHoriz has been reached
	            if (bossPos.x >= borderHoriz) {
	                bossPos.x -= boss1EntrySpeed;
				} else {

					// Set entryDone to true, if it is false (used for accessor method)
					if (!entryDone) {
						entryDone = true;
					}

	            	// Once boss has reached borderHoriz, start vertical movement
	                bossPos.y += boss1Speed;
	            }
	        }

	        // Reverse boss1Speed at clamping borders
	        if (bossPos.y >= borderTop ||
	            bossPos.y <= borderBottom) {
	            boss1Speed *= -1;
	        }

	        // Apply new position unless Boss is recovering from attacking
	        if (moving &&
				gm.GetTimer() >= boss1EntryTime)
	            transform.position = bossPos;

	        // Start attack, if one is not already in progress
	        if (!attacking &&
				gm.GetTimer() >= boss1EntryDone) {
	            
				// Set attacking to true to indicate an attach is in-progress
	            attacking = true;

	            // Boss stops moving just after an attack
	            moving = false;

	            // Call boss1Attack
	            StartCoroutine(boss1Attack());
	        }
		}

		// Call boss defeat method when boss is dead
		if (defeated) {
			StartCoroutine(boss1Defeat());
		}
	}

	// Damage boss if boss is not currently hurt or attacking
	public void damageBoss (float damage) {
		if (!hurt) {
			boss1Health -= damage;
			StartCoroutine(bossHurt());
		}
	}

	// Wait and reset hurt bool
	private IEnumerator bossHurt () {
		hurt = true;
		yield return new WaitForSeconds(hurtTime);
		hurt = false;
	}

	// Boss attack function
	private IEnumerator boss1Attack () {

		// Create dentures at their specified spawn location
		Instantiate(denturesPrefab, denturesSpawnPoint.position, Quaternion.identity);

		// Boss 1 attack sound effect
		sfx.PlaySound(sfx.boss1Attack);

		// Set attackDone to false (used for animator)
		attackDone = false;

		// After moveDelay, Boss moves again
		yield return new WaitForSeconds(moveDelay);
		moving = true;

		// Aftter attackDelay, Boss attacks again
		yield return new WaitForSeconds(attackDelay);
		attacking = false;

		// Set attackDone to true (used for animator)
		attackDone = true;
	}

	// Boss defeated method loads shop
	private IEnumerator boss1Defeat () {
		blackCurtain.StartFadeOut();
		yield return new WaitForSeconds(1);
		gm.LoadShop();
	}

	// Boss 1 Getters (no setters required)
	#region Boss1 Getters

	// Boss Health Getter (no setter required)
	public float GetBoss1Health () {
		return boss1Health;
	}

	// Boss Entry Getter (no setter required)
	public bool GetBoss1EntryDone () {
		return entryDone;
	}

	// Boss defeated Getter (no setter required)
	public bool GetBoss1Defeated () {
		return defeated;
	}
	#endregion
}
