using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAttractor attractor;
    
    public Vector3 gravityUp = Vector3.zero;
    public float gravityMultiplier = 1.0f;

    private void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;


        attractor = GameObject.Find("Planet").GetComponent<FauxGravityAttractor>();
    }

    private void Update()
    {
        gravityUp = attractor.Attract(transform, gravityMultiplier);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GetComponent<PlayerController>() != null)
            {
                Vector3 gravityUp = (transform.position - attractor.transform.position).normalized;
                Vector3 bodyUp = transform.up;

                GetComponent<Rigidbody>().AddForce(gravityUp * 500.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Swap")
        {
            attractor = other.GetComponentInParent<FauxGravityAttractor>();
            Debug.Log("Swapped");
        }
    }


}
