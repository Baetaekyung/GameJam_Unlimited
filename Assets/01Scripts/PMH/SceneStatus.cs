using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneStateType
{
    Setting, Store, InGame, SelectLevel
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
        Debug.Log(sst.ToString() + "스테이트 입니다");
        sceneState = sst;

        if(sceneState == SceneStateType.Setting)
        {
            //Time.timeScale = 0f;
        }
        else if(sceneState == SceneStateType.Store)
        {

        }
        else if( sceneState == SceneStateType.InGame)
        {

        }
        else if(sceneState == SceneStateType.SelectLevel)
        {

        }
    }
}
