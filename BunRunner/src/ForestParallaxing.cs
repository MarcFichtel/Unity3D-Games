using UnityEngine;
using System.Collections;

//Forest background parallax class
public class ForestParallaxing : MonoBehaviour {

	#region Variables
	//Variables
	public static GameMaster gm;
	private Transform bgFar;
	private Transform bgNear;
	private float lastX;
	private float currentX;
	public float parallaxMove;
	#endregion

	#region Start
	void Start () {
	
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		//Get backgrounds
		bgFar = GameObject.FindGameObjectWithTag ("bgfar").GetComponent <Transform> ();
		bgNear = GameObject.FindGameObjectWithTag ("bgnear").GetComponent <Transform> ();
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate () {

		//Set last X position to current X position at the end of fixed update
		lastX = currentX;
	}
	#endregion

	#region Update
	void Update () {
	
		//Get background position
		Vector3 tempFar = bgFar.position;
		Vector3 tempNear = bgNear.position;

		//Get current X position and difference between current and last x position
		currentX = Camera.main.transform.position.x;
		parallaxMove = currentX - lastX;

		//Apply parallax movement to background planes
		if (!gm.isPaused) {
			tempFar.x += parallaxMove / 5;
			tempNear.x += parallaxMove / 10;
		}
		bgFar.position = tempFar;
		bgNear.position = tempNear;
	}
	#endregion
}
