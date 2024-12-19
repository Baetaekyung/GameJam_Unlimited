using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallInputController : MonoSingleton<BallInputController>
{
    public Action<Vector2, float> OnDragEvent;
    public Action<Vector2, float> OnShootEvent;
    public Action<Vector2> OnJetpackEvent;
    public Action OnDragEndEvent;
    
    private bool _isDragging = false;
    public bool IsDragging => _isDragging;
    public bool isJetpack = false;

    public float jetpackDuration = 2f;

    private Vector2 _dragStartPosition;
    private Vector2 _dragPosition;
    private Vector2 _direction;
    
    public Vector2 GetDirection => _direction;
    
    [SerializeField] private float _maxShootForce;
    public float GetMaxForce => _maxShootForce;
    [SerializeField] private float _shootForceDivider;
    private float _shootForce = 0;
    public float GetForce => _shootForce;
    
    private void Update()
    {
        if (isJetpack)
        {
            jetpackDuration -= Time.deltaTime;
            if (jetpackDuration <= 0)
            {
                isJetpack = false;
                jetpackDuration = 4f; //4초만 하자
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            DragStart();
        }
        else if (Input.GetMouseButton(0))
        {
            Drag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DragEnd();
        }
    }
    
    private void DragStart()
    {
        _isDragging = true;
        
        _dragStartPosition = Input.mousePosition;
    }

    private void Drag()
    {
        _dragPosition = Input.mousePosition;
        
        _direction = _dragStartPosition - _dragPosition;
        
        _shootForce = Mathf.Clamp(_direction.magnitude / _shootForceDivider, 0, _maxShootForce);
        
        if(isJetpack is false)
            OnDragEvent?.Invoke(_direction, _shootForce);
        else if (isJetpack)
            OnJetpackEvent?.Invoke(_direction);
    }

    private void DragEnd()
    {
        if (isJetpack)
        {
            _shootForce = 0f;
            _direction = Vector2.zero;
            _isDragging = false;
            isJetpack = false;
            jetpackDuration = 4f;
            OnDragEndEvent?.Invoke();
            
            return;
        }
        
        OnShootEvent?.Invoke(_direction, _shootForce);
        
        _shootForce = 0f;
        _direction = Vector2.zero;
        _isDragging = false;
        OnDragEndEvent?.Invoke();
    }
    
#if UNITY_EDITOR

    // private void OnDrawGizmos()
    // {
    //     if (_isDragging is false) return;
    //     
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawLine(transform.position, _direction * _shootForce);
    //     Gizmos.color = Color.white;
    // }

#endif
}
