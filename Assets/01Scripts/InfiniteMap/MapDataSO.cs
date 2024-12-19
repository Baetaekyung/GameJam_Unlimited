using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapDataSO")]
public class MapDataSO : ScriptableObject
{
    public List<GameObject> _mapPrefabs = new List<GameObject>();
}
