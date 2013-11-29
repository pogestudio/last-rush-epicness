using UnityEngine;
using System.Collections;

public class CriticalHit : SkillEffect
{	
	private int CritHitMultiplier = 2;
	
	public override void doDamage (GameObject colliderObject)
	{
		
		//int critDamage = CritHitMultiplier * baseShotDamage;
		//we will only do single damage, since we are already doing regular shot damage. 
		doDamageToSingleTarget (colliderObject, baseShotDamage, currentWeaponType);
	}
	
	public override void createEffect (GameObject colliderObject)
	{
		
	}
}

