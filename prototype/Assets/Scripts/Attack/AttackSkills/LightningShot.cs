using UnityEngine;
using System.Collections;

public class LightningShot : SkillEffect
{
	
	public override void doDamage (GameObject colliderObject)
	{
	
	}
	
	public override void createEffect (GameObject colliderObject)
	{
		if (targetIsEnemy (colliderObject)) {
			//Debug.Log ("Lightning strike added");
			LightningStrike lStrike = colliderObject.AddComponent<LightningStrike> ();
			lStrike.shotDamage = baseShotDamage;
			lStrike.damageMultiplier = 0.5F;
			lStrike.currentMonster = colliderObject;
			lStrike.weaponToCauseIt = currentWeaponType;
			
		}
	}
}