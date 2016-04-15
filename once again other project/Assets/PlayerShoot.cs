using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public Transform firePoint;
	public GameObject projectile;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
			Instantiate (projectile, firePoint.position, firePoint.rotation);
		}


	}
}
