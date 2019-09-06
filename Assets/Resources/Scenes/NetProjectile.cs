using UnityEngine;

public class NetProjectile : Bolt.EntityBehaviour<ICustomProjectileState>
{

    [Header("Projectile Decay")]
    public bool enableDecay = false;
    public float decayTimer = 0.0f;
    float decayTime = 0;

    [Header("Projectile Variables")]
    public float projectileSpeed = 20.0f;
    
    public override void Attached()
    {
        state.SetTransforms(state.ProjectileTransform, GetComponent<Rigidbody>().transform);

        if (enableDecay)
        {
            decayTime = decayTimer;
        }
    }

    public override void SimulateOwner()
    {
        if (enableDecay)
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
        if (collider != gameObject)
            Debug.Log(collider.name);
    }
}
