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

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetKeyDown (KeyCode.Space)&& grounded) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpheight);
		}

	}
}
