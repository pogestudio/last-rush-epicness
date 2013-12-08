using UnityEngine;
using System.Collections;

public class Gun : AbstractWeapon
{

	private float delay = 0;
	
	void Start ()
	{
		base.Start ();
		thisType = WeaponTypes.HandGun;
         
		timeBetweenShots = 0.5f;
		bulletSpeed = 35;

        gunMuzzle.light.enabled = false;
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
        StartCoroutine(flash());
		audio.Play ();
    }

    //shoot flash coroutine
    private IEnumerator flash()
    {
        gunMuzzle.light.enabled = true;
        yield return 0;
        gunMuzzle.light.enabled = false;
    }
		
}
