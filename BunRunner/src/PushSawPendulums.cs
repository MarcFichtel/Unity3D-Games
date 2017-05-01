using UnityEngine;
using System.Collections;

//Push Saw pendulums on City 2 and Boss 3 levels class (to avoid decreasing momentum)
public class PushSawPendulums : MonoBehaviour {

	#region Variables
	//variables
	private bool addingForce;
	private Vector2 force = new Vector2 (300f, 0);
	private Rigidbody2D sawRB;
	#endregion

	#region Start
	void Start () {
	
		//Get rigidbody
		if (sawRB == null) {
			sawRB = GetComponent<Rigidbody2D>();
		}
	}
	#endregion

	#region Update
	void Update () {
	
		//Give saw pendulums a push every 30sec as long as camera is far enough away
		if (addingForce == false) {
			addingForce = true;
			StartCoroutine(AddForce());
		}
	}
	#endregion

	#region Methods
	//Push saw pendulums so they don't loose their momentum over time on City 2 and Boss 3
	public IEnumerator AddForce () {
		if (Application.loadedLevel == 9) {
			sawRB.AddForce (force);
			Debug.Log("push");
		} else {
			sawRB.AddForce (force * -1);
		}
		yield return new WaitForSeconds (30);
		addingForce = false;
	}
	#endregion
}