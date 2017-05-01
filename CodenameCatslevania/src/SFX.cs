using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour {

	private AudioSource[] sounds;
	public AudioSource songIntro;
	public AudioSource songLoop;
	public AudioSource jumpCat;
	public AudioSource jumpBumble;
	public AudioSource jumpOverseer;
	public AudioSource splosion;
	public AudioSource attack1;
	public AudioSource attack2;
	public AudioSource hurt;
	public AudioSource transformation;
	public AudioSource charge;
	public AudioSource death1;
	public AudioSource death2;
	public AudioSource overseerSomething;

	// Get sounds
	private void Start () {
		sounds = GetComponents <AudioSource> ();
		songIntro = sounds [0];
		songLoop = sounds [1];
		jumpCat = sounds [2];
		jumpBumble = sounds [3];
		jumpOverseer = sounds [4];
		splosion = sounds [5];
		attack1 = sounds [6];
		attack2 = sounds [7];
		hurt = sounds [8];
		transformation = sounds [9];
		charge = sounds [10];
		death1 = sounds [11];
		death2 = sounds [12];
		overseerSomething = sounds [13];
	}

	void Update () {
		if (!songIntro.isPlaying &&
			!songLoop.isPlaying) {
			songLoop.Play ();
		}
	}

	public void playSound (AudioSource sound) {
		sound.Play ();
	}

}
