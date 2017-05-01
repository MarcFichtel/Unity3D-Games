using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Camera Fade In & Out class
public class CameraFade : MonoBehaviour {

	#region Variables
	//General variables
	public Texture2D blackTexture;
	private float fadeTime = 5f;
	private float alphaFadeValue = 1f;
	private bool fading = false;

	//Startmenu variables
	public SpriteRenderer blackBG;
	public Text fishtailGames;
	private bool fadeInFishtailGames = true;
	public bool fadeOutBlackBG = false;
	private Color alphaBlackBG = new Color (1f, 1f, 1f, 1f);
	private Color alphaFishtailGamesText = new Color (1f, 1f, 1f, 0f);

	//Winscreen variables
	public SpriteRenderer firstWhiteBG;
	public SpriteRenderer secondWhiteBG;
	public SpriteRenderer thirdWhiteBG;
	private Color alphaFadeValueTwo = new Color (1f, 1f, 1f, 1f);
	private Color alphaFadeValueThree = new Color (1f, 1f, 1f, 1f);
	private Color alphaFadeValueFour = new Color (1f, 1f, 1f, 1f);
	private bool firstfade = false;
	private bool secondFade = false;
	private bool thirdFade = false;
	#endregion

	#region Start
	void Start () {

		//Fade Object that apply to certain levels only are null in other levels
		if (Application.loadedLevel != 11) {
			firstWhiteBG = null;
			secondWhiteBG = null;
			thirdWhiteBG = null;
		} else if (Application.loadedLevel != 0) {
			blackBG = null;
			fishtailGames = null;
		}
	}
	#endregion

	#region OnGUI
	//Set fade color and rectangle(s on Win Screen) with black or white texture
	void OnGUI () {
		GUI.color = new Color (alphaFadeValue, alphaFadeValue, alphaFadeValue, alphaFadeValue);
		GUI.DrawTexture (new Rect (-2, 0, Screen.width + 4, Screen.height), blackTexture);
	}

	#endregion

	#region Update
	void Update () {

		//Start fading procedure on startmenu
		if (Application.loadedLevel == 0) {
			if (!firstfade) {
				firstfade = true;
				StartCoroutine(StartscreenFade());
			}

			//Fade text in and out over time
			if (fishtailGames != null &&
				fadeInFishtailGames) {
				alphaFishtailGamesText.a += (Time.deltaTime / fadeTime);
			} else {
				alphaFishtailGamesText.a -= (Time.deltaTime / fadeTime);

				//Destroy text once its gone so that it doesn't block the buttons
				if (fishtailGames != null &&
					fishtailGames.color.a <= 0) {
					Destroy (fishtailGames.gameObject);
				}
			}

			//Fade background out
			if (fadeOutBlackBG) {
				alphaBlackBG.a -= (Time.deltaTime / fadeTime);
			}

			//Apply new colour alpha
			blackBG.color = alphaBlackBG;
			if (fishtailGames != null) {
			    fishtailGames.color = alphaFishtailGamesText;
			}
		}

		//Start fading out white winscreen backgrounds
		if (Application.loadedLevel == 11 &&
		    !fading) {
			fading = true;
			StartCoroutine (WinscreenFade ());
		}

		//Fade in or out on all levels
		alphaFadeValue = Mathf.Clamp01 (alphaFadeValue - (Time.deltaTime / fadeTime));

		//Fade out white winscreen backgrounds one after the other, destroy them when invisible
		if (firstfade &&
		    firstWhiteBG != null) {
			alphaFadeValueTwo.a -= (Time.deltaTime / fadeTime);
			firstWhiteBG.color = alphaFadeValueTwo;
			if (alphaFadeValueTwo.a <= 0) {
				Destroy (firstWhiteBG.gameObject);
			}
		} 
		if (secondFade &&
		    secondWhiteBG != null) {
			alphaFadeValueThree.a -= (Time.deltaTime / fadeTime);
			secondWhiteBG.color = alphaFadeValueThree;
			if (alphaFadeValueThree.a <= 0) {
				Destroy (secondWhiteBG.gameObject);
			}
		} 
		if (thirdFade &&
		    thirdWhiteBG != null) {
			alphaFadeValueFour.a -= (Time.deltaTime / fadeTime);
			thirdWhiteBG.color = alphaFadeValueFour;
			if (alphaFadeValueFour.a <= 0) {
				Destroy (thirdWhiteBG.gameObject);
			}
		}
	}
	#endregion

	#region Methods
	//Enable fadeout of additional white backgrounds on winscreen
	public IEnumerator WinscreenFade () {
		yield return new WaitForSeconds (8);
		firstfade = true;
		yield return new WaitForSeconds (8);
		secondFade = true;
		yield return new WaitForSeconds (8);
		thirdFade = true;
	}

	//Handle fading of startscreen black background and fishtail games text
	public IEnumerator StartscreenFade () {
		yield return new WaitForSeconds (5);
		fadeInFishtailGames = false;
		yield return new WaitForSeconds (5);
		fadeOutBlackBG = true;
	}
	#endregion
}