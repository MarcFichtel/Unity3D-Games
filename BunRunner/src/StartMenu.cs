using UnityEngine;
using System.Collections;

//Start menu class
public class StartMenu : MonoBehaviour {

	#region Variables
	//Start menu variables
	public Texture2D blackTexture;
	public float fadeTime = 3f;
	private float alpha = 0f;
	private bool startingGame;
	private AudioSource menuSong;
	private CameraFade camFadeScript;

	//Menu item variables
	public GameObject mainMenu;
	public GameObject helpMenu;
	public GameObject credits;

	#endregion

	#region Start, Update
	void Start () {

		//Starting game is initially false
		startingGame = false;

		//Get menu song
		menuSong = FindObjectOfType<Camera> ().GetComponent<AudioSource> ();

		//Get camFadeScript to make buttons clickable once black background is fading out
		camFadeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFade> ();
	}

	void Update () {

		//Fadeout if game is starting
		if (startingGame) {
			alpha += Time.deltaTime / fadeTime;
			menuSong.volume -= 0.0054f;
		}
	}
	#endregion

	#region Methods
	//Go to help menu
	public void LoadHelpMenu () {
		if (!startingGame &&
		    camFadeScript.fadeOutBlackBG) {
			mainMenu.SetActive (false);
			helpMenu.SetActive (true);
		}
	}

	//Go to credits
	public void LoadCredits () {
		if (!startingGame &&
		    camFadeScript.fadeOutBlackBG) {
			mainMenu.SetActive (false);
			credits.SetActive (true);
		}
	}

	//Go back to main menu
	public void BackToMainMenu () {
		if (!startingGame &&
		    camFadeScript.fadeOutBlackBG) {
			mainMenu.SetActive (true);
			helpMenu.SetActive (false);
			credits.SetActive (false);
		}
	}

	//Set fade color and rectangle with black texture
	void OnGUI () {
		GUI.color = new Color (1f, 1f, 1f, alpha);
		GUI.DrawTexture (new Rect (-2, 0, Screen.width + 4, Screen.height), blackTexture);
	}

	//Start game on click
	public void ClickStart () {
		if (!startingGame &&
		    camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(2));
		}
	}

	// Load Levels
	public void LoadLevel2 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(3));
		}
	}
	public void LoadLevel3 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(4));
		}
	}
	public void LoadLevel4 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(5));
		}
	}
	public void LoadLevel5 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(6));
		}
	}
	public void LoadLevel6 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(7));
		}
	}
	public void LoadLevel7 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(8));
		}
	}
	public void LoadLevel8 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(9));
		}
	}
	public void LoadLevel9 () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(10));
		}
	}
	public void LoadLevelGameOver () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(1));
		}
	}
	public void LoadLevelGameComplete () {
		if (!startingGame &&
			camFadeScript.fadeOutBlackBG) {
			startingGame = true;
			StartCoroutine(StartGame(11));
		}
	}

	//Start game after fadeout delay, set starting number of lives to 3
	IEnumerator StartGame (int level) {
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel(level);
		GameMaster.bunnyLives = 3;
		GameMaster.Score = 0;
	}
	#endregion
}
