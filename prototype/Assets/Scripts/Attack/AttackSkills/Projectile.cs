using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{

	public WeaponTypes currentWeaponType;
	public int baseShotDamage;
	public int standardDestroyTime = 10;


	void Start ()
	{
		Destroy (gameObject, standardDestroyTime);
		if (baseShotDamage == 0)
			Debug.LogError ("Damage is 0 on a projectile, fix!");
	}
	
	public void doDamageTo (GameObject target, int damage, WeaponTypes weaponOfChoice)
	{
		target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
		reportDamageToExpHandler (damage, weaponOfChoice);
	}
	
	private void reportDamageToExpHandler (int damage, WeaponTypes weaponOfChoice)
	{
		ExperienceHandler.sharedHandler ().damagesWasDealt (damage, weaponOfChoice);
	}
	
	public bool targetIsEnemy (GameObject target)
	{
		return (target.tag == "RegularMonster");
	}

}

