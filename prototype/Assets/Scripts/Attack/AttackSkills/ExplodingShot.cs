using UnityEngine;
using System.Collections;

/// <summary>
/// A skill that ads a burning effect. Use as the other attack skills. 
/// </summary>
public class ExplodingShot : Skill
{	
	private float explodingDistance = 5F;
	private float explodingMultiplier = 0.5F;
	
	private override void doDamage (GameObject colliderObject)
	{
		ArrayList monstersWithinExplodingArea = monstersWithinArea (transform.position, explodingDistance);
		int explosionDamage = (int)(baseShotDamage * explodingMultiplier);
		foreach (GameObject monster in monstersWithinExplodingArea) {
			doDamageToSingleTarget (monster, explosionDamage, currentWeaponType);
		}
	}
	
	private override void createEffect (GameObject colliderObject)
	{
		EffectFactory.sharedFactory ().deliverSmallExplosion (transform);
	}
}

