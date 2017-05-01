using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Class represents the game master object, which handles game flow throughout the game
public class Gamemaster : MonoBehaviour {
    
	#region Variables
    // Class fields
    public static Gamemaster gm;            // Declare instance of itself
	private static SFX sfx;					// Declare audiomaster
	private static string[] ownedWeapons = 
		new string[4];						// Stores all equippable weapons
	private static int score = 0;           // Keep track of player score
	private static int money = 0;			// Money the player collects from coins
	private static int currentLevel = 0;	// The current level (excludes menus)
	private static int playerLives = 0;     // Keep track of player lives

	// Gameflow control variables
	private bool gamePaused = false;        // Is the game paused or not?
	private float timer = 0.0f;             // How long has a level been running?

	// Enemy variables
	public GameObject enemyAPrefab;			// Enemy A prefab
	public GameObject enemyBPrefab;			// Enemy B prefab
	public GameObject enemyCPrefab;			// Enemy C prefab
	public GameObject enemyDPrefab;			// Enemy D prefab
	public GameObject enemyEPrefab;			// Enemy E prefab
	public GameObject enemyFPrefab;			// Enemy F prefab
	public GameObject enemyGPrefab;			// Enemy G prefab
	private bool enemySpawning = false;		// Is an enemy currently being spawned?
	private float enemySpawnX = -7.5f;		// Enemy spawn position X
	private float enemySpawnY;				// Enemy spawn position y
	private float enemySpawnTime = 6.0f;	// Enemies start spawning after this time
	private float enemySpawnLimit = 60.0f;	// Enemies won't spawn after this time
	private float enemySpawnDelay = 4.0f;	// How often are enemies spawned?

    // Player variables
	public Transform playerPrefab;          // Declare instance of the player prefab
	private PlayerStats player;             // Declare instance of the player (stats)
	private PlayerStats searchResult;       // Result of the search for the player
    private bool searchingForPlayer = false;// Is the player being searched for?
    private bool killingPlayer = false;     // Is the player currently being killed?

	// Countdown variables
	public GameObject CountdownPrefab; 		// Will be instantiated just after level start
	private Vector3 countdownPosition = 
		new Vector3 (-22.0f , 3.5f, 0.0f);	// Onscreen location of the countdown
	private bool countdownShowing = false; 	// Is the countdown currently showing?

	// Pickup variables
	public GameObject healthPickupPrefab;		// Health pickup
	private bool healthPickupSpawn = false; 	// Is a pickup being spawned?
	private float healthSpawnTime = 10.0f;		// Time at which health pickups start spawning
	private float healthStopTime = 60.0f;		// Time after which pickups shouldn't be spawned anymore
	private float healthPickupResetMax = 12.0f;	// How often will pickups be spawned?
	private float healthPickupResetMin = 6.0f;	// How often will pickups be spawned?
	public GameObject coinPickupPrefab;			// Coin pickup
	private bool coinPickupSpawn = false; 		// Is a pickup being spawned?
	private float coinSpawnTime = 5.0f;			// Time at which health pickups start spawning
	private float coinStopTime = 60.0f;			// Time after which pickups shouldn't be spawned anymore
	private float coinPickupResetMax = 6.0f;	// How often will pickups be spawned?
	private float coinPickupResetMin = 3.0f;	// How often will pickups be spawned?
	#endregion

	#region Methods
	// Get self (gamemaster), Audiomaster, Add spoon to owned weapons 
    private void Start () {

		// Get Gamemaster
        if (gm == null) {
            gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
        }

		// Get Audiomaster
		if (sfx == null) {
			sfx = GameObject.Find("Audiomaster").GetComponent<SFX>();
		}

		// Add default weapon spoon to owned weapons
		ownedWeapons[0] = "Spoon";
    }

	// Toggle pause, Show Countdown, Get player, Kill player, Spawn Enemies
    private void FixedUpdate () {

		if (SceneManager.GetActiveScene().name != "menu" && 
			SceneManager.GetActiveScene().name != "gameover" &&
			SceneManager.GetActiveScene().name != "shop") {

			// Toggle Pause with P (or UI Pause button)
			if (Input.GetKeyDown(KeyCode.P)) {
				TogglePause();
			}

			// Only do the following if the game is not paused
			if (!gamePaused) {

				// Start timer
		        timer += Time.deltaTime;
		        
				// Show countdown at level start
				if (timer >= 1.0f && 
					!countdownShowing &&
					SceneManager.GetActiveScene().buildIndex != 0) {

					// Set bool to false to only display countdown once
					countdownShowing = true;
					Instantiate (CountdownPrefab, countdownPosition, Quaternion.identity);
				}

		        // Start looking for player whenever is null
		        if (player == null &&
					searchingForPlayer == false) {
		            searchingForPlayer = true;
		            StartCoroutine (SearchPlayer());
		        }
		        
		        // Kill player if health is zero or below
		        if (player != null &&
					player.getHealth() <= 0) {

		            // Call KillPlayer only, if it isn't already in progress
		            if (!killingPlayer)
		            {
		                killingPlayer = true;
		                StartCoroutine (KillPlayer(player));
		            }
		        }

				// Spawn Enemies
				if (timer >= enemySpawnTime &&
					timer <= enemySpawnLimit &&
					!enemySpawning) {
					enemySpawning = true;
					StartCoroutine(SpawnEnemy());
				}

				// Spawn health pickups
				if (timer >= healthSpawnTime &&
					timer <= healthStopTime &&
					!healthPickupSpawn) {
					healthPickupSpawn = true;
					StartCoroutine(SpawnHealth());
				}

				// Spawn coin pickups
				if (timer >= coinSpawnTime &&
					timer <= coinStopTime &&
					!coinPickupSpawn) {
					coinPickupSpawn = true;
					StartCoroutine(SpawnCoin());
				}
			}
		}
    }

