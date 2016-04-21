using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public Move player;

	public GameObject enemyDeath;

	public GameObject projectileParticle;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Move> ();

		//shoots the direction right
		if (player.transform.localScale.x < 0)
			speed = -speed;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed,GetComponent<Rigidbody2D> ().velocity.y );
	}

	void OnTriggerEnter2D(Collider2D other){
	if (other.tag == "Enemy"){
		Instantiate (enemyDeath, other.transform.position, other.transform.rotation);
		Destroy (other.gameObject);
//		ScoreManager.AddPoints (pointsForKill);
	}

		Instantiate (projectileParticle, transform.position, transform.rotation);
		Destroy (gameObject);

	}
		
}
