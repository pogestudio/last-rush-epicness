using UnityEngine;
using System.Collections;

public class HomingShot : SkillEffect
{

	private GameObject monsterToAimAt;
	private float searchRadius = 20F;
	private float projectileSpeed;

	// Use this for initialization
	void Start ()
	{
		projectileSpeed = gameObject.rigidbody.velocity.magnitude;
		//Debug.Log ("projectile speed issss..." + projectileSpeed);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (monsterToAimAt == null || !monsterToAimAt.activeSelf) {
			setNewTarget ();
			return;
		} else {
			//Debug.Log ("Missile has target");
		}
		gameObject.transform.LookAt (monsterToAimAt.transform.position);
		
		gameObject.rigidbody.velocity = (gameObject.transform.forward * projectileSpeed);
		//Debug.Log ("Setting missile velocity to " + gameObject.rigidbody.velocity);
		
		
	}
	
	void setNewTarget ()
	{
		ArrayList monstersAroundMe = monstersWithinArea (gameObject.transform.position, searchRadius);
		/*foreach (GameObject monster in monstersAroundMe) {
			Debug.Log ("Monster position: " + monster.transform.position);
		}*/
		CompareDistance comparer = new CompareDistance (gameObject);
		monstersAroundMe.Sort (comparer);
		
		//Debug.Log ("Monster SORTED!");
		
		/*foreach (GameObject monster in monstersAroundMe) {
			Debug.Log ("Monster position: " + monster.transform.position);
		}*/
		if (monstersAroundMe.Count > 0) {
			monsterToAimAt = (GameObject)monstersAroundMe [0];
		}
		
		
		
	}
	
	public override void createEffect (GameObject colliderObject)
	{
		//EffectFactory.sharedFactory ().deliverSphericNova (transform);
	}
	
	public override void doDamage (GameObject colliderObject)
	{
		//EffectFactory.sharedFactory ().deliverSphericNova (transform);
	}
}
