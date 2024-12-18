using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoSingleton<PopUpManager>
{
    [SerializeField] private CanvasGroup _settingCanvas;

    private void Start()
    {
        FadeOutSettingPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_settingCanvas.gameObject.activeSelf)
            {
                FadeOutSettingPanel();
            }
            else
            {
                FadeInSettingPanel();
            }
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
}
