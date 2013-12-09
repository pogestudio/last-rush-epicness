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
		
		bool castLightning;
		
		private BossSpecialAttacks specialAttackToPerform;
		
		float lightningThickness = 0.3F;
		float lightningAttackMaxDistance = 25;
		
		void Update ()
		{

				if (networkView.isMine) {
						if (!target) {
								target = PlayerFinder.sharedHelper ().getClosestPlayer (transform.position, searchRadius);
								return;
						}
						if (castLightning) { //one of the special attacks. for a nice special effects it should be here.
								//&& ((timetoStopCasting - Time.time) / castingTime < Mathf.Pow (Random.value, 2))
								//bail out proportionally, so that it's thicker at the end of the casting time.
								Debug.Log ("want to cast lightning");
								transform.LookAt (target.transform);
								castLightningAttack ();
						}
						if (isCasting && Time.time > timetoStopCasting) {
								//should stop casting
								isCasting = false;
								nextSpecialAttack = Time.time + timeBetweenSpecialAttacks;
								performSpecialAttack ();
								//Debug.Log ("Shoud stop casting. time to cast: " + nextSpecialAttack);
						}


						if (Time.time > nextSpecialAttack && !isCasting) {
								//should start casting
								randomizeNextSpecialAttack ();
								isCasting = true;
								castAttack ();
								timetoStopCasting = Time.time + castingTime;
						}

						if (!isCasting) {
								walk ();
						}
				}

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
								castingTime = 3.0F;
								timetoStopCasting = Time.time + castingTime;
								EffectFactory.sharedFactory ().createMiniBossChargingEffect (gameObject, castingTime);
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
								castingTime = 3.0F;
								EffectFactory.sharedFactory ().createMiniBossChargingEffect (gameObject, castingTime);
								break;
						}
			
				default:
						Debug.Log ("we have more miniboss skills than we are managing");
						break;
				}
		
		}
    
		void shootHomingMissiles ()
		{
				GameObject newProjectile = MonsterAttackFactory.sharedFactory ().miniBossProjectile (gameObject.transform);
				newProjectile.transform.position = newProjectile.transform.position + new Vector3 (0, 0, 2);
		
		
				GameObject anotherProj = MonsterAttackFactory.sharedFactory ().miniBossProjectile (gameObject.transform);
				anotherProj.transform.position = anotherProj.transform.position + new Vector3 (2, 0, 0);
		}
		
		void shootLightningAttack ()
		{
				float distance = Vector3.Distance (target.transform.position, transform.position);
				if (distance > lightningAttackMaxDistance) {
						Debug.Log ("Player is " + distance + " units away, aborting");
						return;
				}
				
				EffectFactory.sharedFactory ().createLightningBetween (transform.position, target.transform.position, lightningThickness * 3F);
				int damage = 1;
				target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
		
		}
		
		void randomizeNextSpecialAttack ()
		{
				int randomInt = Mathf.RoundToInt (Random.Range (0, (int)BossSpecialAttacks.BossSpecialAttacksMAX - 1));
				specialAttackToPerform = (BossSpecialAttacks)randomInt;
		}
		
		void castLightningAttack ()
		{
				float timeLeftToCast = timetoStopCasting - Time.time;
				float castingProgress = 1 - timeLeftToCast / castingTime;
				Vector3 pointForwardOfMiniBoss = transform.position + transform.forward * 5;
				EffectFactory.sharedFactory ().createLightningBetween (transform.position, pointForwardOfMiniBoss, lightningThickness * castingProgress);
				Debug.Log ("casting lightning between points: " + transform.position);
				Debug.Log ("and: " + pointForwardOfMiniBoss);
		
		}

}
