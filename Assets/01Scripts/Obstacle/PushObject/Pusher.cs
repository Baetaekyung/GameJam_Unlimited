using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out BallController ball))
        {
            ball.AddForce(transform.up, 30f, ForceMode2D.Impulse);
            _animator.SetTrigger("Push");
        }
    }
}
