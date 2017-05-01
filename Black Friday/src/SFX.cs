using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour {

	// Variables
	public static SFX sfx;         			// Initialize instance of itself
	private AudioSource[] Sounds;			// Array of all attached audio sources

	// Sound Effects
	public AudioSource boss1Idle;
	public AudioSource boss1Attack;
	public AudioSource boss2AttackMelee;
	public AudioSource boss2AttackRanged;
	public AudioSource boss2Hurt;
	public AudioSource boss2Idle;
	public AudioSource boss3Hurt;
	public AudioSource boss3Idle;
	public AudioSource boss3AttackMelee;
	public AudioSource boss3AttackRanged;
	public AudioSource boss4Hurt;
	public AudioSource boss4AttackMelee;
	public AudioSource boss4AttackRanged;
	public AudioSource boss4GuardWhoosh;
	public AudioSource boss5Hurt;
	public AudioSource boss5AttackMelee;
	public AudioSource boss5AttackRanged;
	public AudioSource boss5Shield;
	public AudioSource boss5Target;
	public AudioSource coinPickup;
	public AudioSource error;
	public AudioSource healthPickup;
	public AudioSource itemBought;
	public AudioSource maleScream1;
	public AudioSource maleScream2;
	public AudioSource maleScream3;
	public AudioSource maleScream4;
	public AudioSource maleScream5;
	public AudioSource maleScream6;
	public AudioSource setScream1;
	public AudioSource setScream2;
	public AudioSource setScream3;
	public AudioSource femaleScream1;
	public AudioSource weaponBall;
	public AudioSource cartLoop;
	public AudioSource fire;
	public AudioSource fireExtinguisher;
	public AudioSource weaponClub;
	public AudioSource weaponGun;
	public AudioSource puff;
	public AudioSource punch;
	public AudioSource weaponSpoon;
	public AudioSource swish;
	public AudioSource thud;
	public AudioSource confirm1;
	public AudioSource confirm2;
	public AudioSource crowdGasp;
	public AudioSource crowdIntro;
	public AudioSource femaleScream2;
	public AudioSource femaleScream3;
	public AudioSource femaleScream4;
	public AudioSource uiClick1;

	private void Start () {

		// Get all attached audio sources
		Sounds = GetComponents<AudioSource>();

		// Assign audio sources
		boss1Idle = Sounds[0];
		boss1Attack = Sounds[1];
		boss2AttackMelee = Sounds[2];
		boss2AttackRanged = Sounds[3];
		boss2Hurt = Sounds[4];
		boss2Idle = Sounds[5];
		boss3Hurt = Sounds[6];
		boss3Idle = Sounds[7];
		boss3AttackMelee = Sounds[8];
		boss3AttackRanged = Sounds[9];
		boss4Hurt = Sounds[10];
		boss4AttackMelee = Sounds[11];
		boss4AttackRanged = Sounds[12];
		boss4GuardWhoosh = Sounds[13];
		boss5Hurt = Sounds[14];
		boss5AttackMelee = Sounds[15];
		boss5AttackRanged = Sounds[16];
		boss5Shield = Sounds[17];
		boss5Target = Sounds[18];
		coinPickup = Sounds[19];
		error = Sounds[20];
		healthPickup = Sounds[21];
		itemBought = Sounds[22];
		maleScream1 = Sounds[23];
		maleScream2 = Sounds[24];
		maleScream3 = Sounds[25];
		maleScream4 = Sounds[26];
		maleScream5 = Sounds[27];
		maleScream6 = Sounds[28];
		setScream1 = Sounds[29];
		setScream2 = Sounds[30];
		setScream3 = Sounds[31];
		femaleScream1 = Sounds[32];
		weaponBall = Sounds[33];
		cartLoop = Sounds[34];
		fire = Sounds[35];
		fireExtinguisher = Sounds[36];
		weaponClub = Sounds[37];
		weaponGun = Sounds[38];
		puff = Sounds[39];
		punch = Sounds[40];
		weaponSpoon = Sounds[41];
		swish = Sounds[42];
		thud = Sounds[43];
		confirm1 = Sounds[44];
		confirm2 = Sounds[45];
		crowdGasp = Sounds[46];
		crowdIntro = Sounds[47];
		femaleScream2 = Sounds[48];
		femaleScream3 = Sounds[49];
		femaleScream4 = Sounds[50];
		uiClick1 = Sounds[51];
	}

	// Method plays a given sound
	public void PlaySound (AudioSource sound) {
		sound.Play();
	}
}
