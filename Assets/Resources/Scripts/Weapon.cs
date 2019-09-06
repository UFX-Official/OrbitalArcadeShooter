using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType { automatic, semi }

public abstract class Weapon : MonoBehaviour
{
    // ########################################################
    // # ----------------- WEAPON VARIABLES ----------------- #
    // ########################################################

    public GameObject parent;
    
    protected WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    protected float nextShot;

    // ########################################################
    // # ------------------- METHODOLOGY -------------------- #
    // ########################################################

    private void Update()
    {
        // Update Shot Timer
        if (nextShot > 0)
        {
            nextShot -= Time.deltaTime;
        }
    }

    // #########################################################
    // # ----------------- ABSTRACT METHODS ------------------ #
    // #########################################################

    public abstract void Shoot();
    public abstract TriggerType GetTriggerType();
}
