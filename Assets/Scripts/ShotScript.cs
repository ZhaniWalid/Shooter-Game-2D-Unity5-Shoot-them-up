using UnityEngine;
using System.Collections;

/// Projectile behavior

public class ShotScript : MonoBehaviour {

	// 1 - Designer variables
	
	/// Damage inflicted
	public int damage = 1;
	
	/// Projectile damage player or enemies?
	public bool isEnemyShot = false;
	
	// Use this for initialization
	void Start () {
		
		// 2 - Limited time to live to avoid any leak ( = fuite )
		//Destroy (GameObject, 20); // 20 seconds
		// We destroy the object after 20 seconds to avoid any leak ( = fuite )
	}

}
