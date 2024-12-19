using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ClearData
{
    public bool[] clearDatas;
}

public class CurrentScene
{
    public int currentSceneNumbers;
}

[MonoSingleton(SingletonFlag.DontDestroyOnLoad)]
public class GameManager : MonoSingleton<GameManager>
{
    private static ClearData clearData;

    public static CurrentScene currentSceneNumber;
    
    protected override void Awake()
    {
        base.Awake();

        if (SaveManager.Exist("currentScene.json")) //이미 접속한 데이터가 있을때 SaveManager.Exist("clearData.json")
        {
            //clearData = SaveManager.Load<ClearData>("clearData.json");
            //
            currentSceneNumber = SaveManager.Load<CurrentScene>("currentScene.json");
        }
        else //게임에 처음 접속했을때
        {
            //clearData = new ClearData
            //{
            //    clearDatas = new bool[46] //0번째 스테이지는 없다 1번째 스테이지부터 있다.
            //};

            currentSceneNumber = new CurrentScene
            {
                currentSceneNumbers = 1 //1번째 스테이지부터.
            };
            SaveManager.Save(currentSceneNumber, "currentScene.json");
            //clearData.clearDatas[1] = true;
            //
            //for (var i = 2; i < clearData.clearDatas.Length; i++)
            //{
            //    clearData.clearDatas[i] = false;
            //}

            //SaveManager.Save(clearData, "clearData.json");
        }
    }

    public void SetCurrentSceneNumber(int num)
    {
        Debug.Log(num + " 번째 스테이지가 최신으로 저장");
        currentSceneNumber.currentSceneNumbers = num;
        SaveManager.Save(currentSceneNumber, "currentScene.json");
    }

    public bool IsClearStage(int stageNum)
    {
        Debug.Log(stageNum + " 번째 스테이지는 : " + clearData.clearDatas[stageNum] + " 상태입니다.");
        return clearData.clearDatas[stageNum];
    }

    public void SetStageClear(int stageNum)
    {
        clearData.clearDatas[stageNum] = true;
        SaveManager.Save(clearData, "clearData.json");
    }
}
