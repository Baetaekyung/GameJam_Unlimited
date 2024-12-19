using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpManager : MonoSingleton<PopUpManager>
{
    [SerializeField] private CanvasGroup _settingCanvas;
    [SerializeField] private bool _firstClose = true;

    private bool isOpenSettingPanel = false;
    private bool isReloading = false;

    private void Start()
    {
        if (_firstClose is false) return;

        FadeOutSettingPanel();
    }

    private void Update()
    {
        if (_firstClose is false) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_settingCanvas.gameObject.activeSelf)
            {
                //������
                Time.timeScale = 1f;
                SoundManager.Instance.SetVolumes();
                isOpenSettingPanel = false;
                FadeOutSettingPanel();
            }
            else
            {
                //���ö�
                Time.timeScale = 0f;
                isOpenSettingPanel = true;
                FadeInSettingPanel();
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && isOpenSettingPanel && isReloading == false)
        {
            Time.timeScale = 1f;
            SoundManager.Instance.SetVolumes();
            isOpenSettingPanel = false;
            FadeOutSettingPanel();

            Debug.Log("�����");
            RestartLevel();
        }
    }

    public void FadeInSettingPanel()
    {
        _settingCanvas.alpha = 1;
        _settingCanvas.gameObject.SetActive(true);
    }

    public void FadeOutSettingPanel()
    {
        _settingCanvas.gameObject.SetActive(false);
        _settingCanvas.alpha = 0;
    }

    public void RestartLevel()
    {
        isReloading = true;
        FadeSceneChanger.Instance.FadeIn(1f, () =>
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);

            isReloading = false;
        });
    }

    public void GoTitleBTN()
    {
        FadeSceneChanger.Instance.FadeIn(1f, () =>
        {
            SceneManager.LoadScene("TItleScene_PMH");
        }); 
    }
}
