using UnityEngine;
using System.Collections;

public class FrostNovaShot : SkillEffect
{
	private float novaDistance = 5F;
	private float slowingSpeed = 0.3F;
	private float slowingDuration = 5F;

	public override void doDamage (GameObject colliderObject)
	{
		ArrayList monstersWithinNova = MonsterFinder.sharedHelper ().monstersWithinArea (transform.position, novaDistance);
		foreach (GameObject monster in monstersWithinNova) {
			SlowingEffect existingEffect = monster.GetComponent<SlowingEffect> ();
			
			//if they are slowed already, and existing effect is stronger, skip
			if (existingEffect != null && existingEffect.slowingPercentage > slowingSpeed) {
				continue;
			}
			
			if (existingEffect != null) {
				existingEffect.slowingDuration = slowingDuration;
				existingEffect.updateTimer ();
			} else {
			
				//if not just add on. 
				SlowingEffect newEffect = monster.AddComponent<SlowingEffect> ();
				newEffect.slowingDuration = slowingDuration;
				newEffect.slowingPercentage = slowingSpeed;
			}
		}
	}

	public override void createEffect (GameObject colliderObject)
	{
		EffectFactory.sharedFactory ().deliverSphericNova (transform.position);
	}
}