	// Reset timer, Track retry level
	private void OnLevelWasLoaded () {

		// Reset timer on each level
		timer = 0.0f;

		// Track the current level (increment on level load, decrement on death)
		if (SceneManager.GetActiveScene().name != "menu" &&
			SceneManager.GetActiveScene().name != "gameover" &&
			SceneManager.GetActiveScene().name != "shop") {
			currentLevel += 1;

	}
}

	// Load the shop
	public void LoadShop () {
		SceneManager.LoadScene("shop");
	}
	#endregion

	#region Getters & Setters
	// Accessors & Mutators for Gamemaster

	// Player Lives getter & setter
	public int GetLives () {
		return playerLives;
	}
	public void SetLives (int livesValue) {
		playerLives = livesValue;
	}

	// Pause getter & Setter
	public bool GetPause () {
		return gamePaused;
	}
	public void TogglePause () {
		if (gamePaused) {
			gamePaused = false;
		} else {
			gamePaused = true;
		}
	}

	// Timer Getter & Setter
	public float GetTimer () {
		return timer;
	}
	public void SetTimer (float timerValue) {
		timer = timerValue;
	}

	// Retry Getter & Setter
	public int GetCurrentLevel () {
		return currentLevel;
	}
	public void SetRetryLevel (int currentLevelIndex) {
		currentLevel = currentLevelIndex;
	}

	// Active weapons Getter &  Setter
	public string[] GetOwnedWeapons () {
		return ownedWeapons;
	}
	public void SetOwnedWeapons (string weapon, int weaponIndex) {
		ownedWeapons.SetValue(weapon, weaponIndex);
	}

	// Money Getter & Setter
	public int GetMoney () {
		return money;
	}
	public void SetMoney (int addedMoneyValue) {
		money += addedMoneyValue;
	}
	#endregion

	#region IEnumerators
	// Search for player method
	private IEnumerator SearchPlayer () {

		// Search for the player object
		player = GameObject.Find("Player").GetComponent<PlayerStats>();

		// After searching, wait a second before a new search can begin
		yield return new WaitForSeconds(1);
		searchingForPlayer = false;
	}

	// Kill player method
	private IEnumerator KillPlayer (PlayerStats player) {
			
		// Wait out death animation
		yield return new WaitForSeconds(3.0f);

		// Do this if player has lives left
		if (playerLives > 0) {

			// Take a life away, restart the current level, decrement currentLevel
			playerLives -= 1;
			currentLevel -= 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			killingPlayer = false;

		}

		// If player has no lives left, the game is over =(
		else {
			yield return new WaitForSeconds(2);
			SceneManager.LoadScene("gameover");
			killingPlayer = false;
		}
	}

	// Spawn enemy method
	private IEnumerator SpawnEnemy () {

		// Generate spawn position
		Vector3 spawnPos = 
			new Vector3 (enemySpawnX, Random.Range(-5.4f, 4.2f), 0f);

		// Choose & spawn a random enemy
		int enemyNum = Random.Range(0, 6);
		switch (enemyNum) {
			case 0:
				Instantiate (enemyAPrefab, spawnPos, Quaternion.identity);
				break;
			case 1:
				Instantiate (enemyBPrefab, spawnPos, Quaternion.identity);
				break;
			case 2:
				Instantiate (enemyCPrefab, spawnPos, Quaternion.identity);
				break;
			case 3:
				Instantiate (enemyDPrefab, spawnPos, Quaternion.identity);
				break;
			case 4:
				Instantiate (enemyEPrefab, spawnPos, Quaternion.identity);
				break;
			case 5:
				Instantiate (enemyFPrefab, spawnPos, Quaternion.identity);
				break;
			case 6:
				Instantiate (enemyGPrefab, spawnPos, Quaternion.identity);
				break;
		}

		// Wait, then allow a new enemy to be spawned
		yield return new WaitForSeconds(enemySpawnDelay);
		enemySpawning = false;
	}

	// Spawn health pickup method
	private IEnumerator SpawnHealth () {

		// Generate spawn position
		Vector3 spawnPos = 
			new Vector3 (enemySpawnX, Random.Range(-5.4f, 4.2f), 0f);

		// Instantiate the pickup
		Instantiate (healthPickupPrefab, spawnPos, Quaternion.identity);

		// Wait, then allow a new pickup to be spawned
		yield return new WaitForSeconds(Random.Range (healthPickupResetMin, healthPickupResetMax));
		healthPickupSpawn = false;
	}

	// Spawn health pickup method
	private IEnumerator SpawnCoin () {

		// Generate spawn position
		Vector3 spawnPos = 
			new Vector3 (enemySpawnX, Random.Range(-5.4f, 4.2f), 0f);

		// Instantiate the pickup
		Instantiate (coinPickupPrefab, spawnPos, Quaternion.identity);

		// Wait, then allow a new pickup to be spawned
		yield return new WaitForSeconds(Random.Range (coinPickupResetMin, coinPickupResetMax));
		coinPickupSpawn = false;
	}
	#endregion
}