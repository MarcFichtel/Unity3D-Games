using UnityEngine;
using System.Collections;

public class DeathParticles : MonoBehaviour {

	private bool killing = false	;

	void Update () {
	
		if (!killing) {
			killing = true;
			StartCoroutine (kill());
		}
	}

	public IEnumerator kill () {
		yield return new WaitForSeconds (5.0f);
		Destroy (this.gameObject);
	}
}
