using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Class manages button hover effects
public class ButtonHover : MonoBehaviour {

	// Variables
	private SFX sfx;
	private Button thisButton;

	// Get button, sfx
	void Start () {

		// Get button
		thisButton = GetComponent<Button> ();

		// Get Audiomaster
		if (sfx == null) {
			sfx = GameObject.Find("Audiomaster").GetComponent<SFX>();
		}
	}
	
	// Play click sound on button hover
	public void OnMouseEnter() {
		sfx.PlaySound (sfx.uiClick1);
	}
}
