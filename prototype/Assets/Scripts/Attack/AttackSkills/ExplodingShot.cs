using UnityEngine;
using System.Collections;

/// <summary>
/// A skill that ads a burning effect. Use as the other attack skills. 
/// </summary>
public class ExplodingShot : Projectile
{	
	private float explodingDistance = 5F;
	private float explodingMultiplier = 0.5F;
	
	void OnCollisionEnter (Collision collisionObject)
	{
		Debug.Log ("Exploding Shot weaponType " + currentWeaponType);
		showExplosion ();
		dealDamage (collisionObject.gameObject);
		
		base.destroyProjectileWithDelay (gameObject);
	}
	
	void showExplosion ()
	{
		EffectFactory.sharedFactory ().deliverSmallExplosion (transform);
	}
	
	void dealDamage (GameObject aroundThisObject)
	{
		ArrayList monstersWithinExplodingArea = monstersWithinArea (transform.position, explodingDistance);
		int explosionDamage = (int)(baseShotDamage * explodingMultiplier);
		foreach (GameObject monster in monstersWithinExplodingArea) {
			doDamageTo (monster, explosionDamage, currentWeaponType);			
		}
		
	}
	
	
}

