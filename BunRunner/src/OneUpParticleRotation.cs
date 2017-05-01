using UnityEngine;
using System.Collections;

public class OneUpParticleRotation : MonoBehaviour {

	private float rotSpeed = 2f;

	void Start () {
	
	}

	void Update () {

		//Get rotation
		transform.Rotate (0, 0, rotSpeed);
	
	}
}
