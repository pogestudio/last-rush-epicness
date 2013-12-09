using UnityEngine;
using System.Collections;

public class RegularShot : SkillEffect
{

    public bool wasTriggeredByMultishot;

    public override void doDamage(GameObject colliderObject)
    {
        doDamageToSingleTarget(colliderObject, baseShotDamage, currentWeaponType);
    }

    public override void createEffect(GameObject colliderObject)
    {
        Vector3 direction = colliderObject.transform.position - transform.position;
        EffectFactory.sharedFactory().deliverBloodSpurtEffect(colliderObject.transform.position, direction);
    }
}