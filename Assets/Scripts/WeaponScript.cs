using UnityEngine;
using System.Collections;

/// Launch projectile

public class WeaponScript : MonoBehaviour {

	//--------------------------------
	// 1 - Designer variables
	//--------------------------------


//	We have two members here : shotPrefab and shootingRate.
//	The first one is needed to set the shot that will be used with this weapon.
//	Select your player in the scene “Hierarchy”. In the “WeaponScript” component, you can see the property “Shot Prefab” with a “None” value.
//	Drag the “Shot” prefab in his field 

	/// Projectile prefab for shooting
	public Transform shotPrefab;

	/// Cooldown in seconds between two shots
	public float shootingRate = 0.25f;

	//--------------------------------
	// 2 - Cooldown
	//--------------------------------

//	Guns have a firing rate. If not, you would be able to create tons of projectiles at each frame.
//	So we have a simple cooldown mechanism. 
//	If it is greater than 0 we simply cannot shoot. 
//	We substract the elapsed time at each frame.

	public float shootCooldown;


	// Use this for initialization
	void Start () {
		shootCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}

	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------


	/// Is the weapon ready to create a new projectile?

	public bool canAttack 
	{  
		get { return shootCooldown <= 0f; }
	}

	/// Create a new projectile if possible

//	This is the main purpose of this script: being called from another one. 
//	This is why we have a public method that can create the projectile.
//	Once the projectile is instantiated, we retrieve the scripts of the shot object and override some variables.
//			
//	Note: the GetComponent<TypeOfComponent>() method allows you to get a precise component (and thus a script, because a script is a component after all) from an object.
//		  The generic (<TypeOfComponent>) is used to indicate the exact component that you want.
//		  There is also a GetComponents<TypeOfComponent>() that gets an array instead of the first one, etc.

	public void Attack (bool isEnemy){

		if (canAttack) {

			shootCooldown = shootingRate;

			// Create a new shot
			var shotTransform = Instantiate(shotPrefab) as Transform;

			// Assign position
			shotTransform.position = transform.position;

			// The is enemy property
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if ( shot!= null ){
				shot.isEnemyShot = isEnemy;
			}

			// Make the weapon shot always towards it
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if ( move != null ){
				move.direction = this.transform.right; // towards in 2D space is the right of the sprite
			}
		}
	}
}
