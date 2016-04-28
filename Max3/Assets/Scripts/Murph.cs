using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//inherit from mover script
public class Murph : Movers {
	
	//This section is to help manage the treasure as each stage continues
	protected override void Start () 
	{
		// create reference to animator
		animator = this.GetComponent<Animator> ();
		// create reference to amount of treasure points
		treasure = GameManager.instance.playerTreasurePoints;
		// dispay amount of treasure points
		treasureText.text= "Treasure: " + treasure; 
		// call start function of movers
		base.Start();

	}

	//This is to display the treasure that was gathered and how much was in the inventory.
	private void OnDisable()
	{
		// store treasure total in Game Manager
		GameManager.instance.playerTreasurePoints = treasure;
	}
	
	// This section is to help the character face the direction it is going to.
	void Update () {
		//  Create variable to determine facing vertical
		var vertical = Input.GetAxis ("Vertical");
		// create variable to determine facing horizontal
		var horizontal = Input.GetAxis ("Horizontal");
		// exit this function if it's not players turn
		if (!GameManager.instance.playersTurn)
						return;
		// Create variables to move horizontal directions and set to 0
		int hor = 0;
		// Create variables to move vertical directions and set to 0
		int vert = 0;
		// receive player input and set to an integer and store in horizontal
		hor = (int)Input.GetAxisRaw ("Horizontal");
		// receive player input and set to an integer and store in vertical
		vert = (int)Input.GetAxisRaw ("Vertical");

		// create conditional statement to determine whether player is moving horizontally
		if (hor != 0)
			//set vertical to 0 when moving horizontally
			vert = 0;

		// create conditional statement to determine whether horizontal or vertical position is anything but 0
		if (hor != 0 || vert != 0)
			// if above condition is true, call attempt move method and pass in horizontal and vertical methods
						AttemptMove <Wall> (hor, vert);

		// create conditional statement to determine facing up
		if (vertical > 0)						
		{
			animator.SetInteger("Direction", 2);
		}
		// create conditional statement to determine facing down
		if (vertical < 0)
		{
			animator.SetInteger("Direction", 0);
		}
		// create conditional statement to determine facing left
		if (horizontal < 0)
		{
			animator.SetInteger("Direction", 1);
		}
		// create conditional statement to determine facing right
		if (horizontal > 0)
		{
			animator.SetInteger("Direction", 3);
		}
	}

	//This section is for the game expericene as the player loses treasure trying to escape the enemy.
	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		// lose treasure by one piece every time player moves
		treasure--;
		// display current amount of treasure
		treasureText.text = "Treasure: " + treasure;

		//call attempt move function
		base.AttemptMove <T> (xDir, yDir);
		// check if game over
		CheckIfGameOver ();
		// after players turn is over, set player turn to false
		GameManager.instance.playersTurn = false;
	}

	//This section is the Treasure that is collected from the board and giving the points.
	private void OnTriggerEnter2D (Collider2D other)
	{
		//create conditional statement to determine if treasure piece is exit piece
		if (other.tag == "Exit")
		{	
			// set sound to play treasure sound
			Sounds.instance.PlayClips(treasureSound);
			//invoke the restartleveldelay
			Invoke ("Restart", restartLevelDelay);
			//disable player object
			enabled = false;
		}
		//create conditional statement to determine if treasure piece is a ruby
		else if (other.tag == "Treasure1")
		{
			// increase treasure points if true
			treasure += pointsPerTreasure1;
			//display amount of treasure
			treasureText.text = "+" + pointsPerTreasure1 + " Treasure: " + treasure;
			// set sound to play treasure sound
			Sounds.instance.PlayClips(treasureSound);
			// disable contacted ruby piece
			other.gameObject.SetActive (false);
		}
		//create conditional statement to determind if treasure piece is a gold
		else if (other.tag == "Treasure2")
		{
			// increase treasure points if true
			treasure += pointsPerTreasure2;
			// display amount of treasure
			treasureText.text = "+" + pointsPerTreasure2 + " Treasure: " + treasure;
			// set sound to play treasure
			Sounds.instance.PlayClips(treasureSound);
			// disable contacted gold piece
			other.gameObject.SetActive (false);
		}

	}

	//This section is the interaction with the rock object that can be brokenx
	protected override void OnCantMove <T> (T component)
	{
		// set hitwall to wall
		Wall hitWall = component as Wall;
		// call damagewall function
		hitWall.DamageWall (wallDamage);
	}


	//This section is to help respwan the next stage, whihc in this case an endless on going stage.
	private void Restart()
	{
		//load last scene
		Application.LoadLevel (Application.loadedLevel);
	}

	//This section is the loss of treasure that was taken back from the player by the enemy.
	public void LoseTreasure (int loss)
	{
		//subtract food points from total
		treasure -= loss;
		//display amount of lost treasure
		treasureText.text = "-" + loss + " Treasure: " + treasure;
		// check if game over
		CheckIfGameOver();
	}

	//This section just notifies the player that the game is over.
	private void CheckIfGameOver()
	{
		//create conditional statement to determine if treasure is less than or equal to 0
		if (treasure <= 0)
		{
			// turn off music
			Sounds.instance.musicSource.Stop ();
			// call game over method
			GameManager.instance.GameOver();
		}
	}
	// public variables for the player controller
	// declare and set wall damage to 1
	public int wallDamage = 1;
	// declare and set ruby points to 20
	public int pointsPerTreasure1 = 20;
	// declare and set gold points to 10
	public int pointsPerTreasure2 = 10;
	// declare restart delay and set time
	public float restartLevelDelay = 1f;
	// declare treasure text variable
	public Text treasureText;
	// declare audioclip treasuresound
	public AudioClip treasureSound;
	//private variables for the player controller
	// declare animator
	private Animator animator;
	// declare treasure
	private int treasure;
}
