using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Lives display class (==> displays lives!!!)
public class LivesDisplay : MonoBehaviour {

	#region Variables
	//Lives variables
	Text lives;
	public static GameMaster gm;
	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
		
		//Get lives object
		lives = GetComponent<Text>();
	}
	#endregion

	#region Update
	void Update () {
		
		//Display lives
		lives.text = ("x " + GameMaster.bunnyLives);
	}
	#endregion
}
