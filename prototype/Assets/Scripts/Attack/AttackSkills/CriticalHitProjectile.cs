using UnityEngine;
using System.Collections;

public class CriticalHitProjectile : ProjectileCollision
{

	public new int damage = 0;
	public new WeaponTypes currentWeaponType;
	
	
	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, destroyTime ());
	}
	
	
	void OnCollisionEnter (Collision collisionObject)
	{
		Debug.Log ("Crit hit colliiiiiision with weaponType " + currentWeaponType);
		if (targetIsEnemy (collisionObject.gameObject)) {
			doDamageTo (collisionObject.gameObject, 2 * damage);			
		}	
		Destroy (gameObject);
	}
}

