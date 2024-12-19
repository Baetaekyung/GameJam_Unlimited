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

            SoundManager.Instance.PlayerSFX(SfxType.GameClear);

            Scene currentScene = SceneManager.GetActiveScene();
            string[] str = currentScene.name.Split('_');

            int offset = 1;
            switch(str[0])
            {
                case "Easy":
                    offset += 0;
                    break;
                case "Normal":
                    offset += 10;
                    break;
                case "Hard":
                    offset += 40;
                    break;
            }

            offset += int.Parse(str[1]);

            Debug.Log(offset + " 네 맞아요 ");

            GameManager.currentSceneNumber = offset;

            GameManager.Instance.SetStageClear(offset);

            _isStageCleared = true;

            FadeSceneChanger.Instance.FadeIn(1f, () =>
            {      
                SceneManager.LoadScene(_nextStageName);
            });

        }
    }
}
