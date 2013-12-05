using UnityEngine;
using System.Collections;

/// <summary>
/// All weapon types in the game. Crude implementation.
/// </summary>
public enum WeaponTypes
{
	HandGun = 0,
	MachineGun = 1,
	Gatling = 2,
	WeaponTypesMAX,
};


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

	private ArrayList allSkills;
	
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
		addAllSkills ();
	}
	
	/// <summary>
	/// Incorporate some nice logic that will match each weapon type to a skill here.
	/// </summary>
	private void addAllSkills ()
	{

		//allSkills = new ArrayList ();
		new Skill ("FrostNovaShot", WeaponTypes.MachineGun);
		new Skill ("CriticalHit", WeaponTypes.HandGun);
		new Skill ("BurningShot", WeaponTypes.MachineGun);
		new Skill ("ShrapnelShot", WeaponTypes.MachineGun);
		new Skill ("ExplodingShot", WeaponTypes.HandGun);
		new Skill ("HomingShot", WeaponTypes.HandGun);
		new Skill ("LightningShot", WeaponTypes.Gatling);

		
	}
	
	/// <summary>
	/// Call this when a projectile impacts a monster. Used to calculate XP gains.
	/// </summary>
	/// <param name="damage">Damage.</param>
	/// <param name="byWeapon">By weapon.</param>
	public void damagesWasDealt (int damage, WeaponTypes byWeapon)
	{
		Skill skillForWeaponDamage = Skill.skillForWeaponType (byWeapon);
		bool didLevelUp = skillForWeaponDamage.addXpToSkill (damage);
		if (didLevelUp) {
			skillLeveledUp (byWeapon);
		}
	}
	
	/// <summary>
	/// Perform actions connected to  alevel up. 
	/// </summary>
	/// <param name="weaponType">Weapon type.</param>
	private void skillLeveledUp (WeaponTypes weaponType)
	{
		Debug.Log ("Skill leveled up for weaponType :: " + weaponType);
	}
}
