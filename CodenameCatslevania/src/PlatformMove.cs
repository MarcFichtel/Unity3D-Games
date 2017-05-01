using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour {

	public Transform bound1; 	// Right or Top
	public Transform bound2;	// Left or bottom
	public bool horizMove;
	private float speed = 0.03f;

	void Update () {

		Vector3 pos = transform.position;

		if (horizMove) {
			pos.x += speed;

			if (pos.x > bound1.position.x) {
				pos.x = bound1.position.x;
				speed *= -1;
			} else if  (pos.x < bound2.position.x) {
				pos.x = bound2.position.x;
				speed *= -1;
			}


		} else {
			pos.y += speed;

			if (pos.y > bound1.position.y) {
				pos.y = bound1.position.y;
				speed *= -1;
			} else if  (pos.y < bound2.position.y) {
				pos.y = bound2.position.y;
				speed *= -1;
			}
		}

		transform.position = pos;
	}
}
