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
	public int standardDestroyTime = 10;

	// Use this for initialization
	void Start ()
	{
		if (damage == 0)
			Debug.Log ("Damage is 0 on a projectile, fix!");
	}
	
	
	public void doDamageTo (GameObject target, int damage)
	{
		target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
		reportDamageToExpHandler (damage);
	}
	
	private void reportDamageToExpHandler (int damage)
	{
		ExperienceHandler.sharedHandler ().damagesWasDealt (damage, currentWeaponType);
	}
	
	public bool targetIsEnemy (GameObject target)
	{
		return (target.tag == "RegularMonster");
	}
	
	void OnCollisionEnter (Collision collisionObject)
	{
		if (targetIsEnemy (collisionObject.gameObject)) {
			doDamageTo (collisionObject.gameObject, damage);			
		}	
		Destroy (gameObject);
	}
	
	public virtual int destroyTime ()
	{
		return standardDestroyTime;
	}
}
