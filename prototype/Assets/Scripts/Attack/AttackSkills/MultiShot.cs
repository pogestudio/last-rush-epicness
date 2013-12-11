using UnityEngine;
using System.Collections;

public class MultiShot : SkillEffect
{
	
		private float projectileSpeed;
		private float shotDamageMinimizer = 0.6F;
		private int amountOfExtraProjectiles = 2;
	
		private Vector3 initialDirectionVector;
	
		private bool hasRunOnce;
	
	
		// Use this for initialization
		void Update ()
		{
				if (Time.time < 0.3 || hasRunOnce)
						return;
				if (shouldBailOut ())
						return;
			
				projectileSpeed = gameObject.rigidbody.velocity.magnitude;
				initialDirectionVector = gameObject.rigidbody.velocity;
				//initialDirectionVector.Normalize ();
		
				//fire off projectiles!
				int eachShotDamage = (int)(baseShotDamage * shotDamageMinimizer);
				for (float i = 0; i < amountOfExtraProjectiles; i++) {
						GameObject newProjectile = ProjectileFactory.sharedFactory ().deliverProjectile (gameObject.transform, currentWeaponType, eachShotDamage);
						setCorrectAngleAndSpeed (newProjectile, i);
						//Debug.Log ("Want to fire projectile " + i + " with vector " + newProjectile.rigidbody.velocity);
			
				}
		
				hasRunOnce = true;
				Destroy (this);
		
		}
	
	
		//calculateAngle
		void setCorrectAngleAndSpeed (GameObject projectile, float projectileNumber)
		{
				//set the directions to the correc angles.
				float rotationAngle;
				float eachStep = 20F;
				if (projectileNumber / (amountOfExtraProjectiles - 1) > 0.5F) {
						rotationAngle = eachStep * (amountOfExtraProjectiles - projectileNumber); //1 vs 0 indexed, so it will never amtOfExtra will never be the same as projNumber
			
				} else {
						rotationAngle = eachStep * -(projectileNumber + 1); //0 indexed
				}
			
				//Debug.Log ("shot fired! " + projectileNumber + " of " + amountOfExtraProjectiles + " and angle: " + rotationAngle);
				//Vector3 newVelocity = transform.TransformDirection (initialDirectionVector);
				//projectile.rigidbody.velocity = Quaternion.Euler (0, rotationAngle, 0) * newVelocity;
//		projectile.transform.rotation = Quaternion.AngleAxis (rotationAngle, Vector3.up);
				initialDirectionVector.Normalize ();
				projectile.rigidbody.velocity = Quaternion.Euler (0, rotationAngle, 0) * initialDirectionVector;
				projectile.rigidbody.velocity *= projectileSpeed;
		
//		vector = Quaternion.Euler(0, -45, 0) * vector;
		
		
				projectile.GetComponent<RegularShot> ().wasTriggeredByMultishot = true;
		
				//directionVector = directionVector * projectileSpeed;
		
				//		Debug.Log ("rotation angle " + rotationAngle);
				//		Debug.Log ("directionVector " + directionVector);
				//		Debug.Log ("velocity after transform " + projectile.transform.rotation);
		
		}
	
		public override void createEffect (GameObject colliderObject)
		{
		
		}
	
		public override void doDamage (GameObject colliderObject)
		{
	
		}
	
		/// <summary>
		/// Function will check if this shot was triggered by multishot. In that case, bail out, since we don't want an endlesssss loop. 
		/// </summary>
		/// <returns><c>true</c>, if bail out was shoulded, <c>false</c> otherwise.</returns>
		private bool shouldBailOut ()
		{
				RegularShot regShotComp = gameObject.GetComponent<RegularShot> ();
				return regShotComp.wasTriggeredByMultishot;
		}
}
