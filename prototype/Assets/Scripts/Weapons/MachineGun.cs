using UnityEngine;
using System.Collections;

public class MachineGun : AbstractWeapon
{

	public float timeBetweenShots = 0.2f;
	private float delay = 0;
	public int bulletSpeed = 20;
	
	public WeaponTypes thisType = WeaponTypes.MachineGun;
	public int weaponDamage = 5;
	

	void Start ()
	{
		thisType = WeaponTypes.MachineGun;
		weaponDamage = 5;
	}
	public void Update ()
	{
		base.Update ();
		delay -= Time.deltaTime;
	}

	public override void triggerDown ()
	{
	}

	public override void triggerHold ()
	{
		if (delay <= 0) {
			delay = timeBetweenShots;
			fire ();
		}
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
