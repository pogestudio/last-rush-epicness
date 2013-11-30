using UnityEngine;
using System.Collections;

public class CriticalHit : SkillEffect
{	
	private float CritHitMultiplier = 1.5F;
	
	public override void doDamage (GameObject colliderObject)
	{
		
		int critDamage = Mathf.CeilToInt (CritHitMultiplier * baseShotDamage);
		//we will only do single damage, since we are already doing regular shot damage. 
		doDamageToSingleTarget (colliderObject, critDamage, currentWeaponType);
	}
	
	public override void createEffect (GameObject colliderObject)
	{
		
	}
}

