using UnityEngine;
using System.Collections;

public class Gun : AbstractWeapon
{
    public float timeBetweenShots = 0.5f;
	public int bulletSpeed = 35;
	public int weaponDamage = 20;

    private float delay = 0;
	
	void Start ()
	{
        base.Start();
         thisType = WeaponTypes.HandGun;
	}

    void Update()
    {
        base.Update();
        delay -= Time.deltaTime;
    }

    public override void triggerDown()
    {
    }

    public override void triggerHold()
    {
        if (delay <= 0)
        {
            delay = timeBetweenShots;
            fire();
        }
    }

    public override void triggerUp()
    {
    }
	
	public override void fire ()
	{
		//TODO : handle different projectile types?
		GameObject projectile = ProjectileFactory.sharedFactory ().deliverProjectile (gunMuzzle, thisType, weaponDamage);
		projectile.rigidbody.velocity = transform.TransformDirection (Vector3.forward * bulletSpeed);
	}
		
}
