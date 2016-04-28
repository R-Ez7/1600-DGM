using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	
	// Use this for initialization
	void Awake () 
	{
	}

	//create damageWall function for visual, audio, and logical aspects of rock breaking
	//this function used to help player character escape randomly generated rock formation traps
	public void DamageWall (int loss)
	{
		//Set audio for rocks breaking
		Sounds.instance.PlayClips (crackSound);
		//Set event for rock hp loss
		hp -= loss;
		//Set criteria for rock break using a boolean conditional statement
		if (hp <= 0)
			gameObject.SetActive (false);
	}
	//Declare variables
		//Declare variable for hp of rock objects
	public int hp = 1;
		// Declare variable for rock breaking sound
	public AudioClip crackSound;
}





