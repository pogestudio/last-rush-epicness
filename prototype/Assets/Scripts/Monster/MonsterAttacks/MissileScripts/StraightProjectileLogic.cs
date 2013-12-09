using UnityEngine;
using System.Collections;

public class StraightProjectileLogic : MonoBehaviour
{

		public int damage = 1;
		public float destroyTime = 10F;
		public float projectileSpeed = 20F;
		public Vector3 initialDirection;
		public float waitTime = 3;
		
		private float timeToStartMoving;
	
		// Use this for initialization
		void Start ()
		{
				destroyTime = destroyTime + waitTime;
				Destroy (gameObject, destroyTime);
				
				initialDirection.Normalize ();
				timeToStartMoving = Time.time + waitTime;
		}
	

		// Update is called once per frame
		void Update ()
		{
				if (Time.time > timeToStartMoving) {
						gameObject.rigidbody.velocity = initialDirection * projectileSpeed;
				}
	
		}
	
		void OnCollisionEnter (Collision hit)
		{
				if (PlayerFinder.sharedHelper ().targetIsPlayer (hit.gameObject) && damage > 0) {
						hit.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
						damage = 0; //make sure we can't hit twice
						Destroy (gameObject);
				}
		}
}
