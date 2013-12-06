using UnityEngine;
using System.Collections;

public class RegularShot : SkillEffect
{

	public bool wasTriggeredByMultishot;

	public override void doDamage (GameObject colliderObject)
	{
		doDamageToSingleTarget (colliderObject, baseShotDamage, currentWeaponType);
	}
	
	public override void createEffect (GameObject colliderObject)
	{
	
	}
}