using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ClearData
{
    public bool[] clearDatas;
}

[MonoSingleton(SingletonFlag.DontDestroyOnLoad)]
public class GameManager : MonoSingleton<GameManager>
{
    private static ClearData clearData;
    
    protected override void Awake()
    {
        base.Awake();

        if (SaveManager.Exist("clearData.json")) //이미 접속한 데이터가 있을때
        {
            clearData = SaveManager.Load<ClearData>("clearData.json");
            Debug.Log(clearData.clearDatas);
        }
        else //게임에 처음 접속했을때
        {
            clearData = new ClearData
            {
                clearDatas = new bool[410]
            };

            clearData.clearDatas[1] = true;
            
            for (var i = 2; i < clearData.clearDatas.Length; i++)
            {
                clearData.clearDatas[i] = false;
            }
            
            SaveManager.Save(clearData, "clearData.json");
        }
    }

    public bool IsClearStage(int stageNum)
    {
        Debug.Log(clearData.clearDatas.Length + " 길이를 가지고 잇습니다");
        return clearData.clearDatas[stageNum];
    }

    public void SetStageClear(int stageNum)
    {
        clearData.clearDatas[stageNum] = true;
        SaveManager.Save(clearData, "clearData.json");
    }
}
