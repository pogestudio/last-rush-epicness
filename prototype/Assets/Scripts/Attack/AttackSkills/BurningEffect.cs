using UnityEngine;
using System.Collections;

/// <summary>
/// A skill that ads a burning effect. Use as the other attack skills. 
/// </summary>
public class BurningEffect : Projectile
{	
	private float burningTime = 5F;
	private float burningMultiplier = 0.5F;
	
	void OnCollisionEnter (Collision collisionObject)
	{
		Debug.Log ("BURNING EFFECT weaponType " + currentWeaponType);
		if (targetIsEnemy (collisionObject.gameObject)) {
			doDamageTo (collisionObject.gameObject, baseShotDamage, currentWeaponType);	
			BurnDPSEffect burningeffect = collisionObject.gameObject.AddComponent<BurnDPSEffect> ();
			burningeffect.weaponToCauseIt = currentWeaponType;
			burningeffect.burningDamage = burningMultiplier * baseShotDamage;
			burningeffect.burningDuration = burningTime;
			
		}
		
		base.destroyProjectileWithDelay (gameObject);
	}
}

