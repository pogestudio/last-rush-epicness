using UnityEngine;
using System.Collections;

public class MachineGun : AbstractWeapon
{

    public float timeBetweenShots = 0.2f;
    private float delay = 0;

    public void Update()
    {
        base.Update();
        delay -= Time.deltaTime;
    }

    public override void triggerDown()
    {
    }

    public override void triggerHold()
    {
        if (delay <= 0)
        {
            delay = timeBetweenShots;
            fire();
        }
    }

    public override void triggerUp()
    {
    }
}
