using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManage : MonoBehaviour
{
    public static LevelManage Instance;

    [SerializeField] private Transform levelParent;
    [SerializeField] private List<LevelSlot> levels = new List<LevelSlot>();

    private int currentLevel = 0;

    private void OnEnable()
    {
        for (int i = 0; i < levelParent.childCount; i++)
        {
            levels.Add(levelParent.GetChild(i).GetComponent<LevelSlot>());
        }

        SetLockedLevelSlot();
        SetClearLevleSlot();
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
}
