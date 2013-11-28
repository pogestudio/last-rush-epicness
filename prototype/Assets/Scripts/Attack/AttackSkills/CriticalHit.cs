using UnityEngine;
using System.Collections;

public class CriticalHit : Projectile
{	
	private int CritHitMultiplier = 2;
	void OnCollisionEnter (Collision collisionObject)
	{
		Debug.Log ("Crit hit colliiiiiision with weaponType " + currentWeaponType);
		doDamageTo (collisionObject.gameObject, CritHitMultiplier * baseShotDamage, currentWeaponType);			
		base.destroyProjectileWithDelay (gameObject);
	}
}

