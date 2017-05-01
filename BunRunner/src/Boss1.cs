using UnityEngine;
using System.Collections;

//Boss 1 class
public class Boss1 : MonoBehaviour {

	#region Variables
	//Boss 1 variables
	public static GameMaster gm;
	private BunnyStats bunny;

	//Stats variables
	public int bossHealth = 99;
	private int bossDamage = 50;
	private float bossSpeedHover = 1.75f;
	private float bossSpeedZoom = 0.5f;
	private bool hovering = true;
	
	//Boss isHurt variables
	private SpriteRenderer spriteRenderer;
	private Color bossColor;
	private Color bossHurtColor;
	private bool blinking = false;
	private float blinkTime = 0.01f;
	private bool bossIsHurt = false;
	private float bossHurtTimer = 3f;
	private Rigidbody2D bossRB;

	//Pinecone variables
	private Vector2 pineconeForce;
	private int pineconeDamage = 33;

	//Animator variables
	private Animator bossAnimator;
	private float lastX;
	private float currentX;
	
	//Collider variables
	private BoxCollider2D boxCol;
	private CircleCollider2D cirCol;

	//Boss defeated variables
	private float fadeTime = 3f;
	public Texture2D blackTexture;
	private float alpha = 0f;
	public bool bossDefeated = false;
	#endregion

	#region Start
	void Start () {

		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}

		//Get rigidbody
		bossRB = GetComponent <Rigidbody2D> (); 

		//Set pinecone force
		pineconeForce = new Vector2 (0, 5f);

		//Set spriteRenderer and colors
		spriteRenderer = GetComponentInChildren <SpriteRenderer> ();
		bossColor = new Color (1f, 1f, 1f, 1f);
		bossHurtColor = new Color (1f, 0, 0, 1f);

		//Get animator
		bossAnimator = GetComponent <Animator> ();

		//Get colliders
		boxCol = GetComponent <BoxCollider2D> ();
		cirCol = GetComponent <CircleCollider2D> ();
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate() {

		//Set last X position to current X position at the end of fixed update
		lastX = currentX;

		//Black fadeout if boss is defeated
		if (bossDefeated == true) {
			alpha += Time.deltaTime / fadeTime;
		}
	}
	#endregion

	#region Update
	void Update () {

		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Create boss hurt timer
		bossHurtTimer -= Time.deltaTime;
		if (bossHurtTimer <= 0) {
			bossIsHurt = false;
		}

		//Get boss position and scale
		Vector3 bossPos = transform.position;
		Vector3 bossScale = transform.localScale;

		//Get current X position
		currentX = bossPos.x;

		//Flip the sprire's X scale if the last X position is bigger than the current one
		if (currentX > lastX) {
			bossScale.x = 1;
		} else if (lastX > currentX) {
			bossScale.x = -1;
		}
		transform.localScale = bossScale;

		//Create hovering behaviour
		if (hovering == true &&
		    bossHealth > 0) {

			//Set animator and colliders
			bossAnimator.SetBool ("hover", true);
			boxCol.enabled = false;
			cirCol.enabled = true;

			//Make bird chase the bunny
			if (bunny != null) {
				transform.position = Vector2.MoveTowards (bossPos, bunny.transform.position, bossSpeedHover * Time.deltaTime);
			}

		//Create zooming behaviour
		} else if (bossHealth > 0) {

			//Set animator
			bossAnimator.SetBool ("hover", false);

			//Switch colliders
			boxCol.enabled = true;
			cirCol.enabled = false;

			//Move boss at zoom speed
			transform.position += new Vector3 (bossSpeedZoom, 0, 0);

			//Turn boss around if it flies too far, its heights is either top or bottom level
			if (transform.position.x >= 50) {
				bossSpeedZoom *= -1;
				gm.boss1zoomSound.Play();
				transform.position = new Vector3 (50, Random.value < 0.5f ? 3 : -3);
			}
			if (transform.position.x <= -50) {
				bossSpeedZoom *= -1;
				gm.boss1zoomSound.Play();
				transform.position = new Vector3 (-50, Random.value < 0.5f ? 3 : -3);
			}

		}

		//Create boss defeat behaviour
		if (bossHealth <= 0 &&
		    !bossDefeated) {
			bossDefeated = true;
			StartCoroutine (levelComplete ());
		}

		//Start hurtFlicker if its not already in progress and boss is hurt, else revert to normal color
		if (bossIsHurt == true &&
		    blinking == false) {
			blinking = true;
			StartCoroutine(hurtFlicker());
		} else {
			spriteRenderer.color = bossColor;
		}
	}
	#endregion

	#region Collisions
	//Collision method
	void OnCollisionEnter2D (Collision2D touchBoss1) {

		//Hurt bunny if it touches the boss
		if (touchBoss1.gameObject.tag == "Player") {
			bunny.DamageBunny (bossDamage);
		}

		//Hurt boss if its hit, apply pinecone force, start zoomEnd timer, and play pinecone hit sound
		if (touchBoss1.gameObject.tag == "Pinecone" && 
		    bossIsHurt == false && 
		    hovering == true &&
		    touchBoss1.gameObject.name == "Pinecone (falling)") {
			DamageBoss (pineconeDamage);
			Destroy(touchBoss1.gameObject);
			StartCoroutine(applyPineconeForce());
			StartCoroutine(zoomEnd());
			hovering = false;
			gm.pineconeHitSound.Play();
		}
	}
	#endregion

	#region Methods
	//Damage boss and reset hurt timer
	public void DamageBoss(int damage) {
		if (bossIsHurt == false) {
			bossIsHurt = true;
			bossHurtTimer = 3f;
			bossHealth -= damage;
		}
	}

	//Change color rapidly between birdHurtColor and birdColor
	private IEnumerator hurtFlicker () {
		spriteRenderer.color = bossHurtColor;
		yield return new WaitForSeconds (blinkTime);
		spriteRenderer.color = bossColor;
		yield return new WaitForSeconds (blinkTime);
		
		//Set blinking to false to let coroutine start anew or be done
		blinking = false;
	}

	//Push boss down for a few seconds after getting hit
	private IEnumerator applyPineconeForce () {
		bossRB.velocity -= pineconeForce;
		yield return new WaitForSeconds (2f);
		bossRB.velocity += pineconeForce;
	}

	//Go back to hovering after a semi-random duration, increase boss hover speed slightly, and reset hurt timer
	private IEnumerator zoomEnd () {;
		yield return new WaitForSeconds (Random.Range(10, 15));
		hovering = true;
		bossSpeedHover += 0.2f;
		bossHurtTimer = 5f;
		bossIsHurt = true;
	}

	//Set fade color and rectangle with black texture
	void OnGUI () {
		GUI.color = new Color (1f, 1f, 1f, alpha);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), blackTexture);
	}
	
	//On boss defeat, load next level after fade out time
	private IEnumerator levelComplete () {
		gm.boss1DefeatSound.Play ();
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	#endregion
}
