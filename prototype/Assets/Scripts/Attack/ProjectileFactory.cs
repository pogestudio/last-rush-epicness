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
	public Rigidbody bullet;

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
	public Rigidbody deliverProjectile (Transform gunOrigin)
	{
		AttackSkills skillToApply = AttackHandler.sharedHandler ().typeOfShotToFire ();
		Rigidbody projectileToFire = createProjectile (gunOrigin);
		addAttackSkills (projectileToFire, skillToApply);
		
		
		return projectileToFire;
	}
	
	/// <summary>
	/// Responsible for creating the project with a special, or regular, attack.
	/// If no special attack, just return the projectile. 
	/// </summary>
	/// <returns>The projectile.</returns>
	/// <param name="skillToApply">Skill to apply.</param>
	private Rigidbody addAttackSkills (Rigidbody projectileToFire, AttackSkills skillToApply)
	{
		switch (skillToApply) {
		case AttackSkills.CriticalHit:
			{
				//projectileToFire.AddComponent ("");
				break;
			}
		default:
			break;
		}
		return projectileToFire;
	}
	
	/// <summary>
	/// Creates the bullet.
	/// </summary>
	/// <returns>The bullet.</returns>
	private Rigidbody createProjectile (Transform gunOrigin)
	{
		int speed = 20;
		
		Rigidbody newProjectile = Instantiate (bullet, gunOrigin.position, gunOrigin.rotation) as Rigidbody;
		newProjectile.gameObject.SetActive (true);
		newProjectile.velocity = gunOrigin.TransformDirection (Vector3.forward * speed);
		ProjectileCollision collisionLogic = newProjectile.GetComponent<ProjectileCollision> () as ProjectileCollision;
		collisionLogic.currentWeaponType = WeaponTypes.MachineGun;
		//Debug.Log (Vector3.forward * speed);
		Destroy (newProjectile.gameObject, 10);
		return newProjectile;
	}
}
