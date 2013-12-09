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
	public WeaponTypes thisType;
	
	private float despawnDelay = 20F; //if it hasnt been picked up in 20, despawn.
	private float despawnSafety = (float)60 * 60 * 2; //incremetn despawn time with this if you pick it up. Should be a lot, like two hours
	private float timeToDespawn;

    private GameObject weaponGlow;
	
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

    public float getDPS()
    {
        return weaponDamage / timeBetweenShots;
    }

	protected void Awake ()
	{
        weaponGlow = GetComponentInChildren<WeaponGlow>().gameObject;
        if (weaponGlow == null) {
            Debug.Log("No weaponGlow found");
        }
		if (gunMuzzle == null) {
			Debug.Log ("Weapon muzzle is not set");
		}
		if (!gameObject.activeInHierarchy) {
			Debug.Log ("The weapon is deactive in hierarchy...?");
		}
		WeaponManager.get().manage(this);
	}


	public void Start ()
	{
	}

	public void Update ()
	{
		if (mode == WeaponMode.RAGDOLL && Time.time > timeToDespawn) {
			Destroy (gameObject);
		}
	}


	/// <summary>
	/// Should be called by the sub-class (specific weapon) whenever it wants to instantiate a projectile
	/// </summary>
	public abstract void fire ();

	public abstract void triggerDown ();
	public abstract void triggerHold ();
	public abstract void triggerUp ();
	
	

}
