using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Credits slider class
public class CreditsSlider : MonoBehaviour {

	#region Variables
	//Credits slider variables
	public Transform target;
	public Slider slider;
	private float sliderValue;
	private Vector3 temp;
	#endregion

	#region Update
	void Update () {

		//Get slider value
		sliderValue = slider.value * 28;

		//Get current position of credits text
		temp = target.position;

		//Apply slider value to y position of credits text
		temp.y = sliderValue;

		//Move credits text based on slider value
		target.position = temp;
	}
	#endregion
}
