using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckPoint;
	private Move player;


	//particles
	public GameObject deathParticle;
	public GameObject respawnParticles;

	//respawn delay
	public float respawnDelay;

	//Store Gravity Value
	private float gravityStore;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Move> ();
	}



	// Update is called once per frame
	void Update () {
	
	}

	//Respawn Player
	public void RespawnPlayer(){
		StartCoroutine ("RespawnPlayerCo");


	}

	public IEnumerator RespawnPlayerCo() {
		//generate death particles
		Instantiate(deathParticle, player.transform.position, player.transform.rotation);

		//Hide Player
		player.enabled = false;
		player.GetComponent<Renderer> ().enabled = false;

		//Gravity Reset
		gravityStore = player.GetComponent<Rigidbody2D> ().gravityScale;
		player.GetComponent<Rigidbody2D> ().gravityScale = 0f;
		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		//Debug Message
		Debug.Log("Player has Respawned!");

		//Respawn Delay
		yield return new WaitForSeconds (respawnDelay);

		//Gravity Restore
		player.GetComponent<Rigidbody2D>().gravityScale = 1;


		//Moves player to current check point
		player.transform.position = currentCheckPoint.transform.position;

		//Show player
		player.enabled = true;
		player.GetComponent<Renderer> ().enabled = true;

		//generate respawn particles
		Instantiate(respawnParticles, player.transform.position, player.transform.rotation);	
	}

}
