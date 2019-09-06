using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public float decayTimer;
    float time;

    private void Start()
    {
        time = decayTimer;
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
