using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	private float speed = 0.1f;
	private CameraStuff cam;
	public CharControl overseerPlayer;
	private bool movingRight = true;

	void Start () {
		cam = GameObject.Find ("Cam").GetComponent <CameraStuff> ();
		overseerPlayer = GameObject.Find ("OverseerPlayer(Clone)").GetComponent <CharControl> ();
	}

	void Update () {

		if (this.transform.position.x >= cam.transform.position.x + 20) {
			Destroy (this.gameObject);
		} else if (this.transform.position.x <= cam.transform.position.x - 20) {
			Destroy (this.gameObject);
		}

		Vector3 pos = transform.position;
		if (movingRight) {
			pos.x += speed;
		} else {
			pos.x -= speed;
		}
		transform.position = pos;
	}

	public void setMovingRight (bool value) {
		movingRight = value;
	}

	public void OnCollisionEnter (Collision c) {

		if (c.gameObject.tag == "Enemy") {
			overseerPlayer.transformIntoJumper (c.gameObject);
		}

		Shot[] shots = GameObject.FindObjectsOfType <Shot> ();
		foreach (Shot shot in shots) {
			Destroy (shot.gameObject);
		}
	}
}
