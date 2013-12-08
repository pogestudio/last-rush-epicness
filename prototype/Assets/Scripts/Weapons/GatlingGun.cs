using UnityEngine;
using System.Collections;

public class GatlingGun : AbstractWeapon
{

		public float heatTime = 5f;
		public float spinUpTime = 2f;
		public float maxRotationSpeed = 200f;

		public GameObject spinningPart;
		public GameObject heatModel;

		private float heatTimer = 0f;
		private float spinUpTimer = 0f;

		private bool trigger = false;
		private float fireDelay = 0f;

		private ParticleSystem smokeEmiter;

		// Use this for initialization
		void Start ()
		{
				base.Start ();
				thisType = WeaponTypes.Gatling;
				timeBetweenShots = 0.05f;
				bulletSpeed = 20;

				gunMuzzle.light.enabled = false;
				smokeEmiter = GetComponentInChildren<ParticleSystem> ();
		}

		// Update is called once per frame
		void Update ()
		{

				//firing and timers
				fireDelay -= Time.deltaTime;

				if (trigger)
						spinUpTimer += Time.deltaTime;
				else
						spinUpTimer -= Time.deltaTime;

				spinUpTimer = Mathf.Clamp (spinUpTimer, 0, spinUpTime);

				if (spinUpTimer >= spinUpTime) {
						heatTimer += Time.deltaTime;
						if (fireDelay <= 0f && heatTimer <= heatTime) {
								fire ();
								fireDelay = timeBetweenShots;
						}
				} else {
						heatTimer -= Time.deltaTime;
				}
        
				heatTimer = Mathf.Max (heatTimer, 0);

				//smoke trigger
				if (heatTimer >= heatTime) {
						audio.Stop();
						smokeEmiter.loop = true;
						if (!smokeEmiter.isPlaying)
								smokeEmiter.Play ();
				} else {
						smokeEmiter.loop = false;
				}

				//rotation animation
				if (spinningPart) {
						float dRotation = maxRotationSpeed * Time.deltaTime * (spinUpTimer / spinUpTime);
						spinningPart.transform.Rotate (0, 0, dRotation);
				}

				//heat

		}

		public override void triggerDown ()
		{
				trigger = true;
		}
		public override void triggerHold ()
		{
        
		}
		public override void triggerUp ()
		{
				trigger = false;
		}

		public override void fire ()
		{
				//TODO : handle different projectile types?
				GameObject projectile = ProjectileFactory.sharedFactory ().deliverProjectile (gunMuzzle, thisType, weaponDamage);
				projectile.rigidbody.velocity = transform.TransformDirection (Vector3.forward * bulletSpeed);
				//Debug.Log("Weapon damage::" + weaponDamage);
				StartCoroutine (flash ());
		audio.Play ();

		}

		//shoot flash coroutine
		private IEnumerator flash ()
		{
				gunMuzzle.light.enabled = true;
				yield return 0;
				gunMuzzle.light.enabled = false;
		}
}
