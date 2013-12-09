using UnityEngine;
using System.Collections;

public enum WeaponMode
{
		/// <summary>
		/// weapon is handled by the physics engine and is pickable
		/// </summary>
		RAGDOLL,

		/// <summary>
		/// weapon is carried by a player but won't fire
		/// </summary>
		HOLSTER,

		/// <summary>
		///  weapon is carried by a player and will kill zombies :)
		/// </summary>
		HAND
}

public abstract class AbstractWeapon : MonoBehaviour
{
		public Transform gunMuzzle;
		public GameObject glowingModel;
		protected WeaponTypes type;
		public WeaponTypes Type {
				get { return type; }
		}

		private float despawnDelay = 20F; //if it hasnt been picked up in 20, despawn.
		private float despawnSafety = (float)60 * 60 * 2; //incremetn despawn time with this if you pick it up. Should be a lot, like two hours
		private float timeToDespawn;

		private Vector3 GlowbaseScale;

		public float timeBetweenShots;
		public int bulletSpeed;
		public int weaponDamage;

		protected WeaponMode mode;
		public WeaponMode Mode {
				get { return mode; }
				set {
						this.mode = value;
						switch (mode) {
						case WeaponMode.RAGDOLL:
								{
										rigidbody.isKinematic = false;
										collider.enabled = true;
										timeToDespawn = Time.time + despawnDelay;

										break;
								}
						case WeaponMode.HOLSTER:
								{
										rigidbody.isKinematic = true;
										collider.enabled = false;
										timeToDespawn = Time.time + despawnSafety;

										break;
								}
						case WeaponMode.HAND:
								{
										rigidbody.isKinematic = true;
										collider.enabled = false;
										timeToDespawn = Time.time + despawnSafety;

										break;
								}
						}
				}
		}



		protected void Awake ()
		{
				if (glowingModel != null) {
						GlowbaseScale = glowingModel.transform.localScale;
				} else {
						Debug.Log ("No weaponGlow found");
				}

				if (gunMuzzle == null) {
						Debug.Log ("Weapon muzzle is not set");
				}
				if (!gameObject.activeInHierarchy) {
						Debug.Log ("The weapon is deactive in hierarchy...?");
				}

				WeaponManager.get ().manage (this);
		}


		public void Start ()
		{
				StartCoroutine (updateGlowState ());
		}

		public void Update ()
		{
				if (mode == WeaponMode.RAGDOLL && Time.time > timeToDespawn) {
						Destroy (gameObject);
				}

		}

		IEnumerator updateGlowState ()
		{
				while (true) {
						AbstractWeapon playerWeaponScript = PlayerWeapons.getMainPlayerWeaponScript ();
						if (playerWeaponScript != null) {
								if (mode == WeaponMode.RAGDOLL && PlayerWeapons.getMainPlayerWeaponScript ().getDPS () < this.getDPS ()) {
										glowingModel.SetActive (true);
										StopCoroutine ("glowLoop");
										StartCoroutine ("glowLoop");
								} else {
										StopCoroutine ("glowLoop");
										glowingModel.SetActive (false);
								}
						} else {
								StopCoroutine ("glowLoop");
								glowingModel.SetActive (false);
						}
						yield return new WaitForSeconds (1f);
				}
		}

		IEnumerator glowLoop ()
		{
				while (true) {
						if (glowingModel) {
								float pulseSize = 0.5f;
								float pulseSpeed = 5f;
								glowingModel.transform.localScale = GlowbaseScale * ((Mathf.Sin (Time.time * pulseSpeed) + 2.1f) * pulseSize);
						}
						yield return null;
				}
		}

		/// <summary>
		/// Should be called by the sub-class (specific weapon) whenever it wants to instantiate a projectile
		/// </summary>
		public abstract void fire ();

		public abstract void triggerDown ();
		public abstract void triggerHold ();
		public abstract void triggerUp ();

		/// <summary>
		/// This should return weaponspeed, with consideration to "cooling effects" and stuff for gatling gun.
		/// Will be overwritten in subclasses where it matters.
		/// </summary>
		/// <returns>The weapon speed.</returns>
		public virtual float normalizedWeaponSpeed ()
		{
				return timeBetweenShots;
		}


		public float getDPS ()
		{
				return weaponDamage / normalizedWeaponSpeed ();
		}

}
