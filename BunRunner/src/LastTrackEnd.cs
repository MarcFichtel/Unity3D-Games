using UnityEngine;
using System.Collections;

//Final track end class
public class LastTrackEnd : MonoBehaviour {

	#region Variables
	//Goal variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public bool goalReached = false;
	
	//Fading variables
	public Texture2D whiteTexture;
	private float fadeTime = 5f;
	private float alpha = 0f;
	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}
	#endregion

	#region OnGUI
	//Set fade color and rectangle with white texture
	void OnGUI () {
		GUI.color = new Color (1f, 1f, 1f, alpha);
		GUI.DrawTexture (new Rect (-2, 0, Screen.width + 4, Screen.height), whiteTexture);
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

	#region Collisions
	//Do this when goal is reached
	void OnCollisionEnter2D (Collision2D trackEnd) {
		if (trackEnd.gameObject.tag == "Player") {
			StartCoroutine(levelComplete());
			goalReached = true;
		}
	}
	#endregion

	#region Methods
	//Load next level after fadeout
	IEnumerator levelComplete () {
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	#endregion
}
