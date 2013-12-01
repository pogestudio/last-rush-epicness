using UnityEngine;
using System.Collections;

public class MachineGun : AbstractWeapon
{


	
	private float delay = 0;
	

	void Start ()
	{
		base.Start ();
		thisType = WeaponTypes.MachineGun;
		weaponDamage = 5;
		timeBetweenShots = 0.2f;
		bulletSpeed = 20;
	}

	void Update ()
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
		Debug.Log ("Weapon damage::" + weaponDamage);
	}
}
