using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
	public static Projectile instance;
	
	public WeaponTypes currentWeaponType;
	public int baseShotDamage;
	public int standardDestroyTime = 4;


	void Start ()
	{
		instance = this;
		Destroy (gameObject, standardDestroyTime);
		if (baseShotDamage == 0)
			Debug.LogError ("Damage is 0 on a projectile, fix!");
	}
	
	public void doDamageTo (GameObject target, int damage, WeaponTypes weaponOfChoice)
	{
		if (targetIsEnemy (target)) {
			target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
			reportDamageToExpHandler (damage, weaponOfChoice);
		}
	}
	
	public static void wantToDamage (GameObject target, int damage, WeaponTypes weaponOfChoice)
	{
		instance.doDamageTo (target, damage, weaponOfChoice);
	}
	
	private void reportDamageToExpHandler (int damage, WeaponTypes weaponOfChoice)
	{
		ExperienceHandler.sharedHandler ().damagesWasDealt (damage, weaponOfChoice);
	}
	
	public bool targetIsEnemy (GameObject target)
	{
		return (target.tag == "RegularMonster");
	}
	
	public void destroyProjectileWithDelay (GameObject projectile)
	{
		Destroy (projectile, 0.05F); // let other components do their stuff
	}
	
	public ArrayList monstersWithinArea (Vector3 center, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere (center, radius);
		int i = 0;
		ArrayList monsters = new ArrayList ();
		
		while (i < hitColliders.Length) {
			if (targetIsEnemy (hitColliders [i].gameObject)) {
				monsters.Add (hitColliders [i].gameObject);
			}
			i++;
		}
		
		return monsters;
	}

}

