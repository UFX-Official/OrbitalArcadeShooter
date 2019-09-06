using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst_Rifle : Weapon
{
    // ########################################################
    // # ----------------- WEAPON VARIABLES ----------------- #
    // ########################################################

    const float rps = 10.0f; // 10.0/s || 600/rpm;
    const int shotsPerBurst = 3;
    bool isShooting = false;

    // #########################################################
    // # ---------------- OVERRIDABLE METHODS ---------------- #
    // #########################################################

    public override void Shoot()
    {
        if (nextShot <= 0 && !isShooting)
        {
            StartCoroutine(BurstFire());

            nextShot = (1 / rps);
        }
    }

    public override TriggerType GetTriggerType()
    {
        return TriggerType.semi;
    }

    // #########################################################
    // # ---------------- ENUMERABLE METHODS ----------------- #
    // #########################################################

    protected IEnumerator BurstFire()
    {
        Vector3 forward = parent.transform.forward;
        isShooting = true;

        for (int i = 0; i < shotsPerBurst; i++)
        {
            GameObject shot = Instantiate(Resources.Load("Prefabs/Projectiles/Standard"), parent.transform.position + forward * 3.0f, Quaternion.identity) as GameObject;
            shot.GetComponent<Projectile>().Shoot(forward);

            yield return new WaitForSeconds(0.05f);
        }

        isShooting = false;
    }

}
