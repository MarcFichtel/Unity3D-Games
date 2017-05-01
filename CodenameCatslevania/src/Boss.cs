using UnityEngine;
using System.Collections;
//using UnityEditor;

public class Boss : MonoBehaviour {

	public BossProjectile shot;
	public Transform boundRight;
	public Transform boundLeft;
	public Transform boundTop;
	public Transform boundBottom;
	private float speed = 0.2f;
	private bool movingUp = false;
	private bool movingDown = false;
	private bool movingLeft = false;
	private bool movingRight = true;
	private bool attacking = false;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 pos = transform.position;

		if (movingUp) {
			pos.y += speed;
		} else if (movingDown) {
			pos.y -= speed;
		} else if (movingRight) {
			pos.x += speed;
		} else if (movingLeft) {
			pos.x -= speed;
		}

		if (movingRight && pos.x > boundRight.position.x) {
			movingRight = false;
			movingDown = true;
		}
		if (movingDown && pos.y < boundBottom.position.y) {
			movingDown = false;
			movingLeft = true;
		}
		if (movingLeft && pos.x < boundLeft.position.x) {
			movingLeft = false;
			movingUp = true;
		}
		if (movingUp && pos.y > boundTop.position.y) {
			movingUp = false;
			movingRight = true;
		}

		transform.position = pos;

		if (!attacking) {
			StartCoroutine (attack ());
		}


	}

	public IEnumerator attack () {
		attacking = true;
		Instantiate (shot, 
			new Vector3(
			transform.position.x, 
			transform.position.y, 
			transform.position.z - 5), 
			Quaternion.identity); 
		yield return new WaitForSeconds (1.0f);
		attacking = false;
	}
}
