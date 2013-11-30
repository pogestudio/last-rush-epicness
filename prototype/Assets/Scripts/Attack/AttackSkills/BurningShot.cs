using UnityEngine;
using System.Collections;

/// <summary>
/// A skill that ads a burning effect. Use as the other attack skills. 
/// </summary>
public class BurningShot : SkillEffect
{	
	private float burningTime = 5F;
	private float burningMultiplier = 0.5F;
	
	public override void doDamage (GameObject colliderObject)
	{
		if (targetIsEnemy (colliderObject.gameObject)) {
			BurnDPSEffect burningeffect = colliderObject.gameObject.AddComponent<BurnDPSEffect> ();
			burningeffect.weaponToCauseIt = currentWeaponType;
			burningeffect.burningDamage = burningMultiplier * baseShotDamage;
			burningeffect.burningDuration = burningTime;
		}
	}
	
	public override void createEffect (GameObject colliderObject)
	{
		
	}
}

