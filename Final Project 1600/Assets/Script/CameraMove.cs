using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public Move player;
	public bool isFollowing;


	public float xOffset;
	public float yOffset;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Move> ();
		isFollowing = true;

	}

	// Update is called once per frame
	void Update () {
		if (isFollowing) {
			transform.position = new Vector3 (player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
		}
	}
}