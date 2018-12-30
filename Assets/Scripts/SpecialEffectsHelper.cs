using UnityEngine;
using System.Collections;

/// Creating instance of particles from code with no effort

public class SpecialEffectsHelper : MonoBehaviour {

	/// Singleton
//  Singleton: a singleton is a design pattern that is used to guarantee that an object is only instantiated once.
//		        We have diverged a bit from the classic implementation in our script: the principle remains, however.

//	We created a singleton that you can access from everywhere using the SpecialEffectsHelper.Instance member.

	public static SpecialEffectsHelper Instance;

	public ParticleSystem smokeEffect;
	public ParticleSystem fireEffect;

	void Awake()
	{
		// Register the singleton
		if (Instance != null) {
			Debug.LogError("Multiple instances of SpecialEffectsHelper!");
		}

		Instance = this;

	}

	/// Create an explosion at the given location
	public void Explosion (Vector3 position)
	{
		// Smoke on the water
		instantiate (smokeEffect, position);
		// Fire in the sky
		instantiate (fireEffect, position);

	}

	/// Instantiate a Particle system from prefab
	private ParticleSystem instantiate (ParticleSystem prefab,Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate(
	     prefab,
         position,
         Quaternion.identity
		) as ParticleSystem;

		// Make sure it will be destroyed
		Destroy ( newParticleSystem.gameObject, newParticleSystem.startLifetime );

		return newParticleSystem;

	}

//Note: because we can have multiple particles in the scene at the same time, 
//	we are forced to create a new prefab each time. If we were sure that only one system was used at a time,
//	we would have kept the reference and use the same everytime.
}
