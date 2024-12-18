using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvaManager : MonoBehaviour
{
    public static GameCanvaManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void FadeInFadeOut(bool isFade)
    {
        if(isFade)
        {
            Debug.Log("화면가리기");
        }
        else
        {
            Debug.Log("화면보이기");
        }
    }
}
