using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	private float rotSpeed = 1.0f;

	void Update () {
	
		this.transform.Rotate (new Vector3(0,rotSpeed,rotSpeed));
	}
}
