using UnityEngine;
using System.Collections;

enum BossSpecialAttacks
{
		HomingMissiles = 0,
		LightningAttack,
		FireStorm,
		BossSpecialAttacksMAX,
}


public class MiniBossLogic : AbstractEnemy
{
	
		private bool isCasting = false;
		private float nextSpecialAttack = 3; // don't cast at once. 
		public float timeBetweenSpecialAttacks;
		private float timetoStopCasting;
		private float castingTime;
		
		private RigidbodyConstraints constraintsFromEditor;
	
		bool castLightning;
	
		private BossSpecialAttacks specialAttackToPerform;
	
		float lightningThickness = 0.3F;
		float lightningAttackMaxDistance = 20;
	
	
		/// FOR SIZE
	
		float startWidth;
		float minimumWidth;
		float startHealth;
	
	
		protected override void setUpMonsterAtStart ()
		{
//				Debug.Log ("miniboss set up!");
				startWidth = gameObject.renderer.bounds.size.x;
				minimumWidth = startWidth * 0.3F;
				startHealth = health;
				
				constraintsFromEditor = transform.rigidbody.constraints;
		
		}
	
		void Update ()
		{
				updateSize ();
		
				if (networkView.isMine) {
			
//						if (!target) {
//								target = PlayerFinder.sharedHelper ().getClosestPlayer (transform.position, searchRadius);
//								return;
//						}
			
						if (castLightning) { //one of the special attacks. for a nice special effects it should be here.
								//&& ((timetoStopCasting - Time.time) / castingTime < Mathf.Pow (Random.value, 2))
								//bail out proportionally, so that it's thicker at the end of the casting time.
								transform.LookAt (target.transform);
								castLightningAttack ();
						}
						if (isCasting && Time.time > timetoStopCasting) {
								//should stop casting
								isCasting = false;
								nextSpecialAttack = Time.time + timeBetweenSpecialAttacks;
								unFreezePosition ();
								performSpecialAttack ();
								//Debug.Log ("Shoud stop casting. time to cast: " + nextSpecialAttack);
						}
			
			
						if (Time.time > nextSpecialAttack && !isCasting && target) {
								//should start casting
								randomizeNextSpecialAttack ();
								isCasting = true;
								freezePosition ();
								castAttack ();
								timetoStopCasting = Time.time + castingTime;
						}
			
						if (target && !isCasting) {
								walk ();
						} else if (!target) {
								target = PlayerFinder.sharedHelper ().getClosestPlayer (transform.position, searchRadius);
						}
				}
		
		}
		
		void freezePosition ()
		{
				transform.rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
		}
		
		void unFreezePosition ()
		{
				transform.rigidbody.constraints = constraintsFromEditor;
		}
	
		void updateSize ()
		{
				float currentSize = gameObject.renderer.bounds.size.x;
				float currentHealthProgess = health / startHealth;
				float sizeWeShouldBeAt = startWidth * currentHealthProgess;
		
				//				Debug.Log ("current size: " + currentSize);
				//				Debug.Log ("health prog: " + currentHealthProgess);
				//				Debug.Log ("sizeWeshouldbeat: " + sizeWeShouldBeAt);
				sizeWeShouldBeAt = Mathf.Max (minimumWidth, sizeWeShouldBeAt);
				
				//float diff = sizeWeShouldBeAt / currentSize;
				//Debug.Log ("Diff : " + diff);
				
				Vector3 newSize = new Vector3 (sizeWeShouldBeAt, sizeWeShouldBeAt, sizeWeShouldBeAt);
				networkView.RPC ("setSizeRPC", RPCMode.All, newSize);
		}
	
		void performSpecialAttack ()
		{
		
				switch (specialAttackToPerform) {
				case BossSpecialAttacks.HomingMissiles:
						{
								shootHomingMissiles ();
								break;
						}
				case BossSpecialAttacks.LightningAttack:
						{
								castLightning = false;
								shootLightningAttack ();
								break;
						}
				case BossSpecialAttacks.FireStorm:
						{
								//do nothinnn.
								break;
						}
			
				default:
						Debug.Log ("we have more miniboss skills than we are managing");
						break;
				}
		
		
		}
	
