using UnityEngine;
using System.Collections;

/// <summary>
/// AttackSkills is a crude reference for the different skills we can level up.
/// They are seperated from weapon types, if we want to randomise the connection
/// or similar. 
/// </summary>
public enum AttackSkills
{
	RegularShot = 0,
	CriticalHit,
	//ChillingEffect,
	//BurningEffect,
	//ExplosionEffect,
	AttackSkillsMAX,
}
;

/// <summary>
/// AttackHandler handles the logic behind each attack. THe projectile factory will call
/// this, and attack handler will decide what kind of type of attack will be fired.
/// It relies upon calls from ExperienceHandler to know when a player levels up in different
/// skills.
/// </summary>
public class AttackHandler : MonoBehaviour
{
	protected static AttackHandler instance; // Needed
	private int totalAmountOfSkills = (int)AttackSkills.AttackSkillsMAX;
	private int[] skillLevels;
	
	// Call AttackHandler.sharedHandler to retrieve the singleton. 
	public static AttackHandler sharedHandler ()
	{
		return instance;
	}
	
	void Start ()
	{
		Debug.Log ("attack handler is GAME!!");
		instance = this;
		skillLevels = new int[totalAmountOfSkills]; //init the skillLevels
	}
	
	/// <summary>
	/// Call this from Experience Handler, to let AH know when a skill has leveled up. 
	/// </summary>
	/// <param name="skillWhichLeveledUp">Skill which leveled up.</param>
	public void skillDidLevelUp (AttackSkills skillWhichLeveledUp)
	{
		Debug.Log ("skill leveled up!" + skillWhichLeveledUp);
		skillLevels [(int)skillWhichLeveledUp] = skillLevels [(int)skillWhichLeveledUp] + 1;
	}
	
	/// <summary>
	/// Call this for every shot to fire. 
	///The attack handler will decide what type of shot to fire
	/// </summary>
	/// <returns>The effect be added to shot.</returns>
	public AttackSkills typeOfShotToFire ()
	{
		return AttackSkills.CriticalHit;
	}
	
}
