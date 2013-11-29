using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour
{
	public static Skill instance;
	public static float[] xpLevels;
	private static int maxLevel = 99;
	public static int currentSkillLevel = 1;
	private float totalXP;
	
	
	public WeaponTypes currentWeaponType;
	
	
	public int baseShotDamage;
	public int standardDestroyTime = 4;


	void Start ()
	{
		instance = this;
		Destroy (gameObject, standardDestroyTime);
	}
	
	/// <summary>
	/// Add Xp to skill.
	/// </summary>
	/// <returns><c>true</c>, if skill leveled up</returns>
	/// <param name="newXP">New X.</param>
	public bool addXpToSkill (float newXP)
	{
		float XpNeededForNextLevel = ExpLevels () [currentSkillLevel - 1];
		totalXP += newXP;
		bool didLevelUp = false;
		//if xp is more than needed for next level. 
		if (totalXP >= XpNeededForNextLevel) {
			currentSkillLevel++;
			didLevelUp = true;
		}
		return didLevelUp;
	}
	
	public float[] ExpLevels ()
	{
		if (xpLevels.Length = 0) {
			
			float multiplier = 1.5F;
			xpLevels = new float[maxLevel];
			
			float firstLevelXp = 500;
			xpLevels [0] = firstLevelXp;
			for (int i = 1; i < maxLevel; i++) {
				xpLevels [i] = xpLevels [i - 1] * multiplier;
			}
		}
		return xpLevels;
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
	
	public bool shouldAddEffect ()
	{
		//at level 100, it should be 40% chance.
		//at level 0, it should be 10% chance. 
		//30% increase over all levels.
		float baseChance = 0.1;
		float maxChance = 0.4;
		float chanceOfHappening = baseChance + (maxChance - baseChance) * maxLevel / currentSkillLevel;
		bool JACKPOT = chanceOfHappening > Random.value;
		
	}
	
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

