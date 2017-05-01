using UnityEngine;
using System.Collections;

// Class represents the blood drop object in start menu
public class Blooddrop : MonoBehaviour {

	// Variables
	private float fallSpeed = 0.1f;			// Spped at which blood enters
	private float fallSpeedNew = 0.6f;		// Spped at which blood falls after entry
	private float fallEntryPos = 6.9f;		// Blood enters as it hangs from the ceiling
	private float borderBottom = -8.0f; 	// Destroy drop offscreen boundary
	
	// 
	void Update () {

		// Blood drop falls
		Vector3 temp = transform.position;
		temp.y -= fallSpeed;
		transform.position = temp;

		// Adjust fallSpeed after entry
		if (temp.y <= fallEntryPos) {
			fallSpeed = fallSpeedNew;
		}

		// Destroy offscreen
		if (temp.y <= borderBottom) {
			Destroy(this.gameObject);
		}
	}
}
