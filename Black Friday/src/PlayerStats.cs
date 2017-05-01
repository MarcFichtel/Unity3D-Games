using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	#region Variables
	// General variables
	private static Gamemaster gm;               // Gamemaster
	private static SFX sfx;						// Declare audiomaster
	private Animator playerAnimator; 			// Animator component used for animation transitions
	private UIMaster ui;						// User Interface
	private Color hurtColor = 
		new Color (0.75f, 0f, 0f, 1.0f);		// Health BG briefly takes on this color on getting hit

	// Movement vraiables
	private float playerHealthFloat = 100.0f;   // Player health
	private int playerHealthInt;   				// Player health
	public float playerSpeedVert = 0.15f;       // Speed at which player moves up and down
	public float playerSpeedForward = 0.2f;     // Speed at which player moves right
	public float playerSpeedBackward = -0.1f;   // Speed at which player moves left
	private bool moving = false;				// Is the player moving?
	private bool isDead = false;				// Is the player dead?

	// Weapon variables
	public GameObject[] allWeapons = new GameObject[4]; 	// Stores all weapons
	private string[] ownedWeaponsStrings = new string[4];	// Stores all equippable weapons as strings
	private GameObject activeWeapon;			// Stores currently equipped weapon
	private GameObject Spoon;					// Spoon
	private GameObject Ball;					// Ball
	private GameObject Club;					// Club
	private GameObject Gun;						// Gun

	// Attack variables
	private bool attacking = false;				// Is the player currently attacking?
	private float attackPower;					// Player attacks deal this much damage
	private float attackReload;					// Attack Reload speed
	private float attackAnimation;				// Duration of attack animation
	#endregion

	#region Methods
	// GM, AM, UI, Animator, Weapons, Bosses
	void Start() {

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}

		// Get Audiomaster
		if (sfx == null) {
			sfx = GameObject.Find("Audiomaster").GetComponent<SFX>();
		}

		// Get UI
		ui = GameObject.Find("Canvas: Game UI").GetComponent<UIMaster>();

		// Get Animator
		playerAnimator = GetComponent<Animator>();

		// Get weapon objects
		Spoon = GameObject.Find("Weapon: Spoon");
		Ball = GameObject.Find("Weapon: Ball");
		Club = GameObject.Find("Weapon: Club");
		Gun = GameObject.Find("Weapon: Gun");

		// Add weapons to their respective lists
		allWeapons = new GameObject[] {Spoon, Ball, Club, Gun};

		// If player has no weapon equipped, set it to default (spoon)
		if (activeWeapon == null) {
			activeWeapon = Spoon;
			float[] activeWeaponStats = Spoon.GetComponent<Weapon>().GetSpoonStats();
			attackPower = activeWeaponStats[0];
			attackReload = activeWeaponStats[1];
			attackAnimation = activeWeaponStats[2];
		}

		// Get equippable weapons
		ownedWeaponsStrings = gm.GetOwnedWeapons();
	}

	// Animation states, Weapon states
	void Update() {

		// Only do the following while the game is not paused
		if (!gm.GetPause()) {
			
			// Update animator bool for moving depending on whether input is received
			playerAnimator.SetBool("moving?", moving);

			// Update animator int for player health
			playerHealthInt = (int)playerHealthFloat;
			playerAnimator.SetInteger("health", playerHealthInt);

			// Update animator string depending on the currently equipped weapon
			if (activeWeapon == Spoon &&
				playerAnimator.GetInteger("weapon") != 1) {
				playerAnimator.SetInteger("weapon", 1);
			}
			if (activeWeapon == Ball && 
				playerAnimator.GetInteger("weapon") != 2) {
				playerAnimator.SetInteger("weapon", 2);
			}
			if (activeWeapon == Club && 
				playerAnimator.GetInteger("weapon") != 3) {
				playerAnimator.SetInteger("weapon", 3);
			}
			if (activeWeapon == Gun && 
				playerAnimator.GetInteger("weapon") != 4) {
				playerAnimator.SetInteger("weapon", 4);
			}

			// Disable weapons while player is not attacking
			if (!attacking) {
				for (int i = 0; i <= 3; i++) {
					allWeapons[i].SetActive(false);
				}
			}

			// Set bool isDead to true if health falls to 0
			if (playerHealthFloat <= 0) {
				isDead = true;
			}
		}
	}

	// Damage player function
	public void damagePlayer(float damage) {
		playerHealthFloat -= damage;
		ui.SetHealthBGColor(hurtColor);
		sfx.PlaySound(sfx.maleScream6);
	}
	#endregion

	#region Getters & Setters
	// Accessors & Mutators for player stats

	// Getter & Setter for health int
	public float getHealth () {
		return playerHealthFloat;
	}
	public void setHealth (float healthValue) {
		playerHealthFloat = healthValue;
	}

	// Getter & Setter for moving bool
	public bool getMoving () {
		return moving;
	}
	public void setMoving (bool movingValue) {
		moving = movingValue;
	}

	// Getter & Setter for attacking bool
	public bool getAttacking () {
		return attacking;
	}
	public void setAttacking (bool attackingValue) {
		attacking = attackingValue;
	}

	// Getters & Setter for activeWeapon
	public GameObject getWeapon () {
		return activeWeapon;
	}
	public float[] getWeaponStats () {
		return new float[3] {attackPower, attackReload, attackAnimation};
	}
	public void setWeapon (GameObject weaponValue) {
		if (weaponValue == Spoon) {
			activeWeapon = Spoon;
			float[] activeWeaponStats = Spoon.GetComponent<Weapon>().GetSpoonStats();
			attackPower = activeWeaponStats[0];
			attackReload = activeWeaponStats[1];
			attackAnimation = activeWeaponStats[2];
			ui.SetEquippedWeaponImage("Spoon");
		}
		else if (weaponValue == Ball) {
			activeWeapon = Ball;
			float[] activeWeaponStats = Ball.GetComponent<Weapon>().GetSpoonStats();
			attackPower = activeWeaponStats[0];
			attackReload = activeWeaponStats[1];
			attackAnimation = activeWeaponStats[2];
			ui.SetEquippedWeaponImage("Ball");
		}
		else if (weaponValue == Club) {
			activeWeapon = Club;
			float[] activeWeaponStats = Club.GetComponent<Weapon>().GetSpoonStats();
			attackPower = activeWeaponStats[0];
			attackReload = activeWeaponStats[1];
			attackAnimation = activeWeaponStats[2];
			ui.SetEquippedWeaponImage("Club");
		}
		else if (weaponValue == Gun) {
			activeWeapon = Gun;
			float[] activeWeaponStats = Gun.GetComponent<Weapon>().GetSpoonStats();
			attackPower = activeWeaponStats[0];
			attackReload = activeWeaponStats[1];
			attackAnimation = activeWeaponStats[2];
			ui.SetEquippedWeaponImage("Gun");
		}
	}

	// Getter & Setter for isDead bool
	public bool GetIsDead () {
		return isDead;
	}
	public void SetIsDead (bool isDeadValue) {
		isDead = isDeadValue;
	}

	// Getter & Setter for equippable weapons array of game objects
	// SetOwnedWeapons can only add a new weapon, since removing one is not allowed
	public string[] getOwnedWeapons () {
		return ownedWeaponsStrings;
	}
	public void setOwnedWeapons (GameObject newWeapon, int weaponIndex) {
		ownedWeaponsStrings.SetValue(newWeapon, weaponIndex);
	}
	#endregion

	#region IEnumerators
    // Player attack function
    public IEnumerator playerAttack() {

		// Set animator property and bool to true
		playerAnimator.SetBool("attacking?", true);
		attacking = true;
		activeWeapon.SetActive(true);

		// Play weapon sound when attacking
		if (activeWeapon == Spoon) {
			sfx.PlaySound(sfx.weaponSpoon);
		} else if (activeWeapon == Ball) {
			sfx.PlaySound(sfx.weaponBall);
		} else if (activeWeapon == Club) {
			sfx.PlaySound(sfx.weaponClub);
		} else if (activeWeapon == Gun) {
			sfx.PlaySound(sfx.weaponGun);
		}

        // Wait till attack animation has played out
		yield return new WaitForSeconds(attackAnimation);
		playerAnimator.SetBool("attacking?", false);
		activeWeapon.SetActive(false);

		// Wait till attack has been reloaded
		yield return new WaitForSeconds(attackReload);
		attacking = false;
    }
	#endregion
}
