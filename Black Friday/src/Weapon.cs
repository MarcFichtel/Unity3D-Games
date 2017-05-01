using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Weapon : MonoBehaviour {

	#region Variables
	// General variables
	private static Gamemaster gm;
	private static SFX sfx;
	private PlayerStats stats;

	// Spoon variables
	private float spoonPower = 10.0f;           // Spoon Damage
	private float spoonReload = 1.0f;			// Spoon Reload Speed
	private float spoonAnimation = 0.5f;		// Spoon Animation Length

	// Ball variables
	private float ballPower = 25.0f;			// Ball Damage
	private float ballReload = 1.5f;			// Ball Reload Speed
	private float ballAnimation = 0.5f;			// Ball Animation Length

	// Club variables
	private float clubPower = 50.0f;			// Club Damage
	private float clubReload = 2.0f;			// Club Reload Speed
	private float clubAnimation = 0.5f;			// Club Animation Length

	// Gun variables
	private float gunPower = 75.0f;				// Gun Damage
	private float gunReload = 1.0f;				// Gun Reload Speed
	private float gunAnimation = 0.5f;			// Gun Animation Length

	// Enemy variables
	private Boss1 boss1;						// First boss
	private int boss1Worth = 5;					// Money gained from defeating first boss
	private int boss2Worth = 5;					// Money gained from defeating second boss
	private int boss3Worth = 5;					// Money gained from defeating third boss
	private int boss4Worth = 5;					// Money gained from defeating fourth boss
	private int boss5Worth = 5;					// Money gained from defeating fifth boss
	private int enemyWorth;						// Money gained from defeating enemies (depends on level)
	#endregion

	#region Methods
	// Get Gamemaster, SFX, Player
	private void Start () {

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

		// Set enemyWorth depending on level
		int currentLevel = SceneManager.GetActiveScene().buildIndex;
		switch (currentLevel) {
		case 1:
			enemyWorth = 5;
			break;
		case 2:
			enemyWorth = 5;
			break;
		case 3:
			enemyWorth = 10;
			break;
		case 4:
			enemyWorth = 10;
			break;
		case 5:
			enemyWorth = 20;
			break;
		}
	}

	// If player hits something, damage it with attackPower
	private void OnTriggerEnter2D (Collider2D other) {

		// If player hits a boss, damage boss
		if (other.gameObject.tag == "Boss") {
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				other.gameObject.GetComponent<Boss1> ().damageBoss (stats.getWeaponStats () [0]);
			} else if (SceneManager.GetActiveScene ().buildIndex == 2) {
				other.gameObject.GetComponent<Boss2> ().damageBoss (stats.getWeaponStats () [0]);
			} 
//			else if (SceneManager.GetActiveScene ().buildIndex == 3) {
//				other.gameObject.GetComponent<Boss3> ().damageBoss (stats.getWeaponStats () [0]);
//			} else if (SceneManager.GetActiveScene ().buildIndex == 4) {
//				other.gameObject.GetComponent<Boss4> ().damageBoss (stats.getWeaponStats () [0]);
//			} else if (SceneManager.GetActiveScene ().buildIndex == 5) {
//				other.gameObject.GetComponent<Boss5> ().damageBoss (stats.getWeaponStats () [0]);
//			}
		}

		// If player hits enemy, get enemy, set it to defeated, and add money
		if (other.gameObject.tag == "Enemy") {
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			BoxCollider2D enemyCol = enemy.GetComponent<BoxCollider2D>();
			KillEnemy(enemy, enemyCol);
		}
	}

	// Method shows enemy defeated animation and destroys enemy object
	private void KillEnemy (Enemy enem, BoxCollider2D coll) {

		// Disable enemy's collider
		coll.enabled = false;

		// Set enemy status to defeated
		enem.SetDefeated(true);

		// Add player money
		gm.SetMoney(enemyWorth);

		// Reset enemy speed
		enem.SetSpeed(0f);

		// Play sound effect depending on enemy type, then destroy enemy object
		switch (enem.name) {
		case "Enemy A(Clone)":
			sfx.PlaySound(sfx.maleScream1);
			break;
		case "Enemy B(Clone)":
			sfx.PlaySound(sfx.maleScream2);
			break;
		case "Enemy C(Clone)":
			sfx.PlaySound(sfx.femaleScream1);
			break;
		case "Enemy D(Clone)":
			sfx.PlaySound(sfx.femaleScream2);
			break;
		case "Enemy E(Clone)":
			sfx.PlaySound(sfx.femaleScream3);
			break;
		case "Enemy F(Clone)":
			sfx.PlaySound(sfx.maleScream3);
			break;
		default:
			sfx.PlaySound(sfx.maleScream4);
			break;
		}
	}
	#endregion

	#region Getters & Setters
	// Getters (no setters required)
	public float[] GetSpoonStats () {
		return new float[3] {spoonPower, spoonReload, spoonAnimation};
	}
	public float[] GetBallStats () {
		return new float[3] {ballPower, ballReload, ballAnimation};
	}
	public float[] GetClubStats () {
		return new float[3] {clubPower, clubReload, clubAnimation};
	}
	public float[] GetGunStats () {
		return new float[3] {gunPower, gunReload, gunAnimation};
	}
	#endregion
}
