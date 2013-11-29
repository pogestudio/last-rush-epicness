using UnityEngine;
using System.Collections;

public class RegularShot : Skill
{

	private override void doDamage (GameObject colliderObject)
	{
		doDamageToSingleTarget (colliderObject, baseShotDamage, currentWeaponType);
	}
	
	private override void createEffect (GameObject colliderObject)
	{
	
	}
}