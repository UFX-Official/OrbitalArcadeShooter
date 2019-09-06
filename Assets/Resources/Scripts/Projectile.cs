using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Achieving Orbital Velocity
    // v = Sqrt(GM/r)
    // G is the gravitational constant
    // M is the mass of the planet
    // r is the distance from the orbit to the center of mass of the planet
    
    [Header("Decay?")]
    public bool canDecay;
    public float decayTimer;
    float decayTime;

    [Header("Destroy on Contact?")]
    public bool onContact;

    public float projectileSpeed = 1000.0f;

    private void Start()
    {
        decayTime = decayTimer;
    }

    private void Update()
    {
        if (canDecay)
        {
            if (decayTime > 0)
            {
                decayTime -= Time.deltaTime;
            }

            if (decayTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction * projectileSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (onContact)
        {
            if (collider.tag != "Projectile" && collider.tag != "Swap")
            {
                Debug.Log(collider.name);
                Destroy(gameObject);
            }
        }
    }
}
