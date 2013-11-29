using UnityEngine;
using System.Collections;

/// <summary>
/// A skill that ads a burning effect. Use as the other attack skills. 
/// </summary>
public class BurningEffect : Skill
{	
	private float burningTime = 5F;
	private float burningMultiplier = 0.5F;
	
	private override void doDamage (GameObject colliderObject)
	{
		if (targetIsEnemy (colliderObject.gameObject)) {
			doDamageToSingleTarget (colliderObject, baseShotDamage, currentWeaponType);
			BurnDPSEffect burningeffect = colliderObject.gameObject.AddComponent<BurnDPSEffect> ();
			burningeffect.weaponToCauseIt = currentWeaponType;
			burningeffect.burningDamage = burningMultiplier * baseShotDamage;
			burningeffect.burningDuration = burningTime;
		}
	}
	
	private override void createEffect (GameObject colliderObject)
	{
		
	}
	
}

