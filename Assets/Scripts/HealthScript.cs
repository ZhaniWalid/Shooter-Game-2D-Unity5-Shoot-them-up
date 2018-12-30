using UnityEngine;
using System.Collections;

/// Handle hitpoints and damages

public class HealthScript : MonoBehaviour {

	/// <summary>
	/// Total hitpoints
	/// </summary>
	public int hp = 2;
	
	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;
	
	//static variable for transfering the new value of score to 'UiManagerScript' class to the variable 'score'
	public static int ScoreReturn; 
	//static variable for transfering the new value of player Health to 'UiManagerScript' class to the variable 'HealthPlayer'
	public static int playerHealthReturn;
	//
	public static HealthScript Instance;
	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>


	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0) {
			// Explosion!
			SpecialEffectsHelper.Instance.Explosion (transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound ();
			//Static value of player score
			ScoreReturn ++;
			// Dead!
			Destroy (gameObject);

		} else {
			//static value of player health
			playerHealthReturn --;
		}
	}
    

	void OnTriggerEnter2D(Collider2D collider)
	{
		// Is this a shot?
		ShotScript shot = collider.gameObject.GetComponent<ShotScript>();

		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);

				//hp -= shot.damage;

				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
				
			/*	if (hp <= 0)
				{
					// Explosion!
					SpecialEffectsHelper.Instance.Explosion(transform.position);
					SoundEffectsHelper.Instance.MakeExplosionSound();
					// Dead!
					Destroy(gameObject);
				}
           */
			}
		}
	}
}
