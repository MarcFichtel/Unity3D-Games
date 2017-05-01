using UnityEngine;
using System.Collections;

//Moving platforms on city levels class
public class MovingCityPlatform : MonoBehaviour {

	#region Variables
	//Moving city platforms variables
	private float moveSpeedVertical = 0.025f;
	public Transform borderTop;
	public Transform borderBottom;
	public int rotationSpeed;
	#endregion

	#region Start
	void Start () {

		//Set rotation speed depending on the platform's location
		if (transform.position.x <= 35) {
			rotationSpeed = 25;
		} else {
			rotationSpeed = 250;
		}

		if (Application.loadedLevel == 9) {
			moveSpeedVertical = 0.035f;
		} else if (Application.loadedLevel == 8 &&
			transform.position.x >= 84) {
			moveSpeedVertical = 0.05f;
		} else if (Application.loadedLevel == 10) {
			moveSpeedVertical = 0.04f;
		}
	}
	#endregion

	#region Update
	void Update () {
		
		//Get current position
		Vector3 pos = transform.position;
		
		//Move platforms up and down, if vertical borders are defined
		if (borderTop != null &&
			borderBottom != null) {

			//Move platforms by with platform speed
			pos.y += moveSpeedVertical;

			//Switch platforms' vertical direction at vertical borders
			if (pos.y >= borderTop.position.y) {
				moveSpeedVertical *= -1;
			} else if (pos.y <= borderBottom.position.y) {
				moveSpeedVertical *= -1;
			} 

			//Apply movement
			transform.position = pos;
		}

		//Rotate platforms if no borders are defined
		if (borderTop == null &&
		    borderBottom == null) {
			transform.Rotate(Vector3.forward * (Time.deltaTime * rotationSpeed));
		}
	}
	#endregion
}