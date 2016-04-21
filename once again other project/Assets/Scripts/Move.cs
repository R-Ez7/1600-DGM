using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	//player movement
	public float moveSpeed;
	public float jumpheight;

	//Ground Check
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	//Double Jump Variable
	private bool doublePop;
	

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

	// Update is called once per frame
	void Update () {
		//players sprite is flip!!!!!!!!!!!!!!
		if (GetComponent<Rigidbody2D> ().velocity.x > 0)
			transform.localScale = new Vector3 (2f, 2f, 2f);

		else if (GetComponent<Rigidbody2D> ().velocity.x < 0)
			transform.localScale = new Vector3 (-2f, 2f, 2f);


		//this is for moving the character
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
		}
		// this is the only jump once
		if (Input.GetKeyDown (KeyCode.Space)&& grounded) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpheight);
		}

		//This is the double jump or pop of the character
		if (grounded)
			doublePop = false;
		//the character can not
		if (Input.GetKeyDown (KeyCode.Space)&& !doublePop && !grounded) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpheight);
			doublePop = true;
		}
	}
}
