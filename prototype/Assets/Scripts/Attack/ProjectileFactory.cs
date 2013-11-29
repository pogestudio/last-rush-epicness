using UnityEngine;
using System.Collections;

/// <summary>
/// Delivers a projectile to the "shooting script", or whoever calls it.
/// Before delivering the projectile, it confers with AttackHandler
/// if it should add any special AttackSkills to the projectile. 
/// </summary>


public class ProjectileFactory : MonoBehaviour
{

	protected static ProjectileFactory instance;
	
	//THE AMMO
	public GameObject bullet;
	

	public static ProjectileFactory sharedFactory ()
	{
		return instance;
	}
	// Use this for initialization
	void Start ()
	{
		instance = this;
	}
	
	/// <summary>
	/// Call from any object belonging to player, for example an equipped gun.
	/// </summary>
	/// <returns>The projectile, in a stand still state at the transform point given</returns>
	public GameObject deliverProjectile (Transform gunOrigin, WeaponTypes weaponType, int weaponDamage)
	{
		GameObject projectileToFire = createProjectile (gunOrigin);
		addAttackSkills (projectileToFire, weaponDamage, weaponType);
		
		
		return projectileToFire;
	}
	
	/// <summary>
	/// Responsible for creating the adding the attack skill component to the projectile
	/// as well as setting it up.
	/// If no special attack, just return the projectile. 
	/// </summary>
	/// <returns>The projectile.</returns>
	/// <param name="skillToApply">Skill to apply.</param>
	private GameObject addAttackSkills (GameObject projectileToFire, int damage, WeaponTypes weaponType)
	{
		//no matter what, add a regular shot
		SkillEffect regularShot = projectileToFire.AddComponent ("RegularShot") as SkillEffect;
		regularShot.setUpProjectile (damage, weaponType);
		
		foreach (Skill attackSkill in Skill.allSkills) {
			Debug.Log (attackSkill.componentName);
			bool shouldAdd = attackSkill.shouldAddEffect ();
			if (shouldAdd) {
				SkillEffect skillComponent = projectileToFire.AddComponent (attackSkill.componentName) as SkillEffect;
				skillComponent.setUpProjectile (damage, weaponType);
			}
		}
		
		return projectileToFire;
	}
	
	/// <summary>
	/// Creates the bullet.
	/// </summary>
	/// <returns>The bullet.</returns>
	private GameObject createProjectile (Transform gunOrigin)
	{
		GameObject newProjectile = Instantiate (bullet, gunOrigin.position, gunOrigin.rotation) as GameObject;	
		return newProjectile;
	}
}
