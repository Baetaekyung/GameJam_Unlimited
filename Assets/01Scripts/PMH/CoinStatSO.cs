using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinStatSO", menuName = "SO/CoinStat")]
public class CoinStatSO : ScriptableObject
{
    public int coinValue;
    public Sprite coinImage;

    public float collSize;
    public Vector2 collOffset;
}