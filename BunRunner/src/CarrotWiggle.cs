using UnityEngine;
using System.Collections;

//Carrot rotation (wiggle) class
public class CarrotWiggle : MonoBehaviour {

	#region Variables
	//Carrot wiggle variables
	public float rotSpeed = 10f;
	public float rotMax = -10f;
	public float rotMin = 70f;
	#endregion

	#region Update
	void Update () {

		//Rotate carrots between min & max rotation to create a wiggle 
		transform.Rotate (new Vector3 (0, 0, rotSpeed) * Time.deltaTime);
		//if (transform.rotation.z >= rotMax ||
		//	transform.rotation.z <= rotMin) {
		//	rotSpeed *= -1;
		//}
	}
	#endregion
}
