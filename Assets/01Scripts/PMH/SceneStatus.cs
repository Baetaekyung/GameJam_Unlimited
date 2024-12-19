using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneStateType
{
    Title, Challenge, Timeattck, Setting, Tip1, Tip2
}
public class SceneStatus : MonoBehaviour
{
    public static SceneStatus Instance;

    private SceneStateType sceneState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void ChangeSceneState(SceneStateType sst)
    {
        //Time.timeScale = 1f;
        //Debug.Log(sst.ToString() + "스테이트 입니다");
        sceneState = sst;

        switch (sst)
        {
            case SceneStateType.Title:
                break;
            case SceneStateType.Challenge:
                break;
            case SceneStateType.Timeattck:
                break;
            case SceneStateType.Setting:
                break;
            case SceneStateType.Tip1:
                break;
            case SceneStateType.Tip2:
                break;
        }
    }
}
