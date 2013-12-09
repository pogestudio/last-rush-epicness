using UnityEngine;
using System.Collections;

/*
This is a "hitter", and should be placed on objects which can do melee damage. 
The game object which handles melee should have this as a component.
That same (empty object) should also be placed slightly in front of the monster, otherwise it will hit itself.

damage - the damage of a hit.
range - the range of the attack

the fireRate should be static through all monsters, so that it is consistent.
If we add animation, so one can predict the hit from that, it could change. 

*/
public class MeleeSystem : MonoBehaviour
{

	public int damage;
	public float range;
	private float fireRate = 1.0F;

	//only used internally
	float nextFire = 0.0F;



	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		Ray targetHitRay = new Ray (transform.position, transform.TransformDirection (Vector3.forward));
		if (Physics.Raycast (targetHitRay, out hit, range)) {
			//Debug.Log("SOMETHING hit");
			if (PlayerFinder.sharedHelper ().targetIsPlayer (hit.transform.gameObject) && Time.time > nextFire) {
				hit.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
				nextFire = Time.time + fireRate;
			}
		}
	}
}
