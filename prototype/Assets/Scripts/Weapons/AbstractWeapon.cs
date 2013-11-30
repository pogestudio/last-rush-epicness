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

	private WeaponMode mode;
	public WeaponMode Mode {
		get { return mode; }
		set {
			this.mode = value;
			switch (mode) {
			case WeaponMode.RAGDOLL:
				{
					rigidbody.isKinematic = false;
					collider.enabled = true;
					break;
				}
			case WeaponMode.HOLSTER:
				{
					rigidbody.isKinematic = true;
					collider.enabled = false;
					break;
				}
			case WeaponMode.HAND:
				{
					rigidbody.isKinematic = true;
					collider.enabled = false;
					break;
				}
			}
		}
	}

	protected void Awake ()
	{
		if (gunMuzzle == null) {
			Debug.Log ("Weapon muzzle is not set");
		}
        WeaponManager.get().manage(this);
	}


    public void Start()
    {
    }

    public void Update()
	{
		if (mode == WeaponMode.HAND) {
			//changing this bloc allow to change the way to fire all weapons
			if (Input.GetButtonDown ("Fire1"))
				triggerDown ();
			else if (Input.GetButtonUp ("Fire1"))
				triggerUp ();

			if (Input.GetButton ("Fire1"))
				triggerHold ();
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
