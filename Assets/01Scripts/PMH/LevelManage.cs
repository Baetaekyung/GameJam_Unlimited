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

    private int currentLevel = 0;

    [Header("LevelsButtons")]
    [SerializeField] private Transform easyLevel;
    [SerializeField] private Transform normalLevel;
    [SerializeField] private Transform hardLevel;

    private readonly int easyLeveloffset = 1;
    private readonly int normalLeveloffset = 100;
    private readonly int hardLeveloffset = 400;

    private void Awake()
    {
        GetChilds();

        CheckLevelsClreard();
        SetClearLevleSlot();

        EasyLevelOpen();
    }

    private void GetChilds()
    {
        for (int i = 0; i < easyLevelParent.childCount; i++)
        {
            easyLevels.Add(normalLevelParent.GetChild(i).GetComponent<LevelSlot>());
        }
        for (int i = 0; i < normalLevelParent.childCount; i++)
        {
            normalLevels.Add(normalLevelParent.GetChild(i).GetComponent<LevelSlot>());
        }
        for (int i = 0; i < hardLevelParent.childCount; i++)
        {
            hardLevels.Add(normalLevelParent.GetChild(i).GetComponent<LevelSlot>());
        }
    }
    public void CheckLevelsClreard()
    {
        for (int i = 0; i < easyLevels.Count; i++)
        {
            Debug.Log($"{i + easyLeveloffset} 번째 이지 맵");
            if (GameManager.Instance.IsClearStage(i + easyLeveloffset))
            {
                easyLevels[i].SetLevelType(LevelSlotType.Cleard);
                if (easyLevels[i + 1] != null)
                {
                    easyLevels[i + 1].SetLevelType(LevelSlotType.Opend); //클리어한 다음 스테이지 열어주기
                }
            }
            else
            {
                easyLevels[i].SetLevelType(LevelSlotType.Closed);
            }
        }
        //----------------------------------------------------------------
        for (int i = 0; i < normalLevels.Count; i++)
        {
            Debug.Log($"{i + normalLeveloffset} 번째 노말 맵");
            if (GameManager.Instance.IsClearStage(i + normalLeveloffset))
            {
                normalLevels[i].SetLevelType(LevelSlotType.Cleard);
                if (normalLevels[i + 1] != null)
                {
                    normalLevels[i + 1].SetLevelType(LevelSlotType.Opend); //클리어한 다음 스테이지 열어주기
                }
            }
            else
            {
                normalLevels[i].SetLevelType(LevelSlotType.Closed);
            }
        }
        //----------------------------------------------------------------
        for (int i = 0; i < hardLevels.Count; i++)
        {
            Debug.Log($"{i + hardLeveloffset} 번째 하드 맵");
            if (GameManager.Instance.IsClearStage(i + hardLeveloffset))
            {
                hardLevels[i].SetLevelType(LevelSlotType.Cleard);
                if (hardLevels[i + 1] != null)
                {
                    hardLevels[i + 1].SetLevelType(LevelSlotType.Opend); //클리어한 다음 스테이지 열어주기
                }
            }
            else
            {
                hardLevels[i].SetLevelType(LevelSlotType.Closed);
            }
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

    //레벨 선택 버튼 해서 셋액티브하기
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
