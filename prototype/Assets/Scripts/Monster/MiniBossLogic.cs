using UnityEngine;
using System.Collections;


public class MiniBossLogic : AbstractEnemy
{

    private bool isCasting = false;
    private float nextSpecialAttack = 3; // don't cast at once. 
    public float timeBetweenSpecialAttacks;
    private float timetoStopCasting;
    public float castingTime;

    void Update()
    {

        if (networkView.isMine)
        {
            if (isCasting && Time.time > timetoStopCasting)
            {
                //should stop casting
                isCasting = false;
                nextSpecialAttack = Time.time + timeBetweenSpecialAttacks;
                performSpecialAttack();
                //Debug.Log ("Shoud stop casting. time to cast: " + nextSpecialAttack);
            }


            if (Time.time > nextSpecialAttack && !isCasting && target)
            {
                //should start casting
                timetoStopCasting = Time.time + castingTime;
                isCasting = true;
                EffectFactory.sharedFactory().createMiniBossChargingEffect(gameObject, castingTime);
            }

            if (target && !isCasting)
            {
                walk();
            }
            else if (!target)
            {
                target = PlayerFinder.sharedHelper().getClosestPlayer(transform.position, searchRadius);
            }
        }

    }

    void performSpecialAttack()
    {

        GameObject newProjectile = MonsterAttackFactory.sharedFactory().miniBossProjectile(gameObject.transform);
        newProjectile.transform.position = newProjectile.transform.position + new Vector3(0, 0, 2);


        GameObject anotherProj = MonsterAttackFactory.sharedFactory().miniBossProjectile(gameObject.transform);
        anotherProj.transform.position = anotherProj.transform.position + new Vector3(2, 0, 0);
    }

}
