using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    // ########################################################
    // # ----------------- WEAPON VARIABLES ----------------- #
    // ########################################################

    const float rps = 0.5f; // 0.5/s || 30/rpm;

    // #########################################################
    // # ---------------- OVERRIDABLE METHODS ---------------- #
    // #########################################################

    public override void Shoot()
    {
        if (nextShot <= 0)
        {
            GameObject shot = BoltNetwork.Instantiate(BoltPrefabs.NetProjectile_Laser, parent.transform.position + parent.transform.forward * 3.0f, Quaternion.identity);
            shot.GetComponent<NetProjectile>().Shoot(parent.transform.forward);

            //GameObject shot = Instantiate(Resources.Load("Prefabs/Projectiles/Laser"), parent.transform.position + parent.transform.forward * 3.0f, Quaternion.identity) as GameObject;
            //shot.GetComponent<Projectile>().Shoot(parent.transform.forward);

            nextShot = (1 / rps);
        }
    }

    public override TriggerType GetTriggerType()
    {
        return TriggerType.semi;
    }
}
