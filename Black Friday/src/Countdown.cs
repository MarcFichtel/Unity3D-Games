using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	private static Gamemaster gm;
	private Animator countdownAnimator;
	private float countdownTimer = 0.0f;
	private float destroyTime = 3.9f;

	// Get Gamemaster, Animator
	void Awake () {

		// Get Gamemaster and Animator
		gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		countdownAnimator = GetComponent<Animator>();
	}

	// Destroy couuntdown after it finished
	private void Update () {

		// Only do the following while the game is not paused
		if (!gm.GetPause()) {

			// Countdown animates when game is not paused
			countdownAnimator.speed = 1;
			// Start timer
			countdownTimer += Time.deltaTime;

			// Destroy countdown object once its done
			if (countdownTimer >= destroyTime) {
				Destroy(this.gameObject);
			}
		
			// Stop the animation while game is paused
		} else {
			countdownAnimator.speed = 0;
		}
	}
}
