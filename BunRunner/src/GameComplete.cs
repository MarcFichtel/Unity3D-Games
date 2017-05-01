using UnityEngine;
using System.Collections;

//Game complete / Win screen class
public class GameComplete : MonoBehaviour {

	#region Variables

	//Game Over screen variables
	public static GameMaster gm;

	#endregion

	#region Start
	void Start () {
		
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}
	#endregion

	#region Methods
	//Retry previous level
	public void RetryLevel () {
		GameMaster.Score = 0;
		GameMaster.bunnyLives = 3;
		Application.LoadLevel (GameMaster.retryLevel);
	}

	//Go back to main menu on click
	public void BackToMainMenu () {
		Application.LoadLevel (0);
	}
	#endregion
}
