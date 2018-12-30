using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput; // for Mobile Controlling with joystick and button

/// <summary>
/// Player controller and behavior
/// </summary>

public class PlayerScript : MonoBehaviour {


	/// 1 - The speed of the ship
	public Vector2 speed = new Vector2(20,20);

	/// 2 - Store the movement and the component 
	private Vector3 movement;
	private Vector3 moveVecMobile; //
	private Rigidbody2D rigidbodyComponent;

	// Use this for initialization
	/*void Start () {
		rigidbodyComponent = this.GetComponent<Rigidbody2D> ();
	}*/
	
	// Update is called once per frame
	void Update () {
	
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

	
		// 4 - Movement per direction (Desktop Control)
		movement = new Vector3 ( speed.x * inputX , speed.y * inputY , 0);
		movement *= Time.deltaTime;

		transform.Translate (movement);
		// Movement per direction (Mobile Control)
		moveVecMobile = new Vector3(speed.x * CrossPlatformInputManager.GetAxis("Horizontal"),speed.y * CrossPlatformInputManager.GetAxis("Vertical"),0);
		moveVecMobile *= Time.deltaTime;

		transform.Translate (moveVecMobile);

	

	// the shoot projectile part from the player
	//Indeed, even if a “WeaponScript” was attached to an entity, the Attack(bool) method would never be called.

//What did we do ?			
//	1.We read the input from a fire button (click or ctrl by default).
//	2.We retrieve the weapon’s script.
//	3.We call Attack(false).
//
//Note:	
//	Button down: you can notice that we use the GetButtonDown() method to get an input. 
//	The “Down” at the end allows us to get the input when the button has been pressed once and only once. 
//	GetButton() returns true at each frame until the button is released. 
//	In our case, we clearly want the behavior of the GetButtonDown() method.			
//	Try to use GetButton() instead, and observe the difference.

		bool shoot = Input.GetButtonDown ("Jump"); // "Fire1" is Left "ctrl" button (Desktop Button Fire)
		//shoot |= Input.GetButtonDown ("Fire1"); // "Jump" is "space" button
		bool shootMobile = CrossPlatformInputManager.GetButton ("KillThem"); // (Mobile Button Fire) 
		// 'KillThem' Name of the button 
		// mwjouda fi : MobileSingleStickControl -> FireButton -> Inspector -> Button Handler (Script) (Parametre : Name) 

		if (shoot || shootMobile) {
		
			WeaponScript weapon = GetComponent<WeaponScript>();
			if ( weapon != null ){
				// false because the player is not an enemy
				weapon.Attack(false);
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			} 
		}

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
		var topBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).y;
		var bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, dist)).y;

		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder)
			, Mathf.Clamp (transform.position.y, topBorder, bottomBorder)
			, transform.position.z
	    );

		// End of the update method
	}

    void OnCollisionEnter2D ( Collision2D collision ) {
	
		bool damagePlayer = false;
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript> ();
		if (enemy != null) {
		   
			// Kill the enemy
			HealthScript enemyHealth =  enemy.GetComponent<HealthScript> ();

			if ( enemyHealth != null ){
				enemyHealth.Damage(enemyHealth.hp);
			}		
			damagePlayer = true;
		}

		// Damage the player
		if (damagePlayer) {
		   
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if(playerHealth != null){
				playerHealth.Damage(1);
			}
		}

	}

	// On Death , two buttons will be shown : 'Restart The Game' and 'Back To Main Menu' and The 'Final Score of the Player'
	void OnDestroy()
	{
		// Game Over.
		var gameOver = FindObjectOfType<GameOverScript> ();
		// Show Buttons & Final Score of Player
		gameOver.ShowButtons();
		gameOver.ShowFinalScore();
	}


	// alled at every fixed framerate frame. 
	// You should use this method over Update() when dealing with physics (“RigidBody” and forces).
	/*void FixedUpdate () {
	
		// 5 - Get the component and store the reference
		if (rigidbodyComponent == null) {	
			GetComponent<Rigidbody2D>();
		}

		// 6 - Move the game object
		rigidbodyComponent.velocity = movement; 
	}*/
}
