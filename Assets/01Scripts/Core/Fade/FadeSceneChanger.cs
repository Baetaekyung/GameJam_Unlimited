using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeSceneChanger : MonoSingleton<FadeSceneChanger>
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeSpeed;

    private void Start()
    {
        FadeOut(1f);
    }

    public void FadeIn(float duration, Action callback = null)
    {
        _fadeImage.color = new Color(0, 0, 0, 0);
        StartCoroutine(CallbackRoutineFadeIn(duration, callback));
    }

    public void FadeOut(float duration, Action callback = null)
    {
        _fadeImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(CallbackRoutineFadeOut(duration, callback));
    }

    private IEnumerator CallbackRoutineFadeIn(float duration, Action callback)
    {
        var progress = 0f;
        
        while (progress < duration)
        {
            progress += _fadeSpeed * Time.unscaledDeltaTime;
            
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, progress);

            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        
        callback?.Invoke();
    }
    
    private IEnumerator CallbackRoutineFadeOut(float duration, Action callback)
    {
        var progress = 1f;
        
        while (progress > 0)
        {
            progress -= _fadeSpeed * Time.unscaledDeltaTime;
            
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, progress);
            
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        
        callback?.Invoke();
    }
}
