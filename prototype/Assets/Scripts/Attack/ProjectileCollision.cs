using UnityEngine;
using System.Collections;

/*
This component handles the logic of projectile collisioning with an object.
damage - the damage of the object

Could be refactored to take a tag name, and check the tag of the collision game object.
*/

public class ProjectileCollision : MonoBehaviour
{
	public int damage = 0;
	public WeaponTypes currentWeaponType;

	// Use this for initialization
	void Start ()
	{
		if (damage == 0)
			Debug.Log ("Damage is 0 on a projectile, fix!");
	}
	
	void OnCollisionEnter (Collision collision)
	{
		GameObject target = collision.gameObject;
		target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
		ExperienceHandler.sharedHandler ().damagesWasDealt (damage, currentWeaponType);
			

		Destroy (gameObject);
	}
}
