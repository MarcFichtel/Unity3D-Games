using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//The almighty Gamemaster class
public class GameMaster : MonoBehaviour {

	#region Variables
	//Gamemaster variables
	public static GameMaster gm;
	public static int Score = 0;
	public Transform spawnPoint;
	public float spawnDelay = 3f;
	[HideInInspector] public TrackEnd trackEnd;
	public bool isPaused = false;

	//Life variables
	public static int bunnyLives = 3;
	public Texture2D bunnyLifeImage;
	private bool killingBunny = false;
	public static int retryLevel;
	public Transform oneUpParticleEffectPrefab;

	//Bunny variables
	public Transform bunnyPrefab;
	public BunnyStats bunny;
	private bool searchingForBunny = false;
	private BunnyStats searchResult;
	public Transform deathParticles;
	
	//Bird & Boss 1 variables
	public Transform birdPrefab;
	private float birdSpawnTime;
	private float minBirdSpawnDelay = 8f;
	private float maxBirdSpawnDelay = 15f;
	private Vector3 birdSpawnPosition;
	private bool birdIsSpawing = false;
	public Transform pineconePrefab;

	//Pigeon variables
	public Transform pigeonPrefab;
	private float pigeonSpawnTime;
	private float minPigeonSpawnDelay = 10f;
	private float maxPigeonSpawnDelay = 16f;
	private Vector3 pigeonSpawnPosition;
	private bool pigeonIsSpawing = false;

	//Audio variables
	public bool jumpSoundNotPlaying = true;
	private AudioSource[] sounds;
	public AudioSource jumpSound;
	public AudioSource deathSound;
	public AudioSource carrotSound;
	public AudioSource oneupSound;
	public AudioSource waterfallSound;
	public AudioSource boss1zoomSound;
	public AudioSource pineconeHitSound;
	public AudioSource shroomJumpSound;
	public AudioSource stalagtiteSound;
	public AudioSource webSound;
	public AudioSource boss2DefeatSound;
	public AudioSource dropSoundOne;
	public AudioSource dropSoundTwo;
	public AudioSource switchSound;
	public AudioSource boss1DefeatSound;

	//Car variables
	public Transform carPrefab;
	private float carSpawnTime;
	private float minCarSpawnDelay = 10f;
	private float maxCarSpawnDelay = 12f;
	private Vector3 carSpawnPosition;
	private bool carIsSpawing = false;
	public bool carMovingRight;
	#endregion

	#region Start
	void Start () {

		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent <GameMaster>();
		}

		//Get track end
		if (Application.loadedLevel != 1 || 
			Application.loadedLevel != 4 || 
		    Application.loadedLevel != 10 ||
		    Application.loadedLevel != 11) {
			trackEnd = FindObjectOfType<TrackEnd> ();
		}

		//Get sounds
		sounds = GetComponents <AudioSource> ();
		jumpSound = sounds [0];
		carrotSound = sounds [1];
		deathSound = sounds [2];
		oneupSound = sounds [3];
		waterfallSound = sounds [4];
		boss1zoomSound = sounds [5];
		pineconeHitSound = sounds [6];
		shroomJumpSound = sounds [7];
		stalagtiteSound = sounds [8];
		webSound = sounds [9];
		boss2DefeatSound = sounds [10];
		dropSoundOne = sounds [11];
		dropSoundTwo = sounds [12];
		switchSound = sounds [13];
		boss1DefeatSound = sounds [14];
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate() {

		//Start SearchForBunny if bunny is null
		if (bunny == null) {
			if (!searchingForBunny) {
				searchingForBunny = true;
				StartCoroutine (SearchForBunny ());
			}
		}

		//Kill bunny if health is zero or below
		if (bunny != null &&
		    bunny.currentHealth <= 0) {
			if (!killingBunny) {
				killingBunny = true;
				StartCoroutine (KillBunny (bunny));
			}
		}

		//If score is 50, give extra life and reset score
		if (Score >= 50) {
			Score = 0;
			StartCoroutine(oneUp());
		}

		//Start spawnBird on forest levels, if its not already in progress
		if (!birdIsSpawing) {
			if (Application.loadedLevel == 2 ||
				Application.loadedLevel == 3) {
				birdIsSpawing = true;
				StartCoroutine (spawnBird ());
			}
		}

		//Start spawnPigeon on city levels, if its not already in progress
		if (!pigeonIsSpawing) {
			if (Application.loadedLevel == 8 ||
			    Application.loadedLevel == 9 ||
			    Application.loadedLevel == 10) {
				pigeonIsSpawing = true;
				StartCoroutine (spawnPigeon ());
			}
		}

		//Start spawnCar on city levels, if its not already in progress
		if (Application.loadedLevel == 8 ||
		    Application.loadedLevel == 9) {
			if (!carIsSpawing &&
				!trackEnd.goalReached) {
				carIsSpawing = true;
				StartCoroutine (spawnCar ());
			}
		}

		return;
	}
	#endregion

