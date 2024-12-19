using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class MaxScore
{
    public int maxScore;
}

public class InfiniteMapManager : MonoSingleton<InfiniteMapManager>
{
    public int height = 0;
    [SerializeField] private TextMeshProUGUI _heightText;
    private StringBuilder _sb = new StringBuilder();
    private StringBuilder _sb2 = new StringBuilder();
    
    [SerializeField] private GameObject _deadObject;
    [SerializeField] private Wind _wind;
    
    [SerializeField] private MapDataSO mapDataSO;
    [SerializeField] private int _initialMapSize = 10;

    public float _spawnPosX;
    public float _spawnPosY = 6f;

    public float _spawnYOffset = 0f;

    private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
    
    public MaxScore maxScore;
    
    protected override void Awake()
    {
        base.Awake();

        if (SaveManager.Exist("maxScore.json"))
        {
            maxScore = SaveManager.Load<MaxScore>("maxScore.json");
        }
        
        for (int i = 0; i < _initialMapSize; i++)
        {
            CreateMap();
        }
    }

    private void Start()
    {
        StartCoroutine(nameof(StartDeadMove));
    }

    public void UpHeight()
    {
        height += 10;
        _sb.Clear();

        _sb.Append("Height: ");
        _sb.Append(height.ToString());
        _sb.Append("m");
        
        _heightText.text = _sb.ToString();
    }
    
    public void CreateMap()
    {
        Instantiate(mapDataSO._mapPrefabs[Random.Range(0, mapDataSO._mapPrefabs.Count)]
            , new Vector3(_spawnPosX, _spawnPosY + _spawnYOffset, 0), Quaternion.identity);

        _spawnYOffset += 11f;
    }

    private IEnumerator StartDeadMove()
    {
        GameObject go = Instantiate(_deadObject, new Vector3(_spawnPosX, -50f, 0), Quaternion.identity);
        
        yield return new WaitForSeconds(1f);
        
        while (true)
        {
            go.transform.position += new Vector3(0f, 1f * Time.deltaTime, 0f);
            yield return _waitForEndOfFrame;
        }
    }

    private IEnumerator StartWind()
    {
        yield return new WaitForSeconds(30f);
        Debug.Log("Wind start");

        while (true)
        {
            _wind.windForce = Random.Range(30f, 200f);
            _wind.windDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            yield return new WaitForSeconds(10f);
        }
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }
}
