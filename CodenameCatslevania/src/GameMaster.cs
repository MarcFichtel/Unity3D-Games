using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class GameMaster : MonoBehaviour {

	private int score = 0;
	private int health = 100;
	public Text scoreText;
	public Text healthText;
	public DeathParticles deathParticles;
	public CharControl Cat;
	public CharControl Bumble;
	public CharControl Overseer;
	public CharControl BombSkull;
	public CharControl jumper;
	public SFX sfx;
	private CameraStuff cam;
	public Transform bombSpawnPoint;
	public BombSkullEnemy bombEnemy;

	void Start () {
		cam = GameObject.Find ("Cam").GetComponent <CameraStuff>();
	}

	void Update () {

		BombSkullEnemy[] bombEnemies = GameObject.FindObjectsOfType <BombSkullEnemy> ();
		if (bombEnemies.Length == 0 &&
			GameObject.Find ("BoomSkullPlayer(Clone)") == null) {
			Instantiate (bombEnemy, bombSpawnPoint.position, Quaternion.identity); 
		}
	}

	public void ShowDeathParticles (Vector3 pos) {
		Instantiate (deathParticles, pos, Quaternion.identity);
	}

	public void changeScore (int value) {
		score += value;
		scoreText.text = "Score: " + score;
	}

	public void StartGame () {
		SceneManager.LoadScene (1);
	}

	public void GameWon () {
		SceneManager.LoadScene (2);
	}

	public void LoadMenu () {
		SceneManager.LoadScene (0);
	}

	public void killPlayer (Vector3 playerPos, CharControl player) {
		ShowDeathParticles (playerPos);
		StartCoroutine (respawn());
		Destroy (player.gameObject);
	}

	public int getHealth() {
		return health;
	}

	public void showExplosion (Vector3 pos) {
		Instantiate (deathParticles, pos, Quaternion.identity);
	}

	public IEnumerator respawn () {
		yield return new WaitForSeconds (3.0f);
		StartGame ();
	}

	public void healthHurt () {
		health -= 10;
		healthText.text = "Health: " + health;
	}

	public void transformPlayer (string enemy, CharControl player, Vector3 pos) {

		if (enemy == "BumblespikeEnemy") {
			CharControl BumblePlayer = Instantiate (Bumble, pos, Quaternion.Euler (0, 180, 0)) as CharControl;
			cam.setTarget (BumblePlayer);
			BumblePlayer.setGM (this);
			BumblePlayer.setSFX (sfx);

			BumblespikeEnemy[] bumbleEnemies = GameObject.FindObjectsOfType <BumblespikeEnemy> ();
			foreach (BumblespikeEnemy bumbleEnemy in bumbleEnemies) {
				bumbleEnemy.setTarget (BumblePlayer.transform);
			}
			BombSkullEnemy[] bombEnemies = GameObject.FindObjectsOfType <BombSkullEnemy> ();
			foreach (BombSkullEnemy bomb in bombEnemies) {
				bomb.setTarget (BumblePlayer.transform);
			}
			JumperEnemy[] jumpers = GameObject.FindObjectsOfType <JumperEnemy> ();
			foreach (JumperEnemy jump in jumpers) {
				jump.setTarget (BumblePlayer.transform);
			}

		} else if (enemy == "OverseerEnemy") {
			CharControl OverseerPlayer = Instantiate (Overseer, pos, Quaternion.Euler (0, 180, 0)) as CharControl;
			cam.setTarget (OverseerPlayer);
			OverseerPlayer.setGM (this);
			OverseerPlayer.setSFX (sfx);
			OverseerPlayer.setJumpPower (600.0f);

			BumblespikeEnemy[] bumbleEnemies = GameObject.FindObjectsOfType <BumblespikeEnemy> ();
			foreach (BumblespikeEnemy bumbleEnemy in bumbleEnemies) {
				bumbleEnemy.setTarget (OverseerPlayer.transform);
			}
			BombSkullEnemy[] bombEnemies = GameObject.FindObjectsOfType <BombSkullEnemy> ();
			foreach (BombSkullEnemy bomb in bombEnemies) {
				bomb.setTarget (OverseerPlayer.transform);
			}
			JumperEnemy[] jumpers = GameObject.FindObjectsOfType <JumperEnemy> ();
			foreach (JumperEnemy jump in jumpers) {
				jump.setTarget (OverseerPlayer.transform);
			}

		} else if (enemy == "BoomSkullEnemy" ||
			enemy == "BoomSkullEnemy(Clone)") {
			CharControl BombSkullPlayer = Instantiate (BombSkull, pos, Quaternion.Euler (270, 90, 90)) as CharControl;
			cam.setTarget (BombSkullPlayer);
			BombSkullPlayer.setGM (this);
			BombSkullPlayer.setSFX (sfx);

			BumblespikeEnemy[] bumbleEnemies = GameObject.FindObjectsOfType <BumblespikeEnemy> ();
			foreach (BumblespikeEnemy bumbleEnemy in bumbleEnemies) {
				bumbleEnemy.setTarget (BombSkullPlayer.transform);
			}
			BombSkullEnemy[] bombEnemies = GameObject.FindObjectsOfType <BombSkullEnemy> ();
			foreach (BombSkullEnemy bomb in bombEnemies) {
				bomb.setTarget (BombSkullPlayer.transform);
			}
			JumperEnemy[] jumpers = GameObject.FindObjectsOfType <JumperEnemy> ();
			foreach (JumperEnemy jump in jumpers) {
				jump.setTarget (BombSkullPlayer.transform);
			}
		
		} else if (enemy == "JumperEnemy") {
			CharControl jumperPlayer = Instantiate (jumper, pos, Quaternion.Euler(0,180,0)) as CharControl;
			cam.setTarget (jumperPlayer);
			jumperPlayer.setGM (this);
			jumperPlayer.setSFX (sfx);
			jumperPlayer.setJumpPower (1000.0f);

			BumblespikeEnemy[] bumbleEnemies = GameObject.FindObjectsOfType <BumblespikeEnemy> ();
			foreach (BumblespikeEnemy bumbleEnemy in bumbleEnemies) {
				bumbleEnemy.setTarget (jumperPlayer.transform);
			}
			BombSkullEnemy[] bombEnemies = GameObject.FindObjectsOfType <BombSkullEnemy> ();
			foreach (BombSkullEnemy bomb in bombEnemies) {
				bomb.setTarget (jumperPlayer.transform);
			}
			JumperEnemy[] jumpers = GameObject.FindObjectsOfType <JumperEnemy> ();
			foreach (JumperEnemy jump in jumpers) {
				jump.setTarget (jumperPlayer.transform);
			}

		} else if (enemy == null) {
			CharControl CatPlayer = Instantiate (Cat, pos, Quaternion.Euler(0,180,0)) as CharControl;
			cam.setTarget (CatPlayer);
			CatPlayer.setGM (this);
			CatPlayer.setSFX (sfx);

			BumblespikeEnemy[] bumbleEnemies = GameObject.FindObjectsOfType <BumblespikeEnemy> ();
			foreach (BumblespikeEnemy bumbleEnemy in bumbleEnemies) {
				bumbleEnemy.setTarget (CatPlayer.transform);
			}
			BombSkullEnemy[] bombEnemies = GameObject.FindObjectsOfType <BombSkullEnemy> ();
			foreach (BombSkullEnemy bomb in bombEnemies) {
				bomb.setTarget (CatPlayer.transform);
			}
			JumperEnemy[] jumpers = GameObject.FindObjectsOfType <JumperEnemy> ();
			foreach (JumperEnemy jump in jumpers) {
				jump.setTarget (CatPlayer.transform);
			}
		}

		sfx.playSound (sfx.transformation);
		Destroy (player.gameObject);
	}
}
