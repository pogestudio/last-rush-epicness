using UnityEngine;
using System.Collections;

public class AbstractEnemy : MonoBehaviour
{
		private GameObject[] effects;

		public int health;
		public float defaultMovingSpeed;
		float _movingSpeed;
		public float movingSpeed {
				get { return _movingSpeed; }
				set { _movingSpeed = Mathf.Max (value, 0); }
		}
		public GameObject target;
		public float searchRadius = 80F;

		private void die ()
		{
				//Debug.Log ("Monster should die");
				DropsLoot lootComponent = gameObject.GetComponent<DropsLoot> ();
				lootComponent.ShouldDropLoot ();
				Network.Destroy (gameObject);
		}


		public void applyDamage (int damage)
		{
				networkView.RPC ("applyDamageRPC", RPCMode.All, damage);
		}

		[RPC]
		private void applyDamageRPC (int damage)
		{

				//real damage
				if (networkView.isMine) {
						health -= damage;


						if (health <= 0) {
								die ();
						}
				}
		}

		// Use this for initialization
		void Start ()
		{
				movingSpeed = defaultMovingSpeed; // set the moving speed we have from editor. 

				if (!target) {
						Debug.Log ("Does not have initial target");
				}
				
				setUpMonsterAtStart ();
				
		}
		
		protected virtual void setUpMonsterAtStart ()
		{
		
		}

        //should only be called within FixedUpdate
		protected void walk ()
		{
				if (!networkView.isMine)
						Debug.LogWarning ("walk is called, shouldnt be since this object is a replicate");

                //Vector3 delta = target.transform.position - transform.position;
                //delta.Normalize ();
                //float moveSpeed = movingSpeed;
                ////transform.position = transform.position + (delta * moveSpeed);
                //rigidbody.velocity = delta * moveSpeed;
                ////rigidbody.AddForce (delta * moveSpeed, ForceMode.Acceleration);
                //transform.LookAt (target.transform);

                Vector3 delta = target.transform.position - transform.position;
                delta.Normalize ();
                rigidbody.velocity = Time.deltaTime * movingSpeed * delta;
                transform.LookAt(target.transform);
				
		}

}

