using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {


	//movement variables
	public float movespeed;
	public bool moveRight;

	//wall check
	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;

	public float edgeCheckRadius;


	//edge check
	private bool notAtEdge;
	public Transform edgeCheck;

	
	// Update is called once per frame
	void Update () {
		notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, edgeCheckRadius, whatIsWall);

		hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

		if (hittingWall || !notAtEdge) {
			moveRight = !moveRight;
		}
			
		if (moveRight) {
	///this is the size of the enemy
			transform.localScale = new Vector3	(-1f,1f,1f);
			GetComponent<Rigidbody2D>().velocity = new Vector2(movespeed,GetComponent<Rigidbody2D>().velocity.y);
		}
		else {
			transform.localScale = new Vector3	(1f,1f,1f);
			GetComponent<Rigidbody2D>().velocity = new Vector2(-movespeed,GetComponent<Rigidbody2D>().velocity.y);
		}
	}
}
