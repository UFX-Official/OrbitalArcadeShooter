using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    // ########################################################
    // # ----------------- WEAPON VARIABLES ----------------- #
    // ########################################################

    const float rps = 10.0f; // 10.0/s || 600/rpm;

    // #########################################################
    // # ---------------- OVERRIDABLE METHODS ---------------- #
    // #########################################################

    public override void Shoot()
    {
        if (nextShot <= 0)
        {
            GameObject shot = Instantiate(Resources.Load("Prefabs/Projectiles/Standard"), parent.transform.position + parent.transform.forward * 3.0f, Quaternion.identity) as GameObject;
            shot.GetComponent<Projectile>().Shoot(parent.transform.forward);

            nextShot = (1 / rps);
        }
    }

    public override TriggerType GetTriggerType()
    {
        return TriggerType.automatic;
    }
}
