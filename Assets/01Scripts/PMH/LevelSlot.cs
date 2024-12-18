using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    private void SetImageAndText()
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
        string str = "map" + (transform.GetSiblingIndex() + 1).ToString();
        Debug.Log(str);
        //Transform map = transform.Find(str);
        //map.gameObject.SetActive(true);
        Debug.Log("Å¬¸¯¹Ö");

        GameCanvaManager.Instance.FadeInFadeOut(true);
        Debug.Log(levelData.levelName);
        Debug.Log(levelData.levelDifficulty.ToString());
        Instantiate(levelData.Level, transform.position, Quaternion.identity);
        TitleCanvasManager.Instance.SetMainView();
        GameCanvaManager.Instance.FadeInFadeOut(false);
    }
}