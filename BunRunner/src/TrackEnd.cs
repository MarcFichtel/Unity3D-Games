using UnityEngine;
using System.Collections;

//End of track class
public class TrackEnd : MonoBehaviour {

	#region Variables
	//Goal variables
	public static GameMaster gm;
	private BunnyStats bunny;
	public bool goalReached = false;
	private AudioSource levelSong;

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

		//Get level song
		levelSong = FindObjectOfType<Camera> ().GetComponent<AudioSource> ();
	}
	#endregion

	#region OnGUI
	//Set fade color and rectangle with black texture
	void OnGUI () {
		GUI.color = new Color (1f, 1f, 1f, alpha);
		GUI.DrawTexture (new Rect (-2, 0, Screen.width + 4, Screen.height), blackTexture);
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
			levelSong.volume -= 0.0054f;
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
