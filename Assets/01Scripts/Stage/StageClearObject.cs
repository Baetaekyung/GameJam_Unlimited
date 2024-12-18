using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearObject : MonoBehaviour
{
    private bool _isStageCleared = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStageCleared) return;

        if(collision.CompareTag("Player"))
        {
            Debug.Log("StageClear");

            //�������� Ŭ���� UI ��Ÿ����

            _isStageCleared = true;
        }
    }
}
