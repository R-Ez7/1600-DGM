using UnityEngine;
using System.Collections;

public class Willy : Movers {

	public int playerDamage;



	private Transform target;
	private bool skipMove;				//We need to add this feature to make it less difficult and posible to get into the hight stages.
	public AudioClip fightSound;


	// Use this for initialization
	protected override void Start () 
	{
		GameManager.instance.AddWillyToList (this);
//		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start();
	}
	

	//this is part of the feature we needed to add so the enemy does not over run us.
	//This section is used to keep the enemy moved in turns.
	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		if (skipMove)
		{
			skipMove = false;
			return;
		}

		base.AttemptMove <T> (xDir, yDir);

		skipMove = true;
	}


	//This section is used for the direction the enemy will need to go, which is to the player.
	public void MoveWilly()
	{
		int xDir = 0;
		int yDir = 0;
	
		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
					yDir = target.position.y > transform.position.y ? 1 : -1;
		else
					xDir = target.position.x > transform.position.x ? 1 : -1;

		AttemptMove <Murph> (xDir, yDir);
	}

	//This section is to prevent the enemy from overlapping and taking over our slot, whihc causes the losted or robbed treasure to be taken back.
	protected override void OnCantMove <T> (T component)
	{
		Murph hitPlayer = component as Murph;
		hitPlayer.LoseTreasure (playerDamage);
		Sounds.instance.RandomizeSfx (fightSound);
	}
}
