using UnityEngine;
using System.Collections;

//Moving platforms on Boss 1 level class
public class PlatformMove : MonoBehaviour {

	#region Variables
	//Boss 1 moving platforms variables
	private float moveSpeed = 0.05f;
	#endregion

	#region Update
	void Update () {

		//Move platforms
		Vector3 temp = transform.position;
		temp.y += moveSpeed;

		//Switch platforms' direction at specified height
		if (temp.y >= 2.5) {
			moveSpeed *= -1;
		}

		if (temp.y <= -4) {
			moveSpeed *= -1;
		}

		//Apply movement
		transform.position = temp;
	}
	#endregion
}
