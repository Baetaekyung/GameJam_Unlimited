using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPlatform : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D _defaultMat;
    [SerializeField] private PhysicsMaterial2D _slidingMat;

    [SerializeField] private BallController _player;

    private Rigidbody2D _rbCompo;
    private CircleCollider2D _collider;

    private void Awake()
    {
        _rbCompo = _player.GetComponent<Rigidbody2D>();
        _collider = _player.GetComponent<CircleCollider2D>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _rbCompo.sharedMaterial = _slidingMat;
            _collider.sharedMaterial = _slidingMat;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _rbCompo.sharedMaterial = _defaultMat;
            _collider.sharedMaterial = _defaultMat;
        }
            
    }
}
