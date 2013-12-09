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
		ShotGun = 3,
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

			allSkills = new ArrayList ();
			allSkills.Add (new Skill ("FrostNovaShot", WeaponTypes.Gatling, "Colder than Lady Gagas tits, these projectiles slow everything around them upon impact"));
			//allSkills.Add (new Skill ("CriticalHit", WeaponTypes.Gatling));
			//allSkills.Add (new Skill ("BurningShot", WeaponTypes.MachineGun));
			//allSkills.Add (new Skill ("ShrapnelShot", WeaponTypes.Gatling));
			allSkills.Add (new Skill ("ExplodingShot", WeaponTypes.HandGun, "Ever wondered what it feels like to fire a dynamite? New witch craft technology enables you to do just that."));
			//allSkills.Add (new Skill ("HomingShot", WeaponTypes.HandGun));
			allSkills.Add (new Skill ("LightningShot", WeaponTypes.MachineGun, "Zeus fury is released upon the foes! "));
			allSkills.Add (new Skill ("MultiShot", WeaponTypes.ShotGun, "Fire one, fire two, or why not more? By adding on this skill you will quickly find out that one muzzle can do the work of several."));
			//allSkills.Add (new Skill ("PiercingShot", WeaponTypes.MachineGun, "This shit fires piercing shots, with piercing abilities close to that of LA Ink"));

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
