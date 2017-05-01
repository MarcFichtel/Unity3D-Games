using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// Class represents the User Interface, and handles display and interaction
public class UIMaster : MonoBehaviour {

	// General variables
	private static Gamemaster gm;		// The gamemaster
	private PlayerStats stats;			// The player
	private Boss1 boss1;				// First boss
	private Boss2 boss2;				// Second boss
//	private Boss3 boss3;				// Third boss
//	private Boss4 boss4;				// Fourth boss
//	private Boss5 boss5;				// Fifth boss
	private FadeInAndOut blackCurtain;	// Curtain for fading

	// Display variables
	private Text healthDisplay;			// Displays health
	private Image healthDisplayBG;		// Health display BG
	private Text bossHealthDisplay; 	// Displays boss health
	private Image bossHealthBG; 		// Highlights boss health display
	private Text moneyDisplay;			// Displays funds
	private Image moneyDisplayBG;		// Money display BG
	private float alpha = 0f; 			// Used for boss health display fade-in
	private float fadeInTime = 3.0f;	// Used for boss health display fade-in
	private float colorReset = 1.0f;	// Health display BG color resets this fast

	// Weapon variables
	private Image attackReady;			// Attack ready indicator
	public Sprite spoonWeapon;			// Spoon
	public Sprite ballWeapon;			// Ball
	public Sprite clubWeapon;			// Club
	public Sprite gunWeapon;			// Gun

	// Volume variables
	private Button volume;				// Toggle volume button
	public Sprite muted; 				// Volume muted state sprite
	public Sprite unmuted;				// Volume unmuted state sprite

	// Other variables
	private GameObject Pause;			// Pause menu object

	// Get UI elements, PlayerStats, Gamemaster, Bosses, Curtain, Weapon Images
	private void Start () {

		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}

		// Get buttons
		volume = GameObject.Find("Button: Volume").GetComponent<Button>();

		// Get fading curtain
		blackCurtain = GameObject.Find("Black Curtain").GetComponent<FadeInAndOut>();

