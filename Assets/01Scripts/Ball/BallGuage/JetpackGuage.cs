using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackGuage : MonoBehaviour
{
    [SerializeField] private RectTransform _guageTrm;
    private BallInputController _ballInput;
    
    private void Start()
    {
        _ballInput = BallInputController.Instance;

        if (_ballInput.jetpackDuration != 0)
        {
            _guageTrm.localScale = new Vector2((_ballInput.jetpackRemainTime / _ballInput.jetpackDuration), 1f);
        }
        else 
            _guageTrm.localScale = new Vector2(0f, 1f);
    }

    private void Update()
    {
        _guageTrm.localScale = new Vector2((_ballInput.jetpackRemainTime / _ballInput.jetpackDuration), 1f);

    }
}