	#region KillBunny Method
	//Kill bunny method
	public IEnumerator KillBunny (BunnyStats bunny) {
		if (bunny != null) {

			//Create particle effect
			Transform clone = Instantiate (deathParticles, new Vector3 (bunny.transform.position.x + (float)5.5, bunny.transform.position.y + (float)0.9, 0), bunny.transform.rotation) as Transform;

			//Play death sound
			deathSound.Play();

			//Destroy bunny and particle effect
			Destroy (bunny.gameObject);
			Destroy (clone.gameObject, 2f);

			if (bunnyLives > 0) {

				//Subtract a life, wait 2 seconds, then restart the current level
				yield return new WaitForSeconds (2);
				bunnyLives -= 1;
				Application.LoadLevel(Application.loadedLevel);
				killingBunny = false;

			} else {

				//Game Over =(
				yield return new WaitForSeconds (2);
				Score = 0;
				retryLevel = Application.loadedLevel;
				Application.LoadLevel("Game Over");
				killingBunny = false;
			}
		}
	}
	#endregion

	#region SearchForBunny Method
	//Search for bunny
	IEnumerator SearchForBunny () {

		//Store search result
		searchResult = GameObject.FindObjectOfType <BunnyStats> ();

		//If no bunny was found, keep looking
		if (searchResult == null) {
			yield return new WaitForSeconds (1f);
			StartCoroutine (SearchForBunny ());

		} else {

			//If bunny was found, stop looking
			bunny = searchResult;
			searchingForBunny = false;
			return false;
		}
	}
	#endregion

	#region SpawnEnemy Methods
	//Spawn bird enemy
	public IEnumerator spawnBird () {

		//Set and wait for semi-random spawn time
		birdSpawnTime = Random.Range (minBirdSpawnDelay, maxBirdSpawnDelay);
		yield return new WaitForSeconds (birdSpawnTime);

		//Position bird right of camera and give it a random height
		birdSpawnPosition = new Vector3 (Camera.main.transform.position.x + 15f, Random.Range (-2.5f, 4f), 0);
		Instantiate (birdPrefab, birdSpawnPosition, Quaternion.identity);

		//Set bool to false to restart coroutine
		birdIsSpawing = false;
	}

	//Spawn pigeon enemy
	public IEnumerator spawnPigeon () {
		
		//Set and wait for semi-random spawn time
		pigeonSpawnTime = Random.Range (minPigeonSpawnDelay, maxPigeonSpawnDelay);
		yield return new WaitForSeconds (pigeonSpawnTime);
		
		//Position pigeon left of camera and give it a set height
		pigeonSpawnPosition = new Vector3 (Camera.main.transform.position.x - 15f, Camera.main.transform.position.y + 4f, 0);
		Instantiate (pigeonPrefab, pigeonSpawnPosition, Quaternion.identity);
		
		//Set bool to false to restart coroutine
		pigeonIsSpawing = false;
	}

	//Spawn car enemy
	public IEnumerator spawnCar () {

		//Do not spawn car after a certain point in the level
		if (bunny != null) {

			//Set and wait for semi-random spawn time
			carSpawnTime = Random.Range (minCarSpawnDelay, maxCarSpawnDelay);
			yield return new WaitForSeconds (carSpawnTime);

			//Position car left or right of camera set moving direction, and give it a set height
			int rand = Random.Range (0, 2);
			if (rand <= 0) {
				carSpawnPosition = new Vector3 (Camera.main.transform.position.x - 15f, -2.0f, 0);
				carMovingRight = true;
			} else if (rand >= 1) {
				carSpawnPosition = new Vector3 (Camera.main.transform.position.x + 15f, -2.0f, 0);
				carMovingRight = false;
			}

			//Spawn car at left or right position
			Instantiate (carPrefab, carSpawnPosition, Quaternion.identity);

			//Set bool to false to restart coroutine
			carIsSpawing = false;
		}
	}
	#endregion

	#region RespawnPinecones Method
	//Respawn boss 1 pinecones
	public IEnumerator respawnPinecone (Vector3 position) {
		yield return new WaitForSeconds (10f);
		Instantiate (pineconePrefab, position, Quaternion.identity);
		yield return null;
	}
	#endregion

	#region OneUp Method
	//Gain a life method
	public IEnumerator oneUp () {
		bunnyLives += 1;
		oneupSound.Play ();
		Transform clone = Instantiate (oneUpParticleEffectPrefab, new Vector3 (Camera.main.transform.position.x - 6.91f, Camera.main.transform.position.y + 3.84f, 0), Quaternion.identity) as Transform;
		clone.SetParent (Camera.main.transform);
		yield return new WaitForSeconds (3);
		Destroy (clone.gameObject);
	}
	#endregion
}
