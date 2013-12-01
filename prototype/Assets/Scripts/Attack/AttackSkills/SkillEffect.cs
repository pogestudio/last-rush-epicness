using UnityEngine;
using System.Collections;

/// <summary>
/// The attack skills that can be added to the projectile. 
/// </summary>
public abstract class SkillEffect : MonoBehaviour
{
	public static SkillEffect instance;
	
	public WeaponTypes currentWeaponType;
	
	
	public int baseShotDamage;
	public int standardDestroyTime = 4;


	void Start ()
	{
		instance = this;
		Destroy (gameObject, standardDestroyTime);
	}
	
	public void OnCollisionEnter (Collision colliderObject)
	{
		if (targetIsEnemy (colliderObject.gameObject)) {
			doDamage (colliderObject.gameObject);
		}
		createEffect (colliderObject.gameObject);
		
		destroyProjectileWithDelay (gameObject);
	}
	
	public void setUpProjectile (int baseWeaponDamage, WeaponTypes firingWeapon)
	{
		baseShotDamage = baseWeaponDamage;
		currentWeaponType = firingWeapon;
	}
	
	
	/*
	               INSIDE STUFF, THINGS THAT SHOULD BE INHERITED BY SUBCLASSES
	*/
	
	public abstract void createEffect (GameObject toObject);
	public abstract void doDamage (GameObject colliderObject);

	
	public void doDamageToSingleTarget (GameObject target, int damage, WeaponTypes weaponOfChoice)
	{
		if (targetIsEnemy (target)) {
			target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
			reportDamageToExpHandler (damage, weaponOfChoice);
		}
	}
	
	public static void wantToDamage (GameObject target, int damage, WeaponTypes weaponOfChoice)
	{
		instance.doDamageToSingleTarget (target, damage, weaponOfChoice);
	}
	
	private void reportDamageToExpHandler (int damage, WeaponTypes weaponOfChoice)
	{
		ExperienceHandler.sharedHandler ().damagesWasDealt (damage, weaponOfChoice);
	}
	
	public bool targetIsEnemy (GameObject target)
	{
		return MonsterFinder.sharedHelper ().targetIsEnemy (target);
	}
	
	public void destroyProjectileWithDelay (GameObject projectile)
	{
		Destroy (projectile, 0.05F); // let other components do their stuff
	}

}
