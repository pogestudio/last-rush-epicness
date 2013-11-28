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
	
	//private 
	private GameObject player; //using this to access the current gun. Maybe the weapon type should be pass on when requesting the projectile instead?

	public static ProjectileFactory sharedFactory ()
	{
		return instance;
	}
	// Use this for initialization
	void Start ()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	/// <summary>
	/// Call from any object belonging to player, for example an equipped gun.
	/// </summary>
	/// <returns>The projectile, in a stand still state at the transform point given</returns>
	public GameObject deliverProjectile (Transform gunOrigin, WeaponTypes weaponType, int weaponDamage)
	{
		AttackSkills skillToApply = AttackHandler.sharedHandler ().typeOfShotToFire ();
		GameObject projectileToFire = createProjectile (gunOrigin);
		addAttackSkills (projectileToFire, skillToApply, weaponDamage, weaponType);
		
		
		return projectileToFire;
	}
	
	/// <summary>
	/// Responsible for creating the adding the attack skill component to the projectile
	/// as well as setting it up.
	/// If no special attack, just return the projectile. 
	/// </summary>
	/// <returns>The projectile.</returns>
	/// <param name="skillToApply">Skill to apply.</param>
	private GameObject addAttackSkills (GameObject projectileToFire, AttackSkills skillToApply, int damage, WeaponTypes weaponType)
	{
		
		switch (skillToApply) {
		case AttackSkills.CriticalHit:
			{
				
				CriticalHit collisionLogic = projectileToFire.AddComponent<CriticalHit> ();
				collisionLogic.baseShotDamage = damage;
				collisionLogic.currentWeaponType = weaponType;
				break;
			}
		case AttackSkills.BurningEffect:
			{
				BurningEffect collisionLogic = projectileToFire.AddComponent<BurningEffect> ();
				collisionLogic.baseShotDamage = damage;
				collisionLogic.currentWeaponType = weaponType;
				break;
			}
		case AttackSkills.RegularShot:
			{
				RegularShot collisionLogic = projectileToFire.AddComponent<RegularShot> ();
				collisionLogic.baseShotDamage = damage;
				collisionLogic.currentWeaponType = weaponType;
				break;
			}
		default:
			{
				Debug.LogError ("We have more skills than we implemented in the factory!!");
				break;
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
