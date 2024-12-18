using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManage : MonoBehaviour
{
    public static LevelManage Instance;

    [SerializeField] private Transform levelParent;
    [SerializeField] private List<LevelSlot> levels = new List<LevelSlot>();

    private int currentLevel = 0;

    [Header("LevelsButtons")]
    [SerializeField] private Transform easyLevel;
    [SerializeField] private Transform normalLevel;
    [SerializeField] private Transform hardLevel;

    private void OnEnable()
    {
        for (int i = 0; i < levelParent.childCount; i++)
        {
            levels.Add(levelParent.GetChild(i).GetComponent<LevelSlot>());
        }

        SetLockedLevelSlot();
        SetClearLevleSlot();

        EasyLevelOpen();
    }

    public void SetLockedLevelSlot()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetLevelType(LevelSlotType.Closed);
        }
    }

    public void SetClearLevleSlot()
    {
        for(int i = 0; i < currentLevel; i++)
        {
            levels[i].SetLevelType(LevelSlotType.Cleard);
        }

        levels[currentLevel].SetLevelType(LevelSlotType.Opend);
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
