using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetGravityAttractor : Bolt.EntityBehaviour<ICustomeCubeState>
{
    public float gravity = -10;

    public Vector3 Attract(Transform body, float multiplier)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity * multiplier);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 5 * Time.deltaTime);

        return gravityUp;
    }

    public Vector3 GetGravityUp(Transform body)
    {
        return (body.position - transform.position).normalized;
    }
}
