using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }

    private readonly int _dissolveHeight = Shader.PropertyToID("_DissolveHeight");
    
    private BallInputController _inputController;
    private LineRenderer _lineRenderer;
    [field: SerializeField] public bool IsInvisible { get; set; } = false;

    [SerializeField] private Material _dissolveMaterial;
    [SerializeField] private GameObject _deadEffect;
    
    [SerializeField] private float _invisibleTime;
    private float _currentInvisibleTime = 0f;

    [SerializeField] private float _jetpackSpeed;
    [SerializeField] private int shootCount = 1;
    public void SetShootCount(int count) => shootCount = count;
    private bool CanShoot() => shootCount > 0;

    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _inputController = BallInputController.Instance;
    }

    private void Start()
    {
        _dissolveMaterial.SetFloat(_dissolveHeight, 1f);
        
        _inputController.OnShootEvent += HandleShootEvent;
        _inputController.OnDragEvent += HandleDragEvent;
        _inputController.OnJetpackEvent += HandleJetpackEvent;

        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        _lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.2f;
    }

    private void Update()
    {
        if (IsInvisible)
        {
            _currentInvisibleTime += Time.deltaTime;

            if (_currentInvisibleTime >= _invisibleTime)
            {
                IsInvisible = false;
                _currentInvisibleTime = 0f;
            }
        }
    }

    private void HandleJetpackEvent(Vector2 direction)
    {
        _lineRenderer.SetPosition(0,  direction);
        _lineRenderer.SetPosition(1, transform.position);
        AddForce(direction.normalized, _jetpackSpeed * Time.deltaTime);
    }

    private void HandleDragEvent(Vector2 direction, float force)
    {
        _lineRenderer.SetPosition(0,  direction);
        _lineRenderer.SetPosition(1, transform.position);
    }

    private void HandleShootEvent(Vector2 direction, float force)
    {
        if (CanShoot() is false) return;
        
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1,  Vector3.zero);

        RbCompo.velocity = Vector2.zero;
        RbCompo.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        SetShootCount(shootCount - 1);
    }
    
    public void AddForce(Vector2 direction, float force, ForceMode2D mode = ForceMode2D.Force)
    {
        RbCompo.AddForce(direction * force, mode);
    }

    private void OnDestroy()
    {
        _inputController.OnShootEvent -= HandleShootEvent;
        _inputController.OnDragEvent -= HandleDragEvent;
        _inputController.OnJetpackEvent -= HandleJetpackEvent;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (shootCount > 0) return;

            SetShootCount(1);
        }
    }

    public void Dead()
    {
        if (IsInvisible is true) return;

        StartCoroutine(nameof(DeadRoutine));
    }

    private IEnumerator DeadRoutine()
    {
        bool instantEffect = false;
        float val = 1f;

        SetShootCount(0);
        RbCompo.velocity = Vector2.zero;
        RbCompo.isKinematic = true;

        while (val > -0.5f)
        {
            val -= Time.deltaTime;
            _dissolveMaterial.SetFloat(_dissolveHeight, val);

            if (val <= 0 && instantEffect is false)
            {
                Instantiate(_deadEffect, transform.position, Quaternion.identity);
                instantEffect = true;
            }
            
            yield return null;
        }
            
        Debug.Log("Player Dead");
    }
}
