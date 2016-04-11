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

		//generate death particles
//		Instantiate(deathParticle, player.transform.position, player.transform.rotation);

//		Debug.Log("Player has Respawned!");
//		player.transform.position = currentCheckPoint.transform.position;

		//generate respawn particles
//		Instantiate(respawnParticles, player.transform.position, player.transform.rotation);
	}

	public IEnumerator RespawnPlayerCo() {
		//generate death particles
		Instantiate(deathParticle, player.transform.position, player.transform.rotation);

		//Hide Player
		player.enabled = false;
		player.GetComponent<Renderer> ().enabled = false;

		//Debug Message
		Debug.Log("Player has Respawned!");

		//Respawn Delay
		yield return new WaitForSeconds (respawnDelay);

		//Moves player to current check point
		player.transform.position = currentCheckPoint.transform.position;

		//Show player
		player.enabled = true;
		player.GetComponent<Renderer> ().enabled = true;

		//generate respawn particles
		Instantiate(respawnParticles, player.transform.position, player.transform.rotation);	
	}

}
