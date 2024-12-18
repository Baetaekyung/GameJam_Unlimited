using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private void Start()
    {
        Destroy(gameObject, 12f);
    }

    private void Update()
    {
        transform.Translate(transform.right * (Time.deltaTime * _speed));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out BallController ball))
        {
            ball.Dead();
            Destroy(gameObject);
        }
    }
}
