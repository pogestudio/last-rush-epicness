using UnityEngine;
using System.Collections;


public class MiniBossLogic : AbstractEnemy
{	

	private bool isCasting = false;
	private float nextSpecialAttack = 3; // don't cast at once. 
	public float timeBetweenSpecialAttacks;
	private float timetoStopCasting;
	public float castingTime;
	
	public GameObject chargingEffect;
	
	
	void Start ()
	{	
		chargingEffect = EffectFactory.sharedFactory ().deliverSnowingFire (gameObject.transform);
		chargingEffect.transform.parent = transform;
		chargingEffect.transform.localPosition = new Vector3 (0, -0.2F, 0);
		chargingEffect.transform.localScale = new Vector3 (1, 1, 1);
		chargingEffect.transform.Rotate (-90F, 0, 0);
		chargingEffect.SetActive (false);
		
	}

	void Update ()
	{
	
		
		if (target && !isCasting) {
			walk ();
		} else if (!target) {
			target = PlayerFinder.sharedHelper ().getClosestPlayer (transform.position, searchRadius);
		}	
		
		
		if (isCasting && Time.time > timetoStopCasting) {
			isCasting = false;
			nextSpecialAttack = Time.time + timeBetweenSpecialAttacks;
			chargingEffect.SetActive (false);
			performSpecialAttack ();
			//Debug.Log ("Shoud stop casting. time to cast: " + nextSpecialAttack);
		}
		
		
		if (Time.time > nextSpecialAttack && !isCasting && target) {
			timetoStopCasting = Time.time + castingTime;
			isCasting = true;
			chargingEffect.SetActive (true);
		}
		
	}
	
	void performSpecialAttack ()
	{
		
		GameObject newProjectile = MonsterAttackFactory.sharedFactory ().miniBossProjectile (gameObject.transform);
		newProjectile.transform.position = newProjectile.transform.position + new Vector3 (0, 0, 2);
		
		
		GameObject anotherProj = MonsterAttackFactory.sharedFactory ().miniBossProjectile (gameObject.transform);
		anotherProj.transform.position = anotherProj.transform.position + new Vector3 (2, 0, 0);
		
		
		
	}

}
