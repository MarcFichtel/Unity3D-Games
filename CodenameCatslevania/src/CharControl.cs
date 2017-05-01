using UnityEngine;
using System.Collections;
using System;

public class CharControl : MonoBehaviour {

	private float speed = 0.2f;
	private float jumpPower = 500.0f;
	private Rigidbody rb;
	public GameMaster gm;
	private Animator animator;
	public Shot shotPrefab;
	public CameraStuff cam;
	public SFX sfx;
	private bool jumping = false;
	private bool jumpReady = true;
	private bool crouching = false;
	private bool running = false;
	private bool movingRight = true;
	private bool attacking = false;
	private bool attackReady = true;
	private bool walled = false;
	private bool enterBoss = false;
	public bool fightingBoss = false;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void Start () {
		rb = GetComponent <Rigidbody> ();
		animator = GetComponentInChildren <Animator> ();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void FixedUpdate () {

		if (jumping && jumpReady) {
			jumpReady = false;
			rb.AddForce (new Vector3(0,jumpPower,0));
			if (this.name == "Cat" ||
				this.name == "Cat(Clone)" ||
				this.name == "JumperPlayer(Clone)") {
				sfx.playSound (sfx.jumpCat);
			} else if (this.name == "BumbleSpikePlayer(Clone)") {
				sfx.playSound (sfx.jumpBumble);
			} else if (this.name == "OverseerPlayer(Clone)") {
				sfx.playSound (sfx.jumpOverseer);
			}
		}

		if (fightingBoss) {
			jumpPower = 800;
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void Update () {

		animator.SetBool ("jumping", jumping);
		animator.SetBool ("running", running);
		animator.SetBool ("attacking", attacking);
		animator.SetBool ("walled", walled);

		Vector3 pos = transform.position;

		if ((Input.GetKeyDown (KeyCode.W) ||
		    Input.GetKeyDown (KeyCode.UpArrow)) &&
			jumpReady && !jumping) {
			jumping = true;
		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			running = true;
			if (!crouching) {
				pos.x += speed;
			} else {
				pos.x += speed / 2;
			}
			if (!movingRight) {
				movingRight = true;
			}

		} else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			running = true;
			if (!crouching) {
				pos.x -= speed;
			} else {
				pos.x -= speed / 2;
			}
			if (movingRight) {
				movingRight = false;
			}
		} else {
			running = false;
		}

		if (enterBoss == true) {
			enterBoss = false;
			print ("Test");
			pos = new Vector3 (155, 115, -2);
		}

		Vector3 scl = transform.localScale;

		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			if (this.name == "BoomSkullPlayer(Clone)") {
				pos.y -= speed;
			}
			crouching = true;
			scl.y = 0.4f;
		} else {
			crouching = false;
			scl.y = 0.8f;
		}

		if (movingRight) {
			scl.x = 0.8f;
		} else {
			scl.x = -0.8f;
		}

		transform.localScale = scl;

		if (transform.position.y <= -20) {
			gm.killPlayer (this.transform.position, this);
		}

		transform.position = pos;

		/////////////////////////////////////////////////////////////////////////////////

		if (Input.GetKey (KeyCode.Space) &&
			attackReady) {
			attacking = true;
			attackReady = false;
			if (this.name == "Cat" ||
				this.name == "Cat(Clone)") {
				StartCoroutine (catAttack ());
			} else if (this.name == "OverseerPlayer(Clone)") {
				StartCoroutine (overseerAttack (movingRight));
			} else if (this.name == "BoomSkullPlayer(Clone)") {
				boomAttack ();
			} else if (this.name == "JumperPlayer(Clone)") {
				jumpAttack ();
			} else if (this.name == "BumblePlayer(Clone)") {
				sfx.playSound (sfx.attack2);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////

		Vector3 vel = rb.velocity;

		if (vel.x != 0) {
			vel.x = 0;
		}

		rb.velocity = vel;

		/////////////////////////////////////////////////////////////////////////////////


	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public bool getJumping () {
		return jumping;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public IEnumerator catAttack () {
		sfx.playSound (sfx.attack1);
		yield return new WaitForSeconds (1.0f);
		attacking = false ;
		yield return new WaitForSeconds (1.0f);
		attackReady = true;
	}

	public IEnumerator overseerAttack (bool facingRight) {
		sfx.playSound (sfx.overseerSomething);
		Shot shot = Instantiate (shotPrefab, this.transform.position, Quaternion.Euler (0,180,90)) as Shot;
		if (movingRight) {
			shot.setMovingRight (true);
		} else {
			shot.setMovingRight (false);
		}
		yield return new WaitForSeconds (0.5f);
		attacking = false ;
		yield return new WaitForSeconds (1.5f);
		attackReady = true;
	}

	public void boomAttack () {
		sfx.playSound (sfx.splosion);
		gm.showExplosion (transform.position);
		if (!enterBoss) {
			gm.transformPlayer (null, this, transform.position);
		} else {
			enterBoss = false;
			gm.transformPlayer (null, this, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z));
		}
	}

	public void jumpAttack () {
		sfx.playSound (sfx.attack1);
		jumping = true;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void OnCollisionEnter (Collision c) {

		if (c.gameObject.tag == "Ground") {
			jumping = false;
			jumpReady = true;
			attacking = false ;
			attackReady = true;
		}

		if (c.gameObject.tag == "Collectable") {
			gm.changeScore (1);
			Destroy (c.gameObject);
		}

		if (c.gameObject.tag == "Projectile") {
			Destroy (c.gameObject);
			gm.healthHurt ();
			if (gm.getHealth () > 0) {
				rb.AddForce (new Vector3 (0, jumpPower, 0));
			} else {
				gm.killPlayer (transform.position, this);
			}
		}

		if (c.gameObject.tag == "Enemy") {
			if (!attacking) {
				sfx.playSound (sfx.hurt);
				gm.healthHurt ();
				if (c.gameObject.name == "BoomSkullEnemy" ||
					c.gameObject.name == "BoomSkullEnemy(Clone)") {
					gm.showExplosion (c.transform.position);
					Destroy (c.gameObject);
				}
				if (gm.getHealth () > 0) {
					rb.AddForce (new Vector3 (0, jumpPower, 0));
				} else {
					sfx.playSound (sfx.death1);
					gm.killPlayer (transform.position, this);
				}
			} else {
				Destroy (c.gameObject);
				gm.transformPlayer (c.gameObject.name, this, transform.position);
			}
		}

		if (c.gameObject.tag == "Wall") {
			if (this.name == "JumperPlayer(Clone)") {
				walled = true;
			} else if (this.name == "BoomSkullPlayer(Clone)") {
				enterBoss = true;
				fightingBoss = true;
				boomAttack ();
			}
		}

		if (c.gameObject.tag == "Boss") {
			if (attacking) {
				BossProjectile bossShot = c.gameObject.GetComponent <BossProjectile> ();
				bossShot.towardsPlayer = false;
			} else {
				sfx.playSound (sfx.hurt);
				gm.healthHurt ();
				Destroy (c.gameObject);
				if (gm.getHealth () > 0) {
					rb.AddForce (new Vector3 (0, jumpPower, 0));
				} else {
					sfx.playSound (sfx.death1);
					gm.killPlayer (transform.position, this);
				}
			}
		}
	}

	public void transformIntoJumper (GameObject enemy) {
		Destroy (enemy);
		gm.transformPlayer (enemy.name, this, transform.position);
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void OnCollisionExit (Collision c) {
		if (c.gameObject.tag == "Wall") {
			if (this.name == "JumperPlayer(Clone)") {
				walled = false;
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void setGM (GameMaster gm) {
		this.gm = gm;
	}

	public void setSFX (SFX sfx) {
		this.sfx = sfx;
	}

	public void setJumpPower (float value) {
		jumpPower = value;
	}
}
