using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerGuage : MonoBehaviour
{
    [SerializeField] private RectTransform _guageTrm;
    private BallInputController _ballInput;
    
    private void Start()
    {
        _ballInput = BallInputController.Instance;
        _ballInput.OnDragEndEvent += HandleDragEnd;
        _guageTrm.localScale = new Vector2((_ballInput.GetForce / _ballInput.GetMaxForce), 1f);
    }

    private void HandleDragEnd()
    {
        _guageTrm.localScale = new Vector2(0f, 1f);
    }

    private void Update()
    {
        _guageTrm.localScale = new Vector2((_ballInput.GetForce / _ballInput.GetMaxForce), 1f);
    }

    private void OnDestroy()
    {
        _ballInput.OnDragEndEvent -= HandleDragEnd;
    }
}
