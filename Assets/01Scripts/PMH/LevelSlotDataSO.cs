using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelDiff
{
    Easy, Normal, Hard
}
[CreateAssetMenu(menuName = "SO/LevelData")]
public class LevelSlotDataSO : ScriptableObject
{
    public GameObject Level;
    public string levelName;
    public LevelDiff levelDifficulty;
}
