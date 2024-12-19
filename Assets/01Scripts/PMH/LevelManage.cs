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
            //Debug.Log($"{i + 0} ��° ���� ��");
            if (GameManager.Instance.IsClearStage(i) == false)
            {
                easyLevels[i - 1].SetLevelType(LevelSlotType.Closed);
            }
        }
        
        for (int i = 0; i < easyLevels.Count; i++)
        {
            //Debug.Log($"{i + 0} ��° ���� ��");
            if (GameManager.Instance.IsClearStage(i + 1))
            {
                easyLevels[i].SetLevelType(LevelSlotType.Cleard);

                if(i < easyLevels.Count)
                {
                    if(i == easyLevels.Count - 1)
                    {
                        break;
                    }
                    easyLevels[i + 1].SetLevelType(LevelSlotType.Opend);
                }
            }
        }


        //��������� �ٽ� �ݴ� ������ ���ڵ�
        //----------------------------------------------------------------
        for (int i = 1; i <= normalLevels.Count; i++)
        {
            if (GameManager.Instance.IsClearStage(i + 10) == false)
            {
                normalLevels[i - 1].SetLevelType(LevelSlotType.Closed);
            }
        }

        for (int i = 0; i < normalLevels.Count; i++)
        {
            if (GameManager.Instance.IsClearStage(i + 10))
            {
                normalLevels[i].SetLevelType(LevelSlotType.Cleard);

                if (i < normalLevels.Count)
                {
                    if (i == normalLevels.Count - 1)
                    {
                        break;
                    }
                    normalLevels[i + 1].SetLevelType(LevelSlotType.Opend);
                }
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

        for (int i = 0; i < hardLevels.Count; i++)
        {
            if (GameManager.Instance.IsClearStage(i + 40))
            {
                hardLevels[i].SetLevelType(LevelSlotType.Cleard);

                if (i < hardLevels.Count)
                {
                    if (i == hardLevels.Count - 1)
                    {
                        break;
                    }
                    hardLevels[i + 1].SetLevelType(LevelSlotType.Opend);
                }
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
