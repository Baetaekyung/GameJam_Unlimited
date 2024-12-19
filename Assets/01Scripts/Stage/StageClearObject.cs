using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageClearObject : MonoBehaviour
{
    [SerializeField] private string _nextStageName;
    private bool _isStageCleared = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStageCleared) return;

        if(collision.CompareTag("Player"))
        {
            Debug.Log("StageClear");
            _isStageCleared = true;

            FadeSceneChanger.Instance.FadeIn(1f, () =>
            {      
                SceneManager.LoadScene(_nextStageName);
            });

        }
    }
}
