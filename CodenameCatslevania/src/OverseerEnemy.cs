using UnityEngine;
using System.Collections;

public class OverseerEnemy : MonoBehaviour {

	public Projectile shotPrefab;
	private CameraStuff cam;
	private Animator animator;
	private bool shotReady = true;
	private bool attacking = false;

	void Start () {
		cam = GameObject.Find ("Cam").GetComponent <CameraStuff> ();
		animator = GetComponent <Animator> ();
	}

	void Update () {
	
		animator.SetBool ("attacking", attacking);

		if (shotReady && (
			this.transform.position.x  - cam.transform.position.x <= 15.0f &&
			this.transform.position.x  - cam.transform.position.x >= -20.0f)) {

			shotReady = false;
			attacking = true;
			StartCoroutine (shoot());
		}
	}

	public IEnumerator shoot () {
		Instantiate (shotPrefab, this.transform.position, Quaternion.Euler (0,0,90));
		yield return new WaitForSeconds (2.0f);
		attacking = false;
		yield return new WaitForSeconds (2.0f);
		shotReady = true;
	}
}
