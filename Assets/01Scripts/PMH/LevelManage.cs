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

        CheckLevelsCleared();
        SetClearLevleSlot();

        EasyLevelOpen();
    }

    private void Update()
    {
        CheckLevelsCleared();
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
    public void CheckLevelsCleared()
    {
        for (int i = 1; i <= easyLevels.Count; i++)
        {
            //Debug.Log($"{i + 0} 번째 이지 맵");
            if (GameManager.Instance.IsClearStage(i) == false)
            {
                easyLevels[i - 1].SetLevelType(LevelSlotType.Closed);
            }
        }
        
        for (int i = 1; i <= easyLevels.Count; i++)
        {
            //Debug.Log($"{i + 0} 번째 이지 맵");
            if (GameManager.Instance.IsClearStage(i))
            {
                easyLevels[i - 1].SetLevelType(LevelSlotType.Cleard);
                easyLevels[i].SetLevelType(LevelSlotType.Opend);
            }
        }


        //열어줬던걸 다시 닫는 레전드 엄코드
        //----------------------------------------------------------------
        for (int i = 1; i <= normalLevels.Count; i++)
        {
            if (GameManager.Instance.IsClearStage(i + 10) == false)
            {
                normalLevels[i - 1].SetLevelType(LevelSlotType.Closed);
            }
        }

        for (int i = 1; i <= normalLevels.Count; i++)
        {
            if (GameManager.Instance.IsClearStage(i + 10))
            {
                normalLevels[i - 1].SetLevelType(LevelSlotType.Cleard);
                normalLevels[i].SetLevelType(LevelSlotType.Opend);
            }
        }


        //----------------------------------------------------------------
        for (int i = 1; i <= hardLevels.Count; i++)
        {
            if (GameManager.Instance.IsClearStage(i + 40) == false)
            {
                hardLevels[i - 1].SetLevelType(LevelSlotType.Closed);
            }
        }

        for (int i = 1; i <= hardLevels.Count; i++)
        {
            if (GameManager.Instance.IsClearStage(i + 40))
            {
                hardLevels[i - 1].SetLevelType(LevelSlotType.Cleard);
                hardLevels[i].SetLevelType(LevelSlotType.Opend);
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
