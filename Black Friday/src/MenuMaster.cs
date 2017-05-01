using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

// Main Menu
public class MenuMaster : MonoBehaviour {

	// Main menu variables
	public GameObject bloodDropPrefab; 	// Blood drop that falls from ceiling
	private Canvas mainMenu; 			// Main menu
	private Canvas helpMenu; 			// How to play menu
	private Canvas creditsMenu;			// Credits menu
	private FadeInAndOut blackCurtain;	// Curtain for fading
	private static SFX sfx;				// Audiomaster
	private AudioSource menuSong; 		// Menu song

	// Blood drop variables
	private bool bloodSpawning = false;			// Are blood drops being spawned?
	private float screenBorderLeft = -31.76f; 	// Used for blood spawn position
	private float screenBorderRight = -14.5f;	// Used for blood spawn position
	private float bloodSpawnPosY = 8.25f;		// Used for blood spawn position

	// Logo variables
	private Button LogoLDS;							// Luis Da Silva Logo
	private Button LogoFTG; 						// Fishtail Games Logo
	private string WebsiteLDS = "www.google.com";	// Luis Da Silva Website
	private string WebsiteFTG = "www.google.com";	// Fishtail Games Website

	// Get Gamemaster, Canvases, Curtain, Blood drop, Logos, SFX, Audiosource, Buttons
	private void Start () {

		// Get canvases, disable all but the main screen
		mainMenu = GameObject.Find("Canvas: Main Menu").GetComponent<Canvas>();
		helpMenu = GameObject.Find("Canvas: Help").GetComponent<Canvas>();
		helpMenu.enabled = false;
		creditsMenu = GameObject.Find("Canvas: Credits").GetComponent<Canvas>();
		creditsMenu.enabled = false;

		// Get fading curtain
		blackCurtain = GameObject.Find("Black Curtain").GetComponent<FadeInAndOut>();

		// Get Logos
		LogoLDS = GameObject.Find("Button: LDS").GetComponent<Button>();
		LogoFTG = GameObject.Find("Button: FTG").GetComponent<Button>();

		// Get Audiomaster
		if (sfx == null) {
			sfx = GameObject.Find("Audiomaster").GetComponent<SFX>();
		}

		// Get & mute BG Song
		menuSong = GameObject.Find("Menu Camera").GetComponent<AudioSource>();
		menuSong.volume = 0.0f;
	}

	// Redirect to author websites on Logo click
	public void ClickLogoLDS () {
		Application.OpenURL(WebsiteLDS);
	}
	public void ClickLogoFTG () {
		Application.OpenURL(WebsiteFTG);
	}

	// Spawn blood drops, fade music in, Button hover sound
	private void FixedUpdate () {

		// Spawn blood droplets
		if (!bloodSpawning) {
			bloodSpawning = true;
			StartCoroutine(SpawnBlood());
		}

		// Fade in BG song
		if (menuSong.volume < 0.3f) {
			menuSong.volume += 0.01f;	
		}
	}

	// Method is bound to start button
	public void StartButton() {
		
		// Clicking the start button starts the game at the first level after a fade-out
		blackCurtain.StartFadeOut();
		sfx.PlaySound(sfx.confirm1);
		StartCoroutine(StartGame());
	}

	// Method is bound to how to play button
	public void HelpButton() {

		// Clicking the help button switches to the help menu canvas
		mainMenu.enabled = false;
		helpMenu.enabled = true;
		sfx.PlaySound(sfx.confirm2);
	}

	// Method is bound to back buttons
	public void BackToMain() {

		// Enables main menu canvas and disabled all others
		helpMenu.enabled = false;
		creditsMenu.enabled = false;
		mainMenu.enabled = true;
	}

	// Method is bound to the credits button
	public void CreditsButton() {

		// Hide main menu and show credits
		mainMenu.enabled = false;
		creditsMenu.enabled = true;
		sfx.PlaySound(sfx.confirm2);
	}

	// Method waits for a second, then starts the game
	private IEnumerator StartGame () {
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(1);
	}

	// Method spawn two blood drops
	private IEnumerator SpawnBlood () {

		// Instantiate first blood drop
		Instantiate (bloodDropPrefab, 
			new Vector3(Random.Range(screenBorderLeft, screenBorderRight), bloodSpawnPosY, 0), 
			Quaternion.identity);

		// Wait, then spawn another blood drop
		yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
		Instantiate (bloodDropPrefab, 
			new Vector3(Random.Range(screenBorderLeft, screenBorderRight), bloodSpawnPosY, 0), 
			Quaternion.identity);

		// Wait for a random time, then set bloodSpawning to false
		yield return new WaitForSeconds (Random.Range(1.0f, 5.0f));
		bloodSpawning = false;
	}
}
