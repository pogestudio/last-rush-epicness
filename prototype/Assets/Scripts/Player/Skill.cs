using UnityEngine;
using System.Collections;

public class Skill
{
		public static ArrayList allSkills = new ArrayList ();
		public WeaponTypes currentWeaponType;
		public string componentName;
		public string description;
	
		public Skill (string componentName, WeaponTypes weaponType, string description)
		{
				allSkills.Add (this);
				this.currentWeaponType = weaponType;
				this.componentName = componentName;
				this.description = description;
		
		}
	

		public static float[] xpLevels;
		private const int maxLevel = 40;
		public int currentSkillLevel = 0;
		private float totalXP;
		
		public static Skill skillForWeaponType (WeaponTypes weaponType)
		{
				int length = allSkills.Count;
				for (int i = 0; i < length; i++) {
						if (((Skill)allSkills [i]).currentWeaponType == weaponType) {
								//Debug.Log ("WeaponType : " + weaponType + " is connected to attackskill: " + (AttackSkills)skillTypePairs [i].skill);
								return (Skill)allSkills [i];
						}
				}
				throw new UnityException ("An attackskill is not connected to a weapontype!");
		}
		
		
		/// <summary>
		/// Add Xp to skill.
		/// </summary>
		/// <returns><c>true</c>, if skill leveled up</returns>
		/// <param name="newXP">New X.</param>
		public bool addXpToSkill (float newXP)
		{
				Debug.Log ("skill level  " + currentSkillLevel);
				float XpNeededForNextLevel = ExpLevels () [currentSkillLevel];
				totalXP += newXP;
				bool didLevelUp = false;
				//if xp is more than needed for next level. 
				if (totalXP >= XpNeededForNextLevel) {
						currentSkillLevel++;
						didLevelUp = true;
				}
				if (didLevelUp)
						Debug.Log (currentWeaponType + " did level up!");
				return didLevelUp;
		}
		
		public float[] ExpLevels ()
		{
				if (xpLevels == null) {
						float multiplier = 1.5F;
						xpLevels = new float[maxLevel];
				
						float firstLevelXp = 500F;
						xpLevels [0] = firstLevelXp;
						for (int i = 1; i < maxLevel; i++) {
								xpLevels [i] = xpLevels [i - 1] * multiplier;
						}
				}
				return xpLevels;
		}
		
		
		public bool shouldAddEffect ()
		{
				//at max level, it should be 40% chance.
				//at level 0, it should be 0% chance.
				//1%  increase every all levels.
				//float baseChance = 0F;
				//float maxChance = 0.4F;
				//float chanceOfHappening = baseChance + (maxChance - baseChance) * (float)currentSkillLevel / (float)maxLevel;
				//Debug.Log ("Chance of " + componentName + " happening is " + chanceOfHappening);
				bool shouldAddEffect = chanceToAddEffect () >= Random.value;
				return shouldAddEffect;
			
		}
		
		public float chanceToAddEffect ()
		{
				return ((float)currentSkillLevel) / 100F;
		}
		
		public float currentXp ()
		{
				return totalXP;
		}

		public float nextXpLimit ()
		{
				return ExpLevels () [currentSkillLevel];
		}

		public float prevXpLimit ()
		{
				if (currentSkillLevel == 0)
						return 0;
				else 
						return ExpLevels () [currentSkillLevel - 1];
		}
	
}
	
	