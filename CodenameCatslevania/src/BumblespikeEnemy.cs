using UnityEngine;
using System.Collections;

public class BumblespikeEnemy : MonoBehaviour {

	public Transform boundLeft;
	public Transform boundRight;
	public Transform target;
	private bool running = false;
	private bool facingLeft = true;
	private float speed = 0.02f;
	private Animator animator;

	void Start () {
		animator = GetComponent <Animator> ();
	}

	void Update () {
	
		animator.SetBool ("running", running);

		Vector3 pos = transform.position;
		Vector3 scl = transform.localScale;

		if (!facingLeft) {
			scl.x = -1.0f;
		} else {
			scl.x = 1.0f;
		}

		if (target != null &&
			target.position.x >= boundLeft.position.x && 
			target.position.x <= boundRight.position.x) {

			running = true;

			if (target.position.x < pos.x) {
				pos.x -= speed;
				facingLeft = true;
			} else if (target.position.x > pos.x) {
				pos.x += speed;
				facingLeft = false;
			}
		} else {
			running = false;
		}

		transform.position = pos;
		transform.localScale = scl;

	}

	public void setTarget (Transform newTarget) {
		target = newTarget;
	}
}
