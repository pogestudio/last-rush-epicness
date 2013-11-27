using UnityEngine;
using System.Collections;

public class Gun : AbstractWeapon
{
	public WeaponTypes thisType = WeaponTypes.HandGun;
	public int weaponDamage = 1;
	public int bulletSpeed = 10;
	
	void Start ()
	{

	}
	public override void triggerDown ()
	{
		fire ();
	}

	public override void triggerHold ()
	{
	}

	public override void triggerUp ()
	{
	}
	
	public override void fire ()
	{
		//TODO : handle different projectile types?
		GameObject projectile = ProjectileFactory.sharedFactory ().deliverProjectile (gunMuzzle, thisType, weaponDamage);
		projectile.rigidbody.velocity = transform.TransformDirection (Vector3.forward * bulletSpeed);
	}
		
}
