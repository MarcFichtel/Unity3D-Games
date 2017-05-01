using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Score display class
public class ScoreDisplay : MonoBehaviour {

	#region Variables
	//Score variables
	Text score;
	public static GameMaster gm;
	#endregion

	#region Start, Update
	void Start () {

		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		//Get score object
		score = GetComponent<Text>();
	}

	void Update () {

		//Display score
		score.text = ("x " + GameMaster.Score);
	}
	#endregion
}