		// Get level UI elements
		if (SceneManager.GetActiveScene().name != "menu" &&
			SceneManager.GetActiveScene().name != "shop" &&
			SceneManager.GetActiveScene().name != "gameover") {
			stats = GameObject.Find("Player").GetComponent<PlayerStats>();						// PlayerStats
			healthDisplay = GameObject.Find("Text: Health").GetComponent<Text>();				// Health text
			healthDisplayBG = GameObject.Find("Image: Health BG").GetComponent<Image>();		// Health BG
			moneyDisplay = GameObject.Find("Text: Money").GetComponent<Text>();					// Money text
			moneyDisplayBG = GameObject.Find("Image: Money BG").GetComponent<Image>();			// Money BG
			attackReady = GameObject.Find("Image: Attack ready").GetComponent<Image>();			// Attach ready indicator
			bossHealthDisplay = GameObject.Find("Text: Boss Health").GetComponent<Text>();
			bossHealthDisplay.color = new Color (0f,0f,0f,0f);
			bossHealthBG = GameObject.Find("Image: Boss Health BG").GetComponent<Image>();
			bossHealthBG.color = new Color (0f,0f,0f,0f);
			Pause = GameObject.Find("Pause");				 									// Pause menu
			Pause.SetActive(false);																// Pause is inactive at level start

			// Get Bosses, hide their health display until entry is done
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				boss1 = GameObject.Find ("Boss1").GetComponent<Boss1> ();
			} else if (SceneManager.GetActiveScene ().buildIndex == 2) {
				boss2 = GameObject.Find ("Boss2").GetComponent<Boss2> ();
			} 
//			else if (SceneManager.GetActiveScene ().buildIndex == 3) {
//				boss3 = GameObject.Find ("Boss3").GetComponent<Boss3> ();
//			} else if (SceneManager.GetActiveScene ().buildIndex == 4) {
//				boss4 = GameObject.Find ("Boss4").GetComponent<Boss4> ();
//			} else if (SceneManager.GetActiveScene ().buildIndex == 5) {
//				boss5 = GameObject.Find ("Boss5").GetComponent<Boss5> ();
//			}
		}
	}

	// Show UI, Update UI Colors, Update display colors
	private void Update () {

		// Do not update certain UI elements on menu screens
		if (SceneManager.GetActiveScene().name != "menu" &&
			SceneManager.GetActiveScene().name != "shop" &&
			SceneManager.GetActiveScene().name != "gameover") {

			// Show Pause Menu while the game is paused
			if (gm.GetPause()) {
				Pause.SetActive(true);
			} else {
				Pause.SetActive(false);
			}

			// Hide attack ready indicator while player is attacking
			if (stats.getAttacking()) {
				attackReady.color = new Color (0f,0f,0f,0f);
			} 		

			// Show attack ready indicator when player is not attacking
			else {
				attackReady.color = new Color (1f,1f,1f,1f);
			}

			// Update health display texts
			healthDisplay.text = ("Health: " + stats.getHealth());

			// Update money display text
			moneyDisplay.text = ("Funds: $" + gm.GetMoney());

			// Reset health BG over time
			Color tempHealthColor = healthDisplayBG.color;
			if (tempHealthColor.r < 1.0f) {
				tempHealthColor.r += (Time.deltaTime / colorReset);
			}
			if (tempHealthColor.g < 1.0f) {
				tempHealthColor.g += (Time.deltaTime / colorReset);
			}
			if (tempHealthColor.b < 1.0f) {
				tempHealthColor.b += (Time.deltaTime / colorReset);
			}
			healthDisplayBG.color = tempHealthColor;

			// Reset money BG over time
			Color tempMoneyColor = moneyDisplayBG.color;
			if (tempMoneyColor.r < 1.0f) {
				tempMoneyColor.r += (Time.deltaTime / colorReset);
			}
			if (tempMoneyColor.g < 1.0f) {
				tempMoneyColor.g += (Time.deltaTime / colorReset);
			}
			if (tempMoneyColor.b < 1.0f) {
				tempMoneyColor.b += (Time.deltaTime / colorReset);
			}
			moneyDisplayBG.color = tempMoneyColor;

			// Update boss health display texts
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				bossHealthDisplay.text = ("Boss: " + boss1.GetBoss1Health ());

				// Boss health display fades in after boss entry is done
				if (boss1.GetBoss1EntryDone ()) {
					alpha += Time.deltaTime / fadeInTime;
					bossHealthBG.color = new Color (1f, 1f, 1f, alpha);
					bossHealthDisplay.color = new Color (0f, 0f, 0f, alpha);
				}
			} else if (SceneManager.GetActiveScene ().buildIndex == 2) {
				bossHealthDisplay.text = ("Boss: " + boss2.GetBoss2Health ());

				// Boss health display fades in after boss entry is done
				if (boss2.GetBoss2EntryDone ()) {
					alpha += Time.deltaTime / fadeInTime;
					bossHealthBG.color = new Color (1f, 1f, 1f, alpha);
					bossHealthDisplay.color = new Color (0f, 0f, 0f, alpha);
				}
			} 
//			else if (SceneManager.GetActiveScene ().buildIndex == 3) {
//				bossHealthDisplay.text = ("Boss: " + boss3.GetBoss3Health ());
//
//				// Boss health display fades in after boss entry is done
//				if (boss3.GetBoss3EntryDone ()) {
//					alpha += Time.deltaTime / fadeInTime;
//					bossHealthBG.color = new Color (1f, 1f, 1f, alpha);
//					bossHealthDisplay.color = new Color (0f, 0f, 0f, alpha);
//				}
//			} else if (SceneManager.GetActiveScene ().buildIndex == 4) {
//				bossHealthDisplay.text = ("Boss: " + boss4.GetBoss4Health ());
//
//				// Boss health display fades in after boss entry is done
//				if (boss4.GetBoss4EntryDone ()) {
//					alpha += Time.deltaTime / fadeInTime;
//					bossHealthBG.color = new Color (1f, 1f, 1f, alpha);
//					bossHealthDisplay.color = new Color (0f, 0f, 0f, alpha);
//				}
//			} else if (SceneManager.GetActiveScene ().buildIndex == 5) {
//				bossHealthDisplay.text = ("Boss: " + boss5.GetBoss5Health ());
//
//				// Boss health display fades in after boss entry is done
//				if (boss2.GetBoss5EntryDone ()) {
//					alpha += Time.deltaTime / fadeInTime;
//					bossHealthBG.color = new Color (1f, 1f, 1f, alpha);
//					bossHealthDisplay.color = new Color (0f, 0f, 0f, alpha);
//				}
//			}
		}
	}

	// Method mutes or unmutes the volume button
	public void toggleVolume () {

		// If volume is on, turn it off and change button sprite
		if (volume.image.sprite == unmuted) {
			volume.image.sprite = muted;
			AudioListener.volume = 0.0f;
		}

		// If volume is off, turn it on and change button sprite
		else {
			volume.image.sprite = unmuted;
			AudioListener.volume = 1.0f;
		}
	}

	// Method loads the next level on shop screen
	public void NextLevel () {
		StartCoroutine(StartNextLevel());
	}

	// Method used for continue button on game over screen
	public void Continue () {
		StartCoroutine(ContinueGame());
	}

	// Method used for quit button on game over screen
	public void Quit () {
		StartCoroutine(QuitGame());
	}

	// Fade out, then go to next level
	private IEnumerator StartNextLevel() {
		blackCurtain.StartFadeOut();
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(gm.GetCurrentLevel() + 1);
	}

	// Fade out, then retry last level
	private IEnumerator ContinueGame() {
		blackCurtain.StartFadeOut();
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(gm.GetCurrentLevel());
	}

	// Fade out, then go to main menu
	private IEnumerator QuitGame() {
		blackCurtain.StartFadeOut();
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(0);
	}

	// Health BG Setter
	public void SetHealthBGColor (Color newColor) {
		healthDisplayBG.color = newColor; 
	}

	// Money BG Setter
	public void SetMoneyBGColor (Color newColor) {
		moneyDisplayBG.color = newColor; 
	}

	// Eqiopped weapon image Setter
	public void SetEquippedWeaponImage (string weapon) {
		if (weapon == "Spoon") {
			attackReady.sprite = spoonWeapon;
		} else if (weapon == "Ball") {
			attackReady.sprite = ballWeapon;
		} else if (weapon == "Club") {
			attackReady.sprite = clubWeapon;
		} else if (weapon == "Gun") {
			attackReady.sprite = gunWeapon;
		}
	}
}
