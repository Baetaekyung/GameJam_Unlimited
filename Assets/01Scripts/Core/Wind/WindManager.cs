using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WindManager : MonoBehaviour
{
    private BallController _ball;
    public float windForce;
    public Vector2 windDirection;
    public float windCoolTime = 0.3f;
    private float _lastWindTime = 0f;
    
    private void Awake()
    {
        _ball = FindObjectOfType<BallController>();
    }

    private void Update()
    {
        if (_lastWindTime >= windCoolTime)
        {
            _ball.AddForce(windDirection.normalized, windForce);
            _lastWindTime = 0f;
        }
        else
        {
            _lastWindTime += Time.deltaTime;
        }
    }
}
