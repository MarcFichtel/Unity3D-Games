using UnityEngine;
using System.Collections;

//Ground collision check / squish the bunny class
public class GroundCollisionCheck : MonoBehaviour {

	#region Variables
	//Variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public BoxCollider2D boxcol;
	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		boxcol = GetComponent<BoxCollider2D> ();
	}
	#endregion

	#region Update, FixedUpdate
	void FixedUpdate() {
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}
	
	void Update () {
	
		if (bunny != null) {
			Vector3 bunnyPos = bunny.transform.position; 
			if (boxcol.bounds.Contains (bunnyPos)) {
				bunny.currentHealth = 0;
			}
		}
	}
	#endregion
}
