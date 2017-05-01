using UnityEngine;
using System.Collections;

//Boss 3 class
public class Boss3 : MonoBehaviour {

	#region Variables
	//Boss 3 variables
	public static GameMaster gm;
	private BunnyStats bunny;

	//Stats variables
	private int bossDamage = 50;
	public int bossHealth = 100;
	public float bossSpeed;
	public bool attacking = false;

	//Boss isHurt variables
	private SpriteRenderer spriteRenderer;
	private Color bossColor;
	private Color bossHurtColor;
	private bool blinking = false;
	private float blinkTime = 0.01f;
	private bool bossIsHurt = false;

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

		//Set spriteRenderer and colors
		spriteRenderer = GetComponentInChildren <SpriteRenderer> ();
		bossColor = new Color (1f, 1f, 1f, 1f);
		bossHurtColor = new Color (1f, 0, 0, 1f);
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate () {

		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Black fadeout if boss is defeated
		if (bossDefeated == true) {
			alpha += Time.deltaTime / fadeTime;
		}
	}
	#endregion

	#region Update
	void Update () {
	
		//Move boss with bossSpeed
		Vector3 temp = transform.position;
		temp.x += bossSpeed;
		transform.position = temp;

		//Adjust bossSpeed for the flow/ebb of a chase scene
		bossSpeed -= 0.0015f;
		if (temp.x <= Camera.main.transform.position.x - 20 && 
		    Camera.main.transform.position.x <= 480) {
			resetBossSpeed ();
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
	void OnCollisionEnter2D (Collision2D touchBoss3) {
		
		//Hurt bunny if it touches the boss
		if (touchBoss3.gameObject.tag == "Player") {
			bunny.DamageBunny (bossDamage);
		}
	}
	#endregion

	#region Methods
	//Damage boss and reset hurt timer
	public void DamageBoss(int damage) {
		if (bossIsHurt == false) {
			bossIsHurt = true;
			//TODO bossHurtTimer = 3f;
			bossHealth -= damage;
		}
	}

	private void resetBossSpeed () {
		bossSpeed = Random.Range((float)0.29, (float)0.316);
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

	//Set fade color and rectangle with black texture
	void OnGUI () {
		GUI.color = new Color (1f, 1f, 1f, alpha);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), blackTexture);
	}
	
	//On boss defeat, load next level after fade out time
	private IEnumerator levelComplete () {
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	#endregion
}
