using UnityEngine;
using System.Collections;

public class HomingShot : SkillEffect
{

	public GameObject monsterToAimAt;
	public float searchRadius = 20F;
	public float projectileSpeed;

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
		monsterToAimAt = MonsterFinder.sharedHelper ().getClosestMonster (gameObject.transform.position, searchRadius);
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
