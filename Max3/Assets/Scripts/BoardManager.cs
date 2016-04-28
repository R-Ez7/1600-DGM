using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

// Create board manager for randomly generating game board
public class BoardManager : MonoBehaviour {

	// Set to Serializable to embed class into the inspector
	[Serializable]
	//Create counting class
	public class Count
	{
		//Declare minimum and maximum variables for the Counting class
		public int minimum;
		public int maximum;

		//Create parameterized constructor for Counting class
		public Count  (int min, int max)
		{
			// Pass class variables through constructor
			minimum = min;
			maximum = max;
		}
	}
	
	// create initialize list method to empty gridpositions between levels and prepare new game board
	void InitialiseList()
	{
		//empty grid positions
		gridPositions.Clear ();

		//create for loop to cycle through rows
		for (int x = 1; x < columns - 1; x++)
		{
			//create for loop to cycle through columns
			for (int y = 1; y < rows - 1; y++)
			{
				//Add vector3 to each grid position
				gridPositions.Add (new Vector3(x,y,0f));
			}
		}

	}

	//Create a setup method to set up outer boundaries and floor
	void BoardSetup ()
	{
		//Set board spot to the transform of the board
		boardSpot = new GameObject ("Board").transform;
		//create for loop to cycle through columns
		for (int x = -1; x < columns + 1; x++)
		{
			//create for loop to cycle through rows
			for (int y = -1; y < rows + 1; y++)
			{
				//prepare to instantiate floor tiles
				GameObject toInstantiate = floorTile;
				//prepare to instantiate boundary wall tiles around outer edge of game board
				if (x == -1 || x== columns || y == -1 || y == rows)
					toInstantiate = outerWallTile;

				//instantiate the preparatory variable and cast as GameObject
				GameObject instance = Instantiate(toInstantiate, new Vector3 (x,y,0f), Quaternion.identity) as GameObject;
				// set the parent of instance variable to boardSpot
				instance.transform.SetParent (boardSpot);
			}
		}
	}

	//Create method for setting random positions for treasure tag items and enemy
	Vector3 RandomPosition()
	{
		//create index for random generation and set to a value from the above count method
		int randomIndex = Random.Range (0, gridPositions.Count);
		//create vector3 and set to above index value
		Vector3 randomPosition = gridPositions [randomIndex];
		//remove value from index
		gridPositions.RemoveAt (randomIndex);
		// return vector3
		return randomPosition;

	}

	//Create method to spawn the objects to their random locations after each level.
	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		//select game objects to generate within limites
		int objectCount = Random.Range (minimum, maximum + 1);

		//create for loop to create objects until limits are met
		for (int i = 0; i < objectCount; i++)
		{
			//create vector3 to choose position for object
			Vector3 randomPosition = RandomPosition ();
			//Create gameobject to choose tiles 
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			//instantiate tile chooser
			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}

	//Create method to interract with GameManger  to create the game experience with random enemies, treasure, objects and wall. 
	public void SetupScene (int level)
	{
		//create floor and wall
		BoardSetup ();
		//reset grid positions
		InitialiseList ();
		// use spawning method to create obstacles
		LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
		// use spawning method to create treasures
		LayoutObjectAtRandom (treasureTiles, treasureCount.minimum, treasureCount.maximum);
		// create enemies exponentially based on game level
		int enemyCount = (int)Mathf.Log (level, 2f);
		// use spawning method to create enemies
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		// create exit tile
		Instantiate (greenRuby, new Vector3 (columns - 1, rows - 1, 0F), Quaternion.identity);
	}

	// Set variables
		//columns with value
	public int columns = 8;
		//rows with value
	public int rows = 8;
		// wallCount with range
	public Count wallCount = new Count (5,9);
		// treasurecount with range
	public Count treasureCount = new Count (3,5);
		// exit treasure object
	public GameObject greenRuby;
		//floortile pattern object
	public GameObject floorTile;
		//wallTiles object array
	public GameObject[]wallTiles;
		// treasure tiles object array
	public GameObject[]treasureTiles;
		// enemy tiles object array
	public GameObject[]enemyTiles;
		//boundary wall object
	public GameObject outerWallTile;

		// board spo transform reference
	private Transform boardSpot;
		// possible tile locations
	private List <Vector3> gridPositions = new List<Vector3>();
}
