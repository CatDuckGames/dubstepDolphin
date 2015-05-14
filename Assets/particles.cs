using UnityEngine;
using System.Collections;

public class particles : MonoBehaviour {
		/// <summary>
		/// Singleton
		/// </summary>
		public static particles Instance;
		
		public ParticleSystem boom;
		
		void Awake()
		{
			// Register the singleton
			if (Instance != null)
			{
				Debug.LogError("Multiple instances of SpecialEffectsHelper!");
			}
			
			Instance = this;
		}
		
		/// <summary>
		/// Create an explosion at the given location
		/// </summary>
		/// <param name="position"></param>
		public void Explosion(Vector3 position)
		{
			// Smoke on the water
			instantiate(boom, position);
		}
		
		/// <summary>
		/// Instantiate a Particle system from prefab
		/// </summary>
		/// <param name="prefab"></param>
		/// <returns></returns>
		private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
		{
			ParticleSystem newParticleSystem = Instantiate(
				prefab,
				position,
				Quaternion.identity
				) as ParticleSystem;
			
			// Make sure it will be destroyed
			Destroy(
				newParticleSystem.gameObject,
				newParticleSystem.startLifetime
				);
			
			return newParticleSystem;
		}
	}