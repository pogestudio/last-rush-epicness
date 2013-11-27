using UnityEngine;
using System.Collections;

public class Gun : AbstractWeapon {

    public override void triggerDown()
    {
        fire();
    }

    public override void triggerHold()
    {
    }

    public override void triggerUp()
    {
    }
}
