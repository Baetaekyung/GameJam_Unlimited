using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Wind : MonoBehaviour
{
    private BallController _ball;
    public float windForce;
    public Vector2 windDirection;
    
    private void Awake()
    {
        _ball = FindObjectOfType<BallController>();
    }

    private void Update()
    {
        _ball.AddForce(windDirection.normalized, windForce * Time.deltaTime);
    }
}
