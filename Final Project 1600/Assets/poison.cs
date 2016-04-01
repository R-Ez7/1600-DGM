using UnityEngine;
using System.Collections;

public class poison : MonoBehaviour {

	public int pointsToSubtract;

	void OnTriggerEnter2D (Collider2D other){
		if (other.GetComponent<Move> () == null)
			return;
		ScoreManager.SubtractPoints (pointsToSubtract);

		//This will distroy the coin once collected
		Destroy (gameObject);

	}

}
