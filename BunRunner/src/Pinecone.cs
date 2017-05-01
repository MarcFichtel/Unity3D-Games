using UnityEngine;
using System.Collections;

//Pinecones on boss 1 level class
public class Pinecone : MonoBehaviour {

	#region Variables
	//Pinecone variables
	public static GameMaster gm;
	public BunnyStats bunny;
	public bool falling = false;
	public float fallSpeed = 1f;
	private Vector2 startingPos;
	public bool respawning = false;
	private Rigidbody2D pineconeRB;
	#endregion

	#region Start
	void Start () {
		startingPos = transform.position;
	}
	#endregion

	#region Update
	void Update () {

		//Get gamemaster
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent <GameMaster> ();
		}
		
		//Get bunny
		if (bunny == null) {
			bunny = gm.bunny;
		}

		//Get rigidbody
		pineconeRB = GetComponent <Rigidbody2D> ();

		//Get position
		Vector3 temp = transform.position;

		//Pinecone falls if falling is true
		if (falling == true) {
			temp.y -= fallSpeed;
		}

		//Respawn one pinecone
		if (temp.y <= 0 && 
		    respawning == false) {
			respawning = true;
			StartCoroutine (gm.respawnPinecone (startingPos));
		}

		//Destroy pinecone offscreen after the respawn coroutine finished
		if (temp.y <= -150) {
			Destroy(this.gameObject);
		}

		//Set new position
		transform.position = temp;
	}
	#endregion

	#region Collisions
	void OnCollisionEnter2D (Collision2D pineconeCollision) {

		//Pinecone starts falling if bunny touches it
		if (pineconeCollision.gameObject.tag == "Player") {
			falling = true;
			this.name = "Pinecone (falling)";
			bunny.bunnyRB.velocity = bunny.enemyForce;
		}

		//Destroy pinecone if it touches the ground
		if (pineconeCollision.gameObject.tag == "Ground") {
			Destroy (pineconeRB);
		}
	}
	#endregion
}