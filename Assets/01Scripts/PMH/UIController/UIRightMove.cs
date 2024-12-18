using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRightMove : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TitleCanvasManager.Instance.SlideScene(1);
        }
    }
}
