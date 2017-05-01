using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour {

	public GameObject door;

	public void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag == "Shot") {
			Destroy (c.gameObject);
			Destroy (door.gameObject);
			Destroy (this.gameObject);
		}
	}
}
