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
	/// <returns>The projectile.</returns>
	public GameObject deliverProjectile (Transform gunOrigin)
	{
		AttackSkills skillToApply = AttackHandler.sharedHandler ().typeOfShotToFire ();
		GameObject projectileToFire = createProjectile (gunOrigin);
		addAttackSkills (projectileToFire, skillToApply);
		
		
		return projectileToFire;
	}
	
	/// <summary>
	/// Responsible for creating the project with a special, or regular, attack.
	/// If no special attack, just return the projectile. 
	/// </summary>
	/// <returns>The projectile.</returns>
	/// <param name="skillToApply">Skill to apply.</param>
	private GameObject addAttackSkills (GameObject projectileToFire, AttackSkills skillToApply)
	{
		int weaponDamage = 50;
		WeaponTypes currentWeaponType = WeaponTypes.MachineGun;
		switch (skillToApply) {
		case AttackSkills.CriticalHit:
			{
				projectileToFire.AddComponent<CriticalHitProjectile> ();
				CriticalHitProjectile collisionLogic = projectileToFire.GetComponent<CriticalHitProjectile> () as CriticalHitProjectile;
				
				collisionLogic.damage = weaponDamage;
				collisionLogic.currentWeaponType = currentWeaponType;
				break;
			}
		default:
			{
				projectileToFire.AddComponent<ProjectileCollision> ();
				ProjectileCollision collisionLogic = projectileToFire.GetComponent<ProjectileCollision> () as ProjectileCollision;
				collisionLogic.damage = weaponDamage;
				collisionLogic.currentWeaponType = currentWeaponType;
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
	
		int weaponSpeed = 20;
		GameObject newProjectile = Instantiate (bullet, gunOrigin.position, gunOrigin.rotation) as GameObject;
		newProjectile.rigidbody.velocity = gunOrigin.TransformDirection (Vector3.forward * weaponSpeed);
		
		return newProjectile;
	}
}