		void castAttack ()
		{
				switch (specialAttackToPerform) {
				case BossSpecialAttacks.HomingMissiles:
						{
								castingTime = 3.0F;
								EffectFactory.sharedFactory ().createMiniBossChargingEffect (gameObject, castingTime);
								break;
						}
				case BossSpecialAttacks.LightningAttack:
						{
								castingTime = 8.0F;
								castLightning = true;
								break;
						}
				case BossSpecialAttacks.FireStorm:
						{
								createFireStorm ();
								break;
						}
			
				default:
						Debug.Log ("we have more miniboss skills than we are managing");
						break;
				}
		
		}
		
		void createFireStorm ()
		{
				//these should be created now and then set a timer. When its time, theyll start moving by themselves.
				castingTime = 3.0F;
				float projectileSpeed = 20;
				for (float i = 0; i < 360; i = i + 45) {
						float z = Mathf.Sin (i) * 2;
						float x = Mathf.Cos (i) * 2;
						GameObject proj = MonsterAttackFactory.sharedFactory ().createMiniBossStraightProjectile (gameObject.transform);
						Vector3 newVector = new Vector3 (x, 0, z);
						proj.transform.position = proj.transform.position + newVector;
						StraightProjectileLogic logic = proj.GetComponent<StraightProjectileLogic> ();
						logic.initialDirection = newVector;
						logic.projectileSpeed = projectileSpeed;
						logic.waitTime = castingTime;
						//Debug.Log ("spawned projectile at position::" + newVector);
				}
		
		}
	
		void shootHomingMissiles ()
		{
				GameObject newProjectile = MonsterAttackFactory.sharedFactory ().createMiniBossHomingProjectile (gameObject.transform);
				newProjectile.transform.position = newProjectile.transform.position + new Vector3 (0, 0, 2);
		
		
				GameObject anotherProj = MonsterAttackFactory.sharedFactory ().createMiniBossHomingProjectile (gameObject.transform);
				anotherProj.transform.position = anotherProj.transform.position + new Vector3 (2, 0, 0);
		}
	
		void shootLightningAttack ()
		{
				float distance = Vector3.Distance (target.transform.position, transform.position);
				Vector3 lightPos = target.transform.position;
				if (distance > lightningAttackMaxDistance) {
						Vector3 directionOfPlayer = target.transform.position - transform.position;
						directionOfPlayer.Normalize ();
						lightPos = transform.position + directionOfPlayer * lightningAttackMaxDistance;
				
				} else {
						//if its inside, do damage
						int damage = 1;
						target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
				}
		
				EffectFactory.sharedFactory ().createLightningBetween (transform.position, lightPos, lightningThickness * 3F);
				
		
		}
	
		void randomizeNextSpecialAttack ()
		{
				//Debug.Log ("Attack MAx" + (int)BossSpecialAttacks.BossSpecialAttacksMAX);
				float randomizedFloat = Random.Range (0F, (float)((int)BossSpecialAttacks.BossSpecialAttacksMAX - 1));
				//Debug.Log ("randomized float" + randomizedFloat);
				int randomInt = Mathf.RoundToInt (randomizedFloat);
				specialAttackToPerform = (BossSpecialAttacks)randomInt;
				//Debug.Log ("random int: " + randomInt + " special attack : " + specialAttackToPerform);
		}
	
		void castLightningAttack ()
		{
				float timeLeftToCast = timetoStopCasting - Time.time;
				float castingProgress = 1 - timeLeftToCast / castingTime;
				Vector3 pointForwardOfMiniBoss = transform.position + transform.forward * 5;
				EffectFactory.sharedFactory ().createLightningBetween (transform.position, pointForwardOfMiniBoss, lightningThickness * castingProgress);
				//Debug.Log ("casting lightning between points: " + transform.position);
				//Debug.Log ("and: " + pointForwardOfMiniBoss);
		
		}
		
	
		[RPC]
		void setSizeRPC (Vector3 newSize)
		{
				transform.localScale = newSize;
		}
	
	
}
