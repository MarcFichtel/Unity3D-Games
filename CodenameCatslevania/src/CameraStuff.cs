using UnityEngine;
using System.Collections;

public class CameraStuff : MonoBehaviour {

	public CharControl target;
	private float yOffset = 5.0f;
	private float yAdjustSpeed = 0.2f;

	void Update () {

		Vector3 pos = transform.position;
		if (target != null) {
			pos.x = target.transform.position.x;

			if (!target.getJumping () && pos.y > target.transform.position.y + yOffset + 1) {
				pos.y -= yAdjustSpeed;
			} else if (!target.getJumping () && pos.y < target.transform.position.y + yOffset - 1) {
				pos.y += yAdjustSpeed;
			}
		}

		transform.position = pos;
	}

	public void setTarget (CharControl newTarget) {
		target = newTarget;
	}
}
