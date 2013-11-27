using UnityEngine;
using System.Collections;

public class CriticalHit : Projectile
{	
	
	void OnCollisionEnter (Collision collisionObject)
	{
		Debug.Log ("Crit hit colliiiiiision with weaponType " + currentWeaponType);
		if (targetIsEnemy (collisionObject.gameObject)) {
			doDamageTo (collisionObject.gameObject, 2 * baseShotDamage, currentWeaponType);			
		}	
		Destroy (gameObject);
	}
}

