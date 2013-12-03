using UnityEngine;
using System.Collections;

public class MonsterHomingMissileScript : MonoBehaviour
{
		
	public GameObject PlayerToAimAt;
	public float searchRadius = 80F;
	public float projectileSpeed;
	public int damage;
	public float destroyTime = 10F;
		
	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("projectile speed issss..." + projectileSpeed);
		Destroy (gameObject, destroyTime);
	}
		
	// Update is called once per frame
	void Update ()
	{
		if (PlayerToAimAt == null || !PlayerToAimAt.activeSelf) {
			setNewTarget ();
			return;
		} else {
			//Debug.Log ("Missile has target");
		}
		gameObject.transform.LookAt (PlayerToAimAt.transform.position);
		gameObject.rigidbody.velocity = (gameObject.transform.forward * projectileSpeed);
		//Debug.Log ("Setting missile velocity to " + gameObject.rigidbody.velocity);
		Debug.Log ("current velocity :: " + gameObject.rigidbody.velocity);
		Debug.Log ("current forward is :: " + gameObject.transform.forward);
		Debug.Log ("current projectilespeed is :: " + projectileSpeed);
	}
		
	void setNewTarget ()
	{
		PlayerToAimAt = PlayerFinder.sharedHelper ().getClosestPlayer (gameObject.transform.position, searchRadius);
	}
	
	void OnCollisionEnter (Collision hit)
	{
		if (PlayerFinder.sharedHelper ().targetIsPlayer (hit.gameObject)) {
			hit.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
			damage = 0; //make sure we can't hit twice
			Destroy (gameObject);
		}
	}
}
	
