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
		SniperRifle = 4,
		WeaponTypesMAX,
};

struct AttackSkillStruct
{
		public AttackSkillStruct (string compN, string desc)
		{
				componentName = compN;
				description = desc;
		}
		public string componentName;
		public string description;
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

		private ArrayList allSkills;

		public GameObject levelUpEffect;

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
				if (!networkView.isMine)
						return;
						

		
				ArrayList allSkills = new ArrayList ();
				allSkills.Add (new AttackSkillStruct ("FrostNovaShot", "Colder than Lady Gagas tits, these projectiles slow everything around them upon impact"));
				allSkills.Add (new AttackSkillStruct ("CriticalHit", "Have a chance to double up in damage!"));
				allSkills.Add (new AttackSkillStruct ("BurningShot", "Make your enemy burn with this. High DOT dps. And yes, they stack!"));
				allSkills.Add (new AttackSkillStruct ("ShrapnelShot", "Chance of splitting up into new bullets on impact!"));
				allSkills.Add (new AttackSkillStruct ("ExplodingShot", "Ever wondered what it feels like to fire a dynamite? New witch craft technology enables you to do just that."));
				allSkills.Add (new AttackSkillStruct ("HomingShot", "Seek out closest enemy and (try to) destroy them utterly!"));
				allSkills.Add (new AttackSkillStruct ("LightningShot", "Zeus fury is released upon the foes!"));
				allSkills.Add (new AttackSkillStruct ("MultiShot", "Fire one, fire two, or why not more? By adding on this skill you will quickly find out that one muzzle can do the work of several."));
				allSkills.Add (new AttackSkillStruct ("PiercingShot", "This shit fires piercing shots, with piercing abilities close to that of LA Ink"));
				
				ArrayList allWeaponTypes = new ArrayList ();
				for (int i = 0; i < (int)WeaponTypes.WeaponTypesMAX; i++) {
						int randomAttackSkillToChoose = Random.Range (0, allSkills.Count - 1);
						AttackSkillStruct chosenAttackSkill = (AttackSkillStruct)allSkills [randomAttackSkillToChoose];
						allSkills.RemoveAt (randomAttackSkillToChoose);
						new Skill (chosenAttackSkill.componentName, chosenAttackSkill.description, (WeaponTypes)i);
				}

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
				if (levelUpEffect) {
						GameObject effect = GameObject.Instantiate (levelUpEffect) as GameObject;
						effect.transform.parent = gameObject.transform;
						effect.transform.localPosition = Vector3.zero;
				}
		}
}
