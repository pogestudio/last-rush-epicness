using UnityEngine;
using System.Collections;

public class CriticalHit : Skill
{	
	private int CritHitMultiplier = 2;
	
	private override void doDamage (GameObject colliderObject)
	{
		int critDamage = CritHitMultiplier * baseShotDamage;
		doDamageToSingleTarget (colliderObject, critDamage, currentWeaponType);
	}
	
	private override void createEffect (GameObject colliderObject)
	{
		
	}
}

