using UnityEngine;
using System.Collections;

//End of boss 2 / transition to next level class
public class Boss2Done : MonoBehaviour {

	#region Variables
	//Boss 2 done variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public bool bossDefeated = false;
	public bool goalReached = false;
	public Transform boss2;
	public Transform webGround;
	public float fallSpeed = 0.2f;
	
	//Fading variables
	public Texture2D blackTexture;
	private float fadeTime = 3f;
	private float alpha = 0f;
	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		//Get spider boss
		if (boss2 == null) {
			boss2 = GameObject.FindGameObjectWithTag ("Enemy").GetComponent <Transform> ();
		}

		//Get web ground
		if (webGround == null) {
			webGround = GameObject.FindGameObjectWithTag ("Web Ground").GetComponent <Transform> ();
		}
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate() {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Fadeout
		if (goalReached == true) {
			alpha += Time.deltaTime / fadeTime;
		}
		
		return;
	}
	#endregion

	#region Update
	void Update () {

		//Execute boss kill sequence: stalagtite falls, web ground falls, boss falls and dies
		Vector3 temp = transform.position;
		Vector3 tempWeb = webGround.transform.position;

		//Move final stalagtite
		if (bossDefeated == true) {
			temp.y -= fallSpeed;
		}

		//Move web ground when stalagtite 'touches' it, but slightly slower
		if (temp.y <= webGround.transform.position.y) {
			tempWeb.y -= fallSpeed / 4;
		}

		//Destroy final stalagtite and web ground offscreen
		if (temp.y <= -500) {
			Destroy (this.gameObject);
			Destroy (webGround.gameObject);
		}

		//Apply movement
		transform.position = temp;
		webGround.transform.position = tempWeb;
	}
	#endregion

	#region Collisions
	//Do this when goal is reached
	void OnCollisionEnter2D (Collision2D trackEnd) {
		if (trackEnd.gameObject.tag == "Player" &&
		    bossDefeated == false) {
			StartCoroutine(levelComplete());
			gm.boss2DefeatSound.Play();
		}
	}
	#endregion

	#region Methods
	//Do boss kill sequence, then start fadeout
	IEnumerator levelComplete () {
		bossDefeated = true;
		yield return new WaitForSeconds (5);
		goalReached = true;
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	//Set fade color and rectangle with black texture
	void OnGUI () {
		GUI.color = new Color (1f, 1f, 1f, alpha);
		GUI.DrawTexture (new Rect (-2, 0, Screen.width + 4, Screen.height), blackTexture);
	}
	#endregion
}