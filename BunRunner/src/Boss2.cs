using UnityEngine;
using System.Collections;

//Boss 2 class
public class Boss2 : MonoBehaviour {

	#region Variables
	//Spider variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public GameObject webPrefab;	
	public Transform webSpawnPoint;
	private float spiderBoundaryLeft = 59.5f;
	private float spiderBoundaryRight = 67.5f;	
	private float spiderBoundaryTop = 13f;
	private float spiderBoundaryBottom = 4f;	
	private float spiderSpeedHorizontal = 0.05f;	
	private float spiderSpeedVertical = 0.1f;		
	private int spiderDamage = 50;
	private bool shootingWebGround = false;
	private bool shootingWebAir = false;
	private bool jumping = false;
	private bool readyToJump = true;
	private Vector3 temp;
	private Transform endStalagtite;
	public bool defeated = false;
	#endregion

	#region Start
	void Start () {
	
		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}

		//Get animator
		//TODO animator = GetComponent <Animator> ();

		//Get track end
		if (endStalagtite == null) {
			endStalagtite = GameObject.FindGameObjectWithTag ("Finish").GetComponent <Transform> ();
		}
	}
	#endregion

	#region Update, FixedUpdate
	void FixedUpdate () {

		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}
	}

	void Update () {
	
		//Set defeated to true if end stalagtite has fallen far enough
		if (endStalagtite.transform.position.y <= -10 &&
		    endStalagtite != null) {
			defeated = true;
		}

		//Get boss position & velocity
		temp = transform.position;
		

		//Boss paces between spiderBoundaryLeft and spiderBoundaryRight while on the ground
		if (jumping == false) {

			//Make spider pace in boundary range
			temp.x += spiderSpeedHorizontal;
		}

		//Turn spider around if it has paced far enough
		if (temp.x >= spiderBoundaryRight) {
			spiderSpeedHorizontal *= -1;
		} else if (temp.x <= spiderBoundaryLeft) {
			spiderSpeedHorizontal *= -1;
		}

		//Boss jumps between spiderBoundaryTop and spiderBoundaryBottom
		if (jumping == true) {
			
			//Make spider jump in boundary range
			temp.y += spiderSpeedVertical;
		}

		//Jump concludes once bottom boundary is reached
		if (temp.y >= spiderBoundaryTop) {
			spiderSpeedVertical *= -1;
		} else if (temp.y <= spiderBoundaryBottom && 
		           jumping == true) {
			jumping = false;
			spiderSpeedVertical *= -1;
		}

		//Apply movement if boss isn't defeated
		if (defeated == false) {
			transform.position = temp;
		}

		//Boss shoots periodically while on the ground
		if (jumping == false &&
			shootingWebGround == false &&
		    defeated == false) {
			shootingWebGround = true;
			StartCoroutine(shootWeb());
		}

		//Spider shoots once at height of top lane while jumping
		if (temp.y >= spiderBoundaryTop - 2.5f && 
			shootingWebAir == false &&
		    defeated == false) {
			shootingWebAir = true;
			StartCoroutine(shootWeb());
		}

		//Spider jumps periodically
		if (jumping == false &&
		    readyToJump == true) {
			jumping = true;
			readyToJump = false;
			StartCoroutine(spiderJump());
		}

		//Destroy spider offscreen
		if (temp.y <= -2) {
			Destroy (this.gameObject);
			gm.boss2DefeatSound.Play();
		}

		//Boss falls slowly once defeated
		if (defeated == true) {
			Vector3 tempDefeated = transform.position;
			tempDefeated.y -= 0.02f;
			transform.position = tempDefeated;
		}
	}
	#endregion

	#region Collisions
	//Damage bunny if it touches boss
	void OnCollisionEnter2D (Collision2D touchSpider) {
		if (touchSpider.gameObject.tag == "Player") {
			bunny.DamageBunny (spiderDamage);
		}
	}
	#endregion

	#region Methods
	//Shoot web
	private IEnumerator shootWeb() {
		Instantiate (webPrefab, webSpawnPoint.position, Quaternion.Euler(0, 0, 90));
		gm.webSound.Play ();
		yield return new WaitForSeconds (Random.Range (5, 10));
		shootingWebAir = false;
		shootingWebGround = false;
	}

	//Wait some time before spider jump can occur again
	private IEnumerator spiderJump () {
		yield return new WaitForSeconds (Random.Range(5,10));
		readyToJump = true;
	}
	#endregion
}
