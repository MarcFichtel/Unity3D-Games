using UnityEngine;
using System.Collections;

public class JumperEnemy : MonoBehaviour {

	private float speed = 0.1f;
	private float attackReload = 5.0f;
	private bool attacking = false;
	private bool attackComplete = true;
	private bool running = false;
	public Transform target;
	public Transform boundLeft;
	public Transform boundRight;
	private Rigidbody rb;
	private Animator animator;

	void Start () {
		rb = GetComponent <Rigidbody> ();
		animator = GetComponent <Animator> ();
	}

	void Update () {

		animator.SetBool ("attacking", attacking);
		animator.SetBool ("running", running);

		Vector3 pos = transform.position;
		float rot = transform.rotation.x;

		if (target == null) {
			target = GameObject.FindObjectOfType <CharControl> ().GetComponent <Transform> ();
		}

		if (target.position.x >= boundLeft.position.x &&
			target.position.x <= transform.position.x &&
			target.position.y >= transform.position.y - 5.0f &&
			target.position.y <= transform.position.y + 5.0f) {

			rot = 180;

			if (!attacking && attackComplete) {
				attacking = true;
				attackComplete = false;
				StartCoroutine (jumpAttack (false));
			
			} else if (!attacking && !attackComplete) {
				pos.x -= speed;
				running = true;
			}
	
		} else if (target.position.x <= boundRight.position.x &&
			target.position.x >= transform.position.x &&
			target.position.y >= transform.position.y - 5.0f &&
			target.position.y <= transform.position.y + 5.0f) {

			rot = 0;

			if (!attacking && attackComplete) {
				attacking = true;
				attackComplete = false;
				StartCoroutine (jumpAttack (true));

			} else if (!attacking && !attackComplete) {
				pos.x += speed;
				running = true;
			} 

		} else {
			running = false;
		}

		transform.position = pos;
		transform.rotation = Quaternion.Euler (0,rot,0);
	}

	public IEnumerator jumpAttack (bool facingRight) {

		if (facingRight) {
			rb.AddForce (new Vector3 (200.0f, 400.0f, 0));
		} else {
			rb.AddForce (new Vector3 (-200.0f, 400.0f, 0));
		}

		yield return new WaitForSeconds (attackReload);
		attackComplete = true;
	}

	public void OnCollisionEnter (Collision c) {

		if (c.gameObject.tag == "Ground") {
			attacking = false;
		}
	}

	public void setTarget (Transform newTarget) {
		target = newTarget;
	}
}
