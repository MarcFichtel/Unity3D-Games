using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class BombSkullEnemy : MonoBehaviour {

	private float speed = 2.0f;
	public Transform target;
	public Transform boundLeft;
	public Transform boundRight;
	public Transform boundTop;
	public Transform boundBottom;

	void Start () {
		target = GameObject.FindObjectOfType <CharControl> ().GetComponent <Transform> ();
		boundLeft = GameObject.Find ("01BS_Left").GetComponent <Transform> ();
		boundRight = GameObject.Find ("01BS_Right").GetComponent <Transform> ();
		boundTop = GameObject.Find ("01BS_Top").GetComponent <Transform> ();
		boundBottom = GameObject.Find ("01BS_Bottom").GetComponent <Transform> ();
	}

	void Update () {
	
		if (target.position.x >= boundLeft.position.x &&
			target.position.x <= boundRight.position.x &&
			target.position.y >= boundBottom.position.y &&
			target.position.y <= boundTop.position.y) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
		
			Vector3 targetDir = target.position - transform.position;
			Vector3 dir = Vector3.RotateTowards (transform.forward, targetDir, speed * Time.deltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation (dir);
		}

		if (target == null) {
			target = GameObject.FindObjectOfType <CharControl> ().GetComponent <Transform> ();
		}
	}

	public void setTarget (Transform newTarget) {
		target = newTarget;
	}
}
