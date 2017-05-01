using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private float speed = 0.1f;
	private Camera cam;

	void Start () {
		cam = GameObject.Find ("Cam").GetComponent <Camera> ();
	}

	void Update () {
	
		if (this.transform.position.x >= cam.transform.position.x + 20 ||
			this.transform.position.z <= cam.transform.position.z - 20 || 
			this.transform.position.x <= cam.transform.position.x - 20) {
			Destroy (this.gameObject);
		}

		Vector3 pos = transform.position;

		if (pos.y < 40.0f) {
			pos.x -= speed;
		} else {
			pos.z -= 3*speed;
		}
		transform.position = pos;
	}
}
