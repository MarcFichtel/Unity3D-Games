using UnityEditor;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour {

	[MenuItem("Edit/Reset Playerprefs")]
	public static void DeletePlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}
}
