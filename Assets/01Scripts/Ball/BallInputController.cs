using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallInputController : MonoBehaviour
{
    public Action<Vector2> OnDragEndEvent;
    public Action<Vector2, float> OnBallShootEvent;
    
    private bool _isDragging = false;
    public bool IsDragging => _isDragging;

    private Vector2 _dragStartPosition;
    private Vector2 _dragPosition;
    private Vector2 _direction;
    
    public Vector2 GetDirection => _direction;

    [SerializeField] private float _maxShootForce;
    [SerializeField] private float _shootForceDivider;
    private float _shootForce;
    
    private void Update()
    {
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
        _direction = Vector2.zero;
        _shootForce = 0f;
        _dragStartPosition = Input.mousePosition;
        
        Debug.Log("Start Drag");
    }

    private void Drag()
    {
        _dragPosition = Input.mousePosition;
        
        _direction = _dragStartPosition - _dragPosition;

        _shootForce = _direction.magnitude / _shootForceDivider; Debug.Log(_shootForce);
        _shootForce = Mathf.Clamp(_shootForce, 0, _maxShootForce);
        
        Debug.Log("Dragging");
    }

    private void DragEnd()
    {
        _direction.Normalize();
        
        if (_direction.y > 0)
        {
            OnBallShootEvent?.Invoke(_direction, _shootForce);
            OnDragEndEvent?.Invoke(_direction);
        }
        
        _isDragging = false;
        
        Debug.Log("End Drag");
    }
    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (_isDragging is false) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _direction * _shootForce);
        Gizmos.color = Color.white;
    }

#endif
}
