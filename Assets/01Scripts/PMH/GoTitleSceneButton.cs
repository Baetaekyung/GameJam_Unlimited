using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoTitleSceneButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1f;
        FadeSceneChanger.Instance.FadeIn(0f, () =>
        {
            SceneManager.LoadScene("TItleScene_PMH");
            
        });
    }
}
