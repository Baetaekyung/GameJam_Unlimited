using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private BallInputController _inputController;
    private Rigidbody2D _rbCompo;

    private bool _canShoot;

    private void Awake()
    {
        _inputController = GetComponent<BallInputController>();
        _rbCompo = GetComponent<Rigidbody2D>();
        _canShoot = true;
    }

    private void Start()
    {
        _inputController.OnBallShootEvent += HandleShootEvent;
    }

    private void HandleShootEvent(Vector2 direction, float force)
    {
        if (_canShoot is false) return;
        
        _rbCompo.AddForce(direction * force, ForceMode2D.Impulse);
        _canShoot = false;
    }

    public void AddForce(Vector2 direction, float force)
    {
        _rbCompo.AddForce(direction, ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        _inputController.OnBallShootEvent -= HandleShootEvent;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _canShoot = true;
        }
    }
}
