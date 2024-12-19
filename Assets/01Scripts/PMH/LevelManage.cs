using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManage : MonoBehaviour
{
    public static LevelManage Instance;

    [SerializeField] private Transform easyLevelParent;
    [SerializeField] private List<LevelSlot> easyLevels = new List<LevelSlot>();

    [SerializeField] private Transform normalLevelParent;
    [SerializeField] private List<LevelSlot> normalLevels = new List<LevelSlot>();

    [SerializeField] private Transform hardLevelParent;
    [SerializeField] private List<LevelSlot> hardLevels = new List<LevelSlot>();

    [Header("LevelsButtons")]
    [SerializeField] private Transform easyLevel;
    [SerializeField] private Transform normalLevel;
    [SerializeField] private Transform hardLevel;

    private int easyLeveloffset;
    private int normalLeveloffset;
    private int hardLeveloffset;

    private void Awake()
    {
        GetChilds();

        EasyLevelOpen();

        SetAllLocked();
        ShinCheckLevels();

        //easyLevels[0].SetLevelType(LevelSlotType.Opend);
    }

    private void GetChilds()
    {
        for (int i = 0; i < easyLevelParent.childCount; i++)
        {
            easyLevels.Add(easyLevelParent.GetChild(i).GetComponent<LevelSlot>());
        }
        for (int i = 0; i < normalLevelParent.childCount; i++)
        {
            normalLevels.Add(normalLevelParent.GetChild(i).GetComponent<LevelSlot>());
        }
        for (int i = 0; i < hardLevelParent.childCount; i++)
        {
            hardLevels.Add(hardLevelParent.GetChild(i).GetComponent<LevelSlot>());
        }
    }
    private void ShinCheckLevels()
    {
        Debug.Log("현재 최신씬은 : " + GameManager.currentSceneNumber.currentSceneNumbers);

        if(GameManager.currentSceneNumber.currentSceneNumbers > 0)
        {
            foreach(var level in easyLevels)
            {
                int levelNum = level.transform.GetSiblingIndex() + 1;
                
                if(levelNum <= GameManager.currentSceneNumber.currentSceneNumbers)
                {
                    level.SetLevelType(LevelSlotType.Opend);
                    Debug.Log(levelNum + " 번 레벨 까지 열림");
                }
            }
        }
        if(GameManager.currentSceneNumber.currentSceneNumbers > 10)
        {
            foreach (var level in normalLevels)
            {
                int levelNum = level.transform.GetSiblingIndex() + 11;

                if (levelNum <= GameManager.currentSceneNumber.currentSceneNumbers)
                {
                    level.SetLevelType(LevelSlotType.Opend);
                    Debug.Log(levelNum + " 번 레벨 까지 열림");
                }
            }
        }
        if (GameManager.currentSceneNumber.currentSceneNumbers > 40)
        {
            foreach (var level in hardLevels)
            {
                int levelNum = level.transform.GetSiblingIndex() + 41;

                if (levelNum <= GameManager.currentSceneNumber.currentSceneNumbers)
                {
                    level.SetLevelType(LevelSlotType.Opend);
                    Debug.Log(levelNum + " 번 레벨 까지 열림");
                }
            }
        }
    }

    private void SetAllLocked()
    {
        foreach (var level in easyLevels)
        {
            level.SetLevelType(LevelSlotType.Closed);
        }
        foreach (var level in normalLevels)
        {
            level.SetLevelType(LevelSlotType.Closed);
        }
        foreach (var level in hardLevels)
        {
            level.SetLevelType(LevelSlotType.Closed);
        }
    }

    public void SetClearLevleSlot()
    {
        //for (int i = 0; i < currentLevel; i++)
        //{
        //    levels[i].SetLevelType(LevelSlotType.Cleard);
        //}
        //
        //levels[currentLevel].SetLevelType(LevelSlotType.Opend);
    }

    //���� ���� ��ư �ؼ� �¾�Ƽ���ϱ�
    public void EasyLevelOpen()
    {
        easyLevel.gameObject.SetActive(true);
        normalLevel.gameObject.SetActive(false);
        hardLevel.gameObject.SetActive(false);
    }
    public void NormalLevelOpen()
    {
        easyLevel.gameObject.SetActive(false);
        normalLevel.gameObject.SetActive(true);
        hardLevel.gameObject.SetActive(false);
    }
    public void HardLevelOpen()
    {
        easyLevel.gameObject.SetActive(false);
        normalLevel.gameObject.SetActive(false);
        hardLevel.gameObject.SetActive(true);
    }
}
