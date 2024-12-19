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

    [SerializeField] private LevelSlotDataSO levelData;

    [SerializeField]
    private Sprite
        opendImage,
        cleardImage,
        closedImage;

    private Image ig;

    private void OnEnable()
    {
        ig = GetComponent<Image>();

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
                    ig.sprite = opendImage;
                    levelAmountTxt.color = Color.black;
                }
                break;
            case LevelSlotType.Cleard:
                {
                    ig.sprite = cleardImage;
                    levelAmountTxt.color = Color.black;
                }
                break;
            case LevelSlotType.Closed:
                {
                    ig.sprite = closedImage;
                    levelAmountTxt.color = Color.white;
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
        Debug.Log($"{levelData.levelDifficulty}_{transform.GetSiblingIndex()}");
        SceneManager.LoadScene($"{levelData.levelDifficulty}_{transform.GetSiblingIndex()}");

        //입장밍

        //GameCanvaManager.Instance.FadeInFadeOut(true);
        //Debug.Log(levelData.levelName);
        //Debug.Log(levelData.levelDifficulty.ToString());
        //Instantiate(levelData.Level, transform.position, Quaternion.identity);
        //TitleCanvasManager.Instance.SetMainView();
        //GameCanvaManager.Instance.FadeInFadeOut(false);
    }
}