using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out BallController ball))
        {
            ball.Dead();
        }
    }
}
