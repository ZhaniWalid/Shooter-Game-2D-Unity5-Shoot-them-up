using UnityEngine;
using System.Collections;

/// Enemy generic behavior

public class EnemyScript : MonoBehaviour {

	private bool hasSpawn; // Spawn = se reproduire = se multiplier
	private MoveScript moveScript;
	//private WeaponScript weapon;
	private WeaponScript[] weapons;
	private Collider2D colliderComponent;
	private SpriteRenderer rendererComponent;
 
//Note: like for GetComponent<>(), 
//GetComponentInChildren<>() also exists in a plural form : GetComponentsInChildren<Type>().
//Notice the s after “Component”. This method returns a list instead of the first corresponding component.

	void Awake () {
		// Retrieve the weapon only once
		//weapon = GetComponent<WeaponScript> ();
		weapons = GetComponentsInChildren<WeaponScript> ();

		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScript> ();
		colliderComponent = GetComponent<Collider2D> ();
		rendererComponent = GetComponent<SpriteRenderer> ();
	}

	// 1 - Disable everything
	void Start() {
	  
		hasSpawn = false;
		// - Disable everything
		// - Collider
		colliderComponent.enabled = false;
		// - Moving
		moveScript.enabled = false;
		// - Shooting
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled=false;
		}
	}

	// Update is called once per frame
	void Update () {

		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false) {
			if (rendererComponent.IsVisibleFrom (Camera.main)) {
				Spawn();
			}
		} else {
		
			// Auto Fire
			foreach (WeaponScript weapon in weapons) {

				if (weapon != null && weapon.canAttack && weapon.enabled) {
					weapon.Attack (true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}

			// 4 - Out of the camera ? Destroy the game object.
			if (rendererComponent.IsVisibleFrom(Camera.main)==false){
				Destroy(gameObject);
			}
		}
	}

	// 3 - Activate itself.
	private void Spawn(){

		hasSpawn = true;
		// Enable everything
		// -- Collider
		colliderComponent.enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled=true;
		}
	}
}
