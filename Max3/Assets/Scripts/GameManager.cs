using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {



	// This section is to help manage our game objects as they go throughout the levels and to keep errors from occuring on the start of each stage.
	void Awake () 
	{
		// create conditional statement for verifying if instance already exists
		if (instance == null)
			// Set instance to this when instance doesn't exist
				instance = this;
		// create conditional statement for if instance doesn't exist AND is not this
		else if (instance != this)
			// if this condition is true, destroy the game object so only one instance exists
				Destroy (gameObject);

		//Set to do not destroy when reloading
		DontDestroyOnLoad (gameObject);
		//Assign enemy to enemy list
		willy = new List<Willy> ();
		// create a board manager reference
		boardScript = GetComponent<BoardManager>();
		// Begin first level
		InitGame();
	}
	//This section of the script is to help the transition of stages with title cards as the game progresses.
	void OnLevelWasLoaded (int index)

	{
		level++;

		InitGame ();
	}

	//create initiation method to help display what will be on the title cards for each transition of stages.
	void InitGame()
	{
		// prevent user input while doing setup
		doingSetup = true;
		// create reference to level image
		levelImage = GameObject.Find ("LevelImage");
		// set level text to the number of the level
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		// set leveltext to stage
		levelText.text = "S T A G E  " + level;
		// set level image to active for a few seconds in between levels
		levelImage.SetActive (true);
		// call hide level image function
		Invoke ("HideLevelImage", levelStartDelay);
		// Clear all enemy objects
		willy.Clear();
		//Apply scene level to applied set up scene method using the board manager reference
		boardScript.SetupScene (level);
	}

	//This section is to help hide the display card to start the stage.
	void HideLevelImage()
	{
		//set level image to false 
		levelImage.SetActive (false);
		// set setup to false so player can move
		doingSetup = false;
	}

	//This section is when the game is over and Treasure is depleted or the enmeies have taken back all of thier treasure.
	public void GameOver()
	{
		//display game over message
		levelText.text = "Lost all treasure on Stage " + level;
		//enable level image
		levelImage.SetActive (true);
		//disable game manager
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//create conditional statement to determine whether movers are moving or game is doing setup
		if (playersTurn || willyMoving || doingSetup)
						return;
		//move enemies
		StartCoroutine (MoveWilly ());
	}
	// add enemy method to increase the number of existing enemies
	public void AddWillyToList(Willy script)
	{
		// add enemy
		willy.Add (script);
	}
	// create method to move enemy
	IEnumerator MoveWilly()
	{
		// when enemy is moving, player cannot move
		willyMoving = true;
		// player must wait for enemy to finish moving
		yield return new WaitForSeconds(turnDelay);
		//Conditional statement for player movement if no enemy is present
		if (willy.Count == 0)
		{
			// set up time delay while no enemy is present
			yield return new WaitForSeconds(turnDelay);
		}
		// create for loop to cycle through enemies
		for (int i = 0; i < willy.Count; i++)
		{
			//Call move enemy function
			willy[i].MoveWilly();
			//move enemies one at a time
			yield return new WaitForSeconds(willy[i].moveTime);
		}
		// Set players turn to true
		playersTurn = true;
		// set enemy movement to false
		willyMoving = false;

	}
	//Declare variables
		//create variable for game start delay
	public float levelStartDelay = 2f;
		//create variable for turn delay
	public float turnDelay = .1f;
		//create instance of GameManager class, and set to null
	public static GameManager instance = null;

		//create variable for default treasure points and set to 100
	public int playerTreasurePoints = 100;
		// create hidden value to determine players turn
	[HideInInspector]public bool playersTurn = true;

		//create text to display the level
	private Text levelText;
		// create game object to select visual for menu screen
	private GameObject levelImage;
		// create board manager reference
	public BoardManager boardScript;	
		// create variable to set level 
	private int level = 1;
		// create enemy 
	private List<Willy> willy;
		// create conditional statement for seeing if enemy is moving
	private bool willyMoving;
		// create conditional statement for seeing if board is being set up
	private bool doingSetup;
}
