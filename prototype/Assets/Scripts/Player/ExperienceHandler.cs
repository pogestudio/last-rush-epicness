using UnityEngine;
using System.Collections;

/// <summary>
/// All weapon types in the game. Crude implementation.
/// </summary>
public enum WeaponTypes
{
	WeaponTypeINVALID = 0,
	MachineGun = 1,
	//Revolver,
	WeaponTypesMAX,
};

/// <summary>
/// The pairing of skills and weapon types. This can be randomised at game
/// start for more diversity. This is how XPHandler knows what skill to level up
/// when doing damage with a certain weapon. 
/// </summary>
struct SkillTypePair
{
	public int skill;
	public WeaponTypes weaponType;
}

/// <summary>
/// Experience handler.
/// Every projectile that does damage to a monster should report to experience handler.
/// When damage is reported, experience handler calculates XP in each skill.
/// XP handler keeps track of which levels the different weapons are at, and reports to
/// the different handlers each time a skill dings. 
/// </summary>
public class ExperienceHandler : MonoBehaviour
{

	protected static ExperienceHandler instance;

	private int attackSkillsMax;
	private SkillTypePair[] skillTypePairs;
	private float[] weaponTypesXP;
	/// <summary>
	/// Use this to retrieve the singleton
	/// </summary>
	public static ExperienceHandler sharedHandler ()
	{
		return instance;
	}
	
	
	void Start ()
	{
		instance = this;
		weaponTypesXP = new float[(int)WeaponTypes.WeaponTypesMAX];
		skillTypePairs = new SkillTypePair[(int)WeaponTypes.WeaponTypesMAX];
		matchWeaponTypeToSkills ();
	}
	
	/// <summary>
	/// Incorporate some nice logic that will match each wepaon type to a skill here.
	/// </summary>
	private void matchWeaponTypeToSkills ()
	{
		//in test mode, machine gun = critical hit. 
		SkillTypePair first = new SkillTypePair ();
		first.skill = (int)AttackSkills.RegularShot;
		first.weaponType = WeaponTypes.WeaponTypeINVALID;
		
		SkillTypePair second = new SkillTypePair ();
		second.skill = (int)AttackSkills.CriticalHit;
		second.weaponType = WeaponTypes.MachineGun;
		
		skillTypePairs [0] = first;
		skillTypePairs [1] = second;
	}
	
	/// <summary>
	/// Will return the matched AttackSkill to the weapon type. At the moment we only have
	/// attack skills, so it's simple.
	/// </summary>
	/// <returns>The for weapon type.</returns>
	/// <param name="weaponType">Weapon type.</param>
	private AttackSkills skillForWeaponType (WeaponTypes weaponType)
	{
		int length = skillTypePairs.Length;
		for (int i = 0; i < length; i++) {
			if (skillTypePairs [i].weaponType == weaponType) {
				Debug.Log ("WeaponType : " + weaponType + " is connected to attackskill: " + (AttackSkills)skillTypePairs [i].skill);
				return (AttackSkills)skillTypePairs [i].skill;
			}
		}
		throw new UnityException ("An attackskill is not connected to a weapontype");
	}
	
	/// <summary>
	/// Call this when a projectile impacts a monster. Used to calculate XP gains.
	/// </summary>
	/// <param name="damage">Damage.</param>
	/// <param name="byWeapon">By weapon.</param>
	public void damagesWasDealt (int damage, WeaponTypes byWeapon)
	{
		Debug.Log ("damage was dealt byy WeaponType: " + byWeapon);
		float currentXp = weaponTypesXP [(int)byWeapon];
		float newExp = currentXp + (float)damage;
		weaponTypesXP [(int)byWeapon] = newExp;
		if (didWeaponLevelUp (currentXp, newExp)) {
			skillLeveledUp ();
		}
	}
	
	/// <summary>
	/// Check if the the XP passed a level threshold.
	/// </summary>
	/// <returns><c>true</c>, if weapon level up was dided, <c>false</c> otherwise.</returns>
	/// <param name="oldXp">Old xp.</param>
	/// <param name="newXp">New xp.</param>
	private bool didWeaponLevelUp (float oldXp, float newXp)
	{
		Debug.Log ("Old EXP::" + oldXp + "     new:  " + newXp);
		//extremely crude
		bool didLevel = false;
		if (oldXp < 99 && newXp > 98)
			didLevel = true;
		return didLevel;
	}
	
	/// <summary>
	/// Perform actions connected to  alevel up. 
	/// </summary>
	/// <param name="weaponType">Weapon type.</param>
	private void skillLeveledUp (WeaponTypes weaponType)
	{
		AttackSkills skillForWeapon = skillForWeaponType (weaponType);
		AttackHandler.sharedHandler ().skillDidLevelUp (skillForWeapon);
	}
}
