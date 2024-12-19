using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LevelSlotType
{
    Opend, Cleard, Closed
}
public enum LevelDifficulty
{
    Easy, Normal, Hard
}
public class LevelSlot : MonoBehaviour
{
    [SerializeField] private LevelSlotType levelType;
    [SerializeField] private LevelDifficulty levelDifficulty;

    [SerializeField] TMP_Text levelAmountTxt;

    [SerializeField] private string levelDiff;

    private void OnEnable()
    {
        int cc = transform.GetSiblingIndex();
        levelAmountTxt.text = (cc + 1).ToString();
        SetImageAndText();
    }

    public void SetImageAndText()
    {
        switch (levelType)
        {
            case LevelSlotType.Opend:
                {
                    levelAmountTxt.color = Color.white;
                }
                break;
            case LevelSlotType.Cleard:
                {
                    levelAmountTxt.color = Color.white;
                }
                break;
            case LevelSlotType.Closed:
                {
                    levelAmountTxt.color = Color.red;
                }
                break;
        }
    }
    
    public void SetLevelType(LevelSlotType levelType)
    {
        this.levelType = levelType;
        SetImageAndText();
    }
    public void LetsPlay()
    {
        if (levelType == LevelSlotType.Closed) return; //금지 소리라든지 이펙트 추가? ㅎ 
        //string str = "map" + (transform.GetSiblingIndex() + 1).ToString();
        //Debug.Log(str);
        ////Transform map = transform.Find(str);
        ////map.gameObject.SetActive(true);
        Debug.Log("클릭밍");
        int sceneNum = transform.GetSiblingIndex();
        int loadScene = transform.GetSiblingIndex();

        switch (levelDifficulty)
        {
            case LevelDifficulty.Easy:
                sceneNum = loadScene + 1;
                break;
            case LevelDifficulty.Normal:
                sceneNum = loadScene + 11;
                break;
            case LevelDifficulty.Hard:
                sceneNum = loadScene + 41;
                break;
        }


        Debug.Log($"다음에 로딩 할 씬은 : {levelDiff}_{loadScene + 1} 번째 씬입니다.");

        FadeSceneChanger.Instance.FadeIn(1f, () =>
        {
            SceneManager.LoadScene($"{levelDiff}_{loadScene + 1}");
        });
        //입장밍

        //GameCanvaManager.Instance.FadeInFadeOut(true);
        //Debug.Log(levelData.levelName);
        //Debug.Log(levelData.levelDifficulty.ToString());
        //Instantiate(levelData.Level, transform.position, Quaternion.identity);
        //TitleCanvasManager.Instance.SetMainView();
        //GameCanvaManager.Instance.FadeInFadeOut(false);
    }
}