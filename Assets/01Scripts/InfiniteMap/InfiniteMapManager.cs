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
    [SerializeField] private TextMeshProUGUI _maxScoreText;
    
    private StringBuilder _sb = new StringBuilder();
    private StringBuilder _sb2 = new StringBuilder();
    
    [SerializeField] private GameObject _deadObject;
    
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
        
        _sb = new StringBuilder();
        _sb2 = new StringBuilder();

        if (SaveManager.Exist("maxScore.json"))
        {
            maxScore = SaveManager.Load<MaxScore>("maxScore.json");
            _maxScoreText.text = $"best height: {maxScore.maxScore.ToString()}m";
        }
        else
        {
            _maxScoreText.text = $"best height: 0m";
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

        if (height > maxScore.maxScore)
        {
            _maxScoreText.text = $"best height: {height.ToString()}m";
        }
        
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
        
        yield return new WaitForSeconds(30f);
        
        while (true)
        {
            go.transform.position += new Vector3(0f, 2f * Time.deltaTime, 0f);
            yield return _waitForEndOfFrame;
        }
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }
}
