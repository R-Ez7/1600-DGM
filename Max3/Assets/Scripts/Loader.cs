using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour 
{

	public GameObject gameManager;

	//What the loader script is to help with is to instantiate the game manager.
	void Awake () 
	{
		//create conditional statement to determine if gameManager prefab has been set to the GameManager script
	if (GameManager.instance == null)
			//instantiate the gameManager prefab
			Instantiate (gameManager);
	}

}
