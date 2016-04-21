using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {

	public int pointsToAdd;

	void OnTriggerEnter2D (Collider2D other){
		if (other.GetComponent<Move> () == null)
			return;
		ScoreManager.AddPoints (pointsToAdd);

		//This will distroy the coin once collected
		Destroy (gameObject);

	}

}
