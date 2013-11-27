using UnityEngine;
using System.Collections;

public class RegularShot : Projectile
{

	void OnCollisionEnter (Collision collisionObject)
	{
		if (targetIsEnemy (collisionObject.gameObject)) {
			doDamageTo (collisionObject.gameObject, baseShotDamage, currentWeaponType);			
		}	
		Destroy (gameObject);
	}
}

