using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }

    private readonly int _dissolveHeight = Shader.PropertyToID("_DissolveHeight");
    
    private BallInputController _inputController;
    private LineRenderer _lineRenderer;
    [SerializeField] private int _lineResolution = 20;
    [SerializeField] private ObjectPoolManagerSO _poolManagerSO;
    
    [field: SerializeField] public bool IsInvisible { get; set; } = false;

    [SerializeField] private Material _dissolveMaterial;
    [SerializeField] private GameObject _deadEffect;
    [SerializeField] private GameObject _detectEffect;
    
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
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0,  direction);
        _lineRenderer.SetPosition(1, transform.position);
        AddForce(direction.normalized, _jetpackSpeed * Time.deltaTime);
    }

    private void HandleDragEvent(Vector2 direction, float force)
    {
        Vector3 initialVelocity = (direction.normalized * force) / RbCompo.mass;
        //Debug.Log(force);
        
        _lineRenderer.positionCount = _lineResolution;
        Vector3[] points = new Vector3[_lineResolution];
        
        float timeStep = GetTrajectoryDuration(initialVelocity) / _lineResolution;

        for (int i = 0; i < _lineResolution; i++)
        {
            float t = i * timeStep;
            points[i] = CalculatePositionAtTimeWithDrag(initialVelocity, t, RbCompo.drag);
        }
        
        _lineRenderer.SetPositions(points);
    }

    private Vector3 CalculatePositionAtTimeWithDrag(Vector3 initialVelocity, float time, float drag)
    {
        Vector3 startPosition = transform.position;
        float gravity = Mathf.Abs(Physics.gravity.y) * (RbCompo.gravityScale * drag); //??dragë¥?ê³±í•´???‘ë™?˜ëŠ” ì§€??ëª¨ë¥´ê² ìŒ, ?˜ì?ë§??‘ë™??

        float x = (initialVelocity.x / drag) * (1 - Mathf.Exp(-drag * time));
        float y = (initialVelocity.y / drag) * (1 - Mathf.Exp(-drag * time)) -
                  (gravity / (drag * drag)) * (time - (1 - Mathf.Exp(-drag * time)) / drag);
        float z = (initialVelocity.z / drag) * (1 - Mathf.Exp(-drag * time));
        
        return startPosition + new Vector3(x, y, z);
    }

    private float GetTrajectoryDuration(Vector3 initialVelocity)
    {
        float vy = initialVelocity.y;
        float gravity = Mathf.Abs(Physics.gravity.y) * (RbCompo.gravityScale * RbCompo.drag); //??dragë¥?ê³±í•´???‘ë™?˜ëŠ” ì§€??ëª¨ë¥´ê² ìŒ, ?˜ì?ë§??‘ë™??

        return (2 * vy) / gravity;
    }

    private void HandleShootEvent(Vector2 direction, float force)
    {
        if (CanShoot() is false) return;

        _lineRenderer.positionCount = 0;

        SoundManager.Instance.PlayerSFX(SfxType.BALLJUMP);
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
            SoundManager.Instance.PlayerSFX(SfxType.BALLDETECT);
            _poolManagerSO.Spawn("DetectEffect", transform.position, Quaternion.identity);
            
            if (shootCount > 0) return;

            SetShootCount(1);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SetShootCount(1);
        }
    }

    public void Dead()
    {
        if (IsInvisible is true) return;

        Debug.Log("Á×À½½ºÅ×ÀÌÆ®");
        //transform.position = transform.parent.Find("SpawnPoint").transform.position;

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

                //Á×À½ Ãß°¡ (»ç¿îµå¶û ¾À Àç½ÃÀÛ)
                FadeSceneChanger.Instance.FadeIn(1f, () =>
                {
                    //½Î¿îµå
                    Scene cs = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(cs.name);
                });
            }
            
            yield return null;
        }
            
        Debug.Log("Player Dead");
    }
}
