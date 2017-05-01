using UnityEngine;
using System.Collections;

//Bunny class (not just the stats)
public class BunnyStats : MonoBehaviour {

	#region Variables
	//Gamemaster
	public static GameMaster gm;

	//Stats
	public int currentHealth = 100;
	public int carrotScore = 1;
	private int fallBoundary = -6;
	
	//Bunny clamping variables
	public float leftBorder;
	public float rightBorder;
	public float horizontalCameraExtent;

	//isHurt variables
	public bool isHurt = false;
	public float hurtTimer = 5f;
	[HideInInspector] public Rigidbody2D bunnyRB;
	[HideInInspector] public Vector2 enemyForce;

	//Animator variables
	private Animator bunnyAnimator;
	public bool grounded;

	//Cave variables
	[HideInInspector] public Vector2 mushroomForce;
	#endregion

	#region Start
	void Start () {

		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}

		//Get bunny's Rigidbody2D & set enemyForce for damage pushback
		bunnyRB = GetComponent<Rigidbody2D> ();
		enemyForce = new Vector2 (0, (float)Random.Range(10, 15));

		//Get animator
		bunnyAnimator = GetComponent <Animator> ();

		//Set mushroom jump force
		mushroomForce = new Vector2 (0, 20);
	}
	#endregion
	
	#region Update
	void Update () {

		//Get 'Ground' bool from animator
		grounded = bunnyAnimator.GetBool ("Ground");

		//Kill bunny if it falls too far
		if (transform.position.y <= fallBoundary) {
			isHurt = false;
			DamageBunny(9999);
		}

		//On Cave 2 and City 1, kill bunny if its too far above or below camera
		if (Application.loadedLevel == 6 ||
		    Application.loadedLevel == 8) {
			if (transform.position.y > Camera.main.transform.position.y + 10 ||
			    transform.position.y < Camera.main.transform.position.y - 7.5)
				currentHealth = 0;
		}

		//Play jump sound only once per jump
		if (!grounded && 
			gm.jumpSoundNotPlaying && 
		    bunnyRB.velocity.y > 0 &&
		    currentHealth > 0) {
				gm.jumpSoundNotPlaying = false;
				gm.jumpSound.Play();
		}

		//Allow jump sound if the bunny is grounded
		if (grounded) {
			gm.jumpSoundNotPlaying = true;
		}

		//Create hurt timer
		hurtTimer -= Time.deltaTime;
		if (hurtTimer <= 0) {
			isHurt = false;
		}

		#region Clamping
		// Calculate horizontal extent of camera, and clamp bunny within it
		Vector3 bunnyPos = transform.position;

		//Increase horizontal extent for spider boss
		if (Application.loadedLevel != 7) {
			horizontalCameraExtent = Camera.main.orthographicSize * (Screen.width / Screen.height) + 2.5f;
		} else {
			horizontalCameraExtent = Camera.main.orthographicSize * (Screen.width / Screen.height) + 3.7f;
		}
		leftBorder = Camera.main.transform.position.x - horizontalCameraExtent;
		rightBorder = Camera.main.transform.position.x + horizontalCameraExtent;
		bunnyPos.x = Mathf.Clamp (transform.position.x, leftBorder, rightBorder);
		transform.position = bunnyPos;
		#endregion
	}
	#endregion

	#region Collisions
	void OnCollisionEnter2D (Collision2D bunnyCollisions) {
		
		//Collect carrots and score carrot points
		if (bunnyCollisions.gameObject.tag == "Collect") {
			Destroy(bunnyCollisions.gameObject);
			gm.carrotSound.Play();
			GameMaster.Score += carrotScore;
		}
		
		//Bunny bounces on mushrooms
		if (bunnyCollisions.gameObject.tag == "Mushroom") {
			gm.shroomJumpSound.Play();
			bunnyRB.velocity = mushroomForce;
		}
	}
	#endregion

	#region Methods
	//Damage bunny, reset hurt timer, and apply damage pushback, if the goal hasn't already been reached
	public void DamageBunny(int damage) {
		if (!isHurt) {
			isHurt = true;
			hurtTimer = 5f;
			currentHealth -= damage;
			bunnyRB.velocity = enemyForce;
		}
	}
	#endregion
}