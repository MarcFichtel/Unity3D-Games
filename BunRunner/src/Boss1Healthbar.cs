using UnityEngine;
using System.Collections;

//Boss 1 healthbar class
public class Boss1Healthbar : MonoBehaviour {

	#region Variables
	//Boss 1 healthbar variables
	public Boss1 boss;
	private int maxHealth = 100;
	private int currentHealth;
	private RectTransform healthbar;
	private Vector3 healthbarScale;
	private float healthPercentage = 1f;
	#endregion

	#region Start, Update
	void Start () {

		//Get healthbar rectangle transform
		healthbar = GetComponent <RectTransform> ();
	}

	void Update () {

		//Get boss health & percentage
		currentHealth = boss.bossHealth;
		healthPercentage = (float)currentHealth / (float)maxHealth;

		//Width of healthbar equals percentage of boss health
		healthbarScale = healthbar.transform.localScale;
		healthbarScale.x = healthPercentage;
		healthbar.transform.localScale = healthbarScale;
	}
	#endregion
}
