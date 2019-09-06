using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    // ########################################################
    // # ----------------- WEAPON VARIABLES ----------------- #
    // ########################################################

    const float rps = 3.0f; // 3.0/s || 60/rpm;
    int numBullets = 8;

    float shotDeviationX = 0.2f;
    float shotDeviationY = 0.2f;
    float shotDeviationZ = 0.2f;

    // #########################################################
    // # ---------------- OVERRIDABLE METHODS ---------------- #
    // #########################################################

    public override void Shoot()
    {
        if (nextShot <= 0)
        {
            for (int i = 0; i < numBullets; i++)
            {
                float randX = Random.Range(-shotDeviationX, shotDeviationX);
                float randY = Random.Range(-shotDeviationY, shotDeviationY);
                float randZ = Random.Range(-shotDeviationZ, shotDeviationZ);
                Vector3 forward = parent.transform.forward + new Vector3(randX, randY, randZ);

                GameObject shot = Instantiate(Resources.Load("Prefabs/Projectiles/Shotgun"), parent.transform.position + parent.transform.forward * 3.0f, Quaternion.identity) as GameObject;
                shot.GetComponent<Projectile>().Shoot(forward);

            }
            
            nextShot = (1 / rps);            
        }        
    }

    public override TriggerType GetTriggerType()
    {
        return TriggerType.semi;
    }
}
