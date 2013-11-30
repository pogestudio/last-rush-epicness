using UnityEngine;
using System.Collections;

public class ShrapnelShot : SkillEffect
{

	private float projectileSpeed;
	private float amountOfProjectiles = 4F;
	
	private Vector3 lastPositionBeforeHit;
	private Vector3 secondLastPos;
	private Vector3 directionVector;
	
	
	// Use this for initialization
	void Start ()
	{
		projectileSpeed = gameObject.rigidbody.velocity.magnitude;
	}
	
	public override void doDamage (GameObject colliderObject)
	{	
		if (!targetIsEnemy (colliderObject)) {
			return;
		}
		
		//fire out projectiles
		// set the direction vecot of current projectile
		directionVector = gameObject.rigidbody.velocity;
		//Debug.Log ("calcu dv" + directionVector);
		directionVector.Normalize ();
		//Debug.Log ("normalized dv" + directionVector);
		
		for (float i = 0; i < amountOfProjectiles; i++) {
			GameObject newProjectile = ProjectileFactory.sharedFactory ().deliverProjectile (gameObject.transform, currentWeaponType, baseShotDamage);
			//ignoreCollisionIfExistsAndActive (newProjectile, colliderObject);			
			setCorrectAngleAndSpeed (newProjectile, i / amountOfProjectiles);
			//Debug.Log ("Want to fire projectile " + i + " with vector " + newProjectile.rigidbody.velocity);
			;
			
		}
	}
	
	//calculateAngle
	void setCorrectAngleAndSpeed (GameObject projectile, float factor)
	{
		//set the directions to the correc angles.
		// the angles are between -135 degrees and -90, and +90 degrees and +135
		float rotationAngle;
		if (factor < 0.5) {
			rotationAngle = -(135 - (90) * factor);
		} else {
			rotationAngle = 45 + 90 * factor;
		}

		projectile.rigidbody.velocity = transform.TransformDirection (directionVector * projectileSpeed);
		projectile.transform.rotation = Quaternion.AngleAxis (rotationAngle, Vector3.up);
		//directionVector = directionVector * projectileSpeed;
		
//		Debug.Log ("rotation angle " + rotationAngle);
//		Debug.Log ("directionVector " + directionVector);
//		Debug.Log ("velocity after transform " + projectile.transform.rotation);
		
	}
	
	//ignore collision if object exists and is active
	void ignoreCollisionIfExistsAndActive (GameObject newObject, GameObject oldObject)
	{
		if (oldObject != null && oldObject.activeSelf) {
			Physics.IgnoreCollision (newObject.collider, oldObject.collider);
		}
	}
	
	public override void createEffect (GameObject colliderObject)
	{
		
	}
	
}
