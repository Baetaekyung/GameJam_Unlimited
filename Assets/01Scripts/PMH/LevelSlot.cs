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
public class LevelSlot : MonoBehaviour
{
    [SerializeField] private LevelSlotType levelType;

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
                    levelAmountTxt.color = Color.green;
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
        int cnum = transform.GetSiblingIndex();
        cnum = cnum + 1;
        Debug.Log($"{levelDiff}_{cnum}");

        FadeSceneChanger.Instance.FadeIn(1f, () =>
        {
            GameManager.Instance.SetCurrentSceneNumber(cnum);
            SceneManager.LoadScene($"{levelDiff}_{cnum}");
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