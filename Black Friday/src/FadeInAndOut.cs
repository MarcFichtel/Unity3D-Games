using UnityEngine;
using System.Collections;

public class FadeInAndOut : MonoBehaviour {

	private SpriteRenderer blackTexture;
	private float alpha = 1.0f;
	private float fadeSpeed = 1.0f;
	private bool fadingIn = true;
	private bool fadingOut = false;

	// Get curtain
	void Start () {

		// Get black texture used for fading
		blackTexture = GameObject.Find("Black Curtain").GetComponent<SpriteRenderer>();
	}

	// Fade In, Fade out
	void Update () {

		// Update the texture's alpha to fade out at scene start
		if (fadingIn) {
			alpha -= Time.deltaTime / fadeSpeed;
			blackTexture.color = new Color (1f, 1f, 1f, alpha);

			// Set fadingIn to false once the fade-in is done
			if (blackTexture.color.a <= 0f) {
				fadingIn = false;
				blackTexture.enabled = false;
			}
		}

		// Update the texture's alpha to fade in at the end of a scene
		if (fadingOut) {
			alpha += Time.deltaTime / fadeSpeed;
			blackTexture.color = new Color (1f, 1f, 1f, alpha);

			// Set fadingOut to false once the fade-out is done
			if (blackTexture.color.a >= 1f) {
				fadingOut = false;
			}
		}
	}

	// Method starts fadeout sequence
	public void StartFadeOut () {
		fadingOut = true;
		blackTexture.enabled = true;
	}
}
