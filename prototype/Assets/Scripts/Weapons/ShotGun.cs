using UnityEngine;
using System.Collections;

public class ShotGun : AbstractWeapon
{
    public int magazineSize = 6;
    public float firingRate = 0.3f; //delay beetween shots. Here timeBetween shot is the "reload" time since it's what defines the weapon DPS
    public float maxBulletSpread = 30f;
    public int bulletPerShot = 4;

    public AudioSource reloadSound;
    public AudioSource shotSound;
    public AudioSource pumpSound;

    private float delay = 0;
    private int loadedBullets;

    void Start()
    {
        base.Start();
        thisType = WeaponTypes.ShotGun;
        bulletSpeed = 20;
        timeBetweenShots = 0.6f;    //Here timeBetween shot is the "reload" time since it's what defines the weapon DPS

        loadedBullets = magazineSize;

        gunMuzzle.light.enabled = false;

        StartCoroutine("reloadLoop");
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
        if (loadedBullets>0 && delay<=0)
        {
            delay = firingRate;
            loadedBullets--;
            fire();
            StopCoroutine("reloadLoop");    //restart reload delay
            StartCoroutine("reloadLoop");
        }
    }

    public override void triggerUp()
    {
    }


    public override void fire()
    {
        //TODO : handle different projectile types?
        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject projectile = ProjectileFactory.sharedFactory().deliverProjectile(gunMuzzle, thisType, weaponDamage);
            
            float randomSpread = (Random.value - 0.5f) * maxBulletSpread;
            Quaternion randomRotation = Quaternion.Euler(0, randomSpread, 0);
            Vector3 direction = gunMuzzle.forward;
            direction = randomRotation * direction;
            direction.Normalize();
            projectile.rigidbody.velocity = direction * bulletSpeed;
        }

        StartCoroutine(flash());
        shotSound.Play();
    }

    //shoot flash coroutine
    private IEnumerator flash()
    {
        gunMuzzle.light.enabled = true;
        yield return 0;
        gunMuzzle.light.enabled = false;
    }

    private IEnumerator reloadLoop()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        while (true)
        {
            if (mode == WeaponMode.HAND && loadedBullets < magazineSize)
            {
                loadedBullets++;
                reloadSound.Play();
                if (loadedBullets == magazineSize)
                {
                    pumpSound.PlayDelayed(timeBetweenShots);
                }
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
