using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mace : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0,0,1440 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out BallController ball))
        {
            ball.Dead();
        }
    }
}
