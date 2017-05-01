using UnityEngine;
using System.Collections;

public class BossProjectile : MonoBehaviour {

	private float speed = 0.2f;
	public bool towardsPlayer = true;
	public GameMaster gm;
	private Transform boss;

	void Start () {
		boss = GameObject.Find ("Boss").GetComponent <Transform> ();
		gm = GameObject.Find ("GameMaster").GetComponent <GameMaster> ();
	}

	void Update () {
	
		Vector3 pos = transform.position;

		if (towardsPlayer) {
			pos.z -= speed;
		} else {
			speed = 15.0f;
			pos = Vector3.MoveTowards (transform.position, boss.position, speed * Time.deltaTime);
		}

		if (pos.z >= 25) {
			StartCoroutine (gameDone());
		}

		if (pos.z < -20) {
			Destroy (this.gameObject); 
		}

		transform.position = pos;

	}

	public void OnCollisionEnter (Collision c) {

		if (c.gameObject.name == "Boss") {
			Destroy (c.gameObject);
			StartCoroutine (gameDone());
		}
	}

	public IEnumerator gameDone() {
		gm.showExplosion (transform.position);
		yield return new WaitForSeconds (0.1f);
		gm.showExplosion (transform.position);
		yield return new WaitForSeconds (0.1f);
		gm.showExplosion (transform.position);
		yield return new WaitForSeconds (0.1f);
		gm.showExplosion (transform.position);
		yield return new WaitForSeconds (0.1f);
		gm.showExplosion (transform.position);
		yield return new WaitForSeconds (0.1f);
		gm.GameWon ();
	}
}
