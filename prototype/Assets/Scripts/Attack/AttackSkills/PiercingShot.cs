using UnityEngine;
using System.Collections;

public class PiercingShot : SkillEffect
{
	private float projectileSpeed;
		
	private Vector3 initialDirectionVector;
		
	private bool hasRunOnce;
		
		
	// Use this for initialization
	
	void Start ()
	{
		initialDirectionVector = gameObject.rigidbody.velocity;
		initialDirectionVector.Normalize ();
		projectileSpeed = gameObject.rigidbody.velocity.magnitude;
	}
	
	public override void createEffect (GameObject colliderObject)
	{
			
	}
		
	public override void doDamage (GameObject colliderObject)
	{
		//spawn a new projectile until the other side of this monster
		float width = colliderObject.renderer.bounds.size.x;
		float depth = colliderObject.renderer.bounds.size.y;
		float totalAcross = Mathf.Sqrt (Mathf.Pow (width, 2) + Mathf.Pow (depth, 2));
		Vector3 newProjectilePos = gameObject.transform.position + initialDirectionVector * totalAcross;
		GameObject newProjectile = ProjectileFactory.sharedFactory ().deliverProjectileWithoutTransform (newProjectilePos, Quaternion.Euler (0, 0, 0), currentWeaponType, baseShotDamage);
		newProjectile.rigidbody.velocity = initialDirectionVector * projectileSpeed;
			
	}
	
}

