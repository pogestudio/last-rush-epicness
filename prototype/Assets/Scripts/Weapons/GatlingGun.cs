using UnityEngine;
using System.Collections;

public class GatlingGun : AbstractWeapon
{

    public float heatTime = 7f;
    public float spinUpTime = 1.8f;
    public float maxRotationSpeed = 1000f;

    public GameObject spinningPart;

    public AudioSource spinSound;
    public AudioSource shootSound;

    private float heatTimer = 0f;
    private float spinUpTimer = 0f;

    private bool trigger = false;
    private float fireDelay = 0f;

    private ParticleSystem smokeEmiter;

    // Use this for initialization
    void Start()
    {
        base.Start();
        type = WeaponTypes.Gatling;
        timeBetweenShots = 0.05f;
        bulletSpeed = 20;

        gunMuzzle.light.enabled = false;
        smokeEmiter = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        //firing and timers
        fireDelay -= Time.deltaTime;

        if (trigger)
            spinUpTimer += Time.deltaTime;
        else
            spinUpTimer -= Time.deltaTime / 2;    //spin up twice faster than slowing down

        spinUpTimer = Mathf.Clamp(spinUpTimer, 0, spinUpTime);

        if (spinUpTimer >= spinUpTime)
        {
            heatTimer += Time.deltaTime;
            if (fireDelay <= 0f && heatTimer <= heatTime)
            {
                fire();
                fireDelay = timeBetweenShots;
            }
        }
        else
        {
            heatTimer -= Time.deltaTime * 2; //cools twice faster than heating
        }

        heatTimer = Mathf.Max(heatTimer, 0);

        //smoke trigger
        if (heatTimer >= heatTime)
        {
            shootSound.Stop();
            smokeEmiter.loop = true;
            if (!smokeEmiter.isPlaying)
                smokeEmiter.Play();
        }
        else
        {
            smokeEmiter.loop = false;
        }

        //rotation animation
        if (spinningPart)
        {
            float dRotation = maxRotationSpeed * Time.deltaTime * (spinUpTimer / spinUpTime);
            spinningPart.transform.Rotate(0, 0, dRotation);
        }

    }

    public override void triggerDown()
    {
        trigger = true;
        spinSound.Play();
    }
    public override void triggerHold()
    {

    }
    public override void triggerUp()
    {
        trigger = false;
        shootSound.Stop();
        spinSound.Stop();
    }

    public override void fire()
    {
        //TODO : handle different projectile types?
        GameObject projectile = ProjectileFactory.sharedFactory().deliverProjectile(gunMuzzle, type, weaponDamage);
        projectile.rigidbody.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
        //Debug.Log("Weapon damage::" + weaponDamage);
        StartCoroutine(flash());
        shootSound.Play();

    }

    //shoot flash coroutine
    private IEnumerator flash()
    {
        gunMuzzle.light.enabled = true;
        yield return 0;
        gunMuzzle.light.enabled = false;
    }

    public override float normalizedWeaponSpeed()
    {
        return (timeBetweenShots + (heatTime + spinUpTime) / (heatTime / timeBetweenShots));
    }
}
