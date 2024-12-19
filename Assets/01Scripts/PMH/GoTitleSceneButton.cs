using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoTitleSceneButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        FadeSceneChanger.Instance.FadeIn(1f, () =>
        {
            SceneManager.LoadScene("TItleScene_PMH");
            Time.timeScale = 1f;
        });
    }
}
