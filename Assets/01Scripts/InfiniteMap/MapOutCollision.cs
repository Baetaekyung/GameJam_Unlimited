using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOutCollision : MonoBehaviour
{
    private Collider2D _col;
    private bool _mapCreated = false;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out BallController player))
        {
            if (player is not null)
            {
                InfiniteMapManager.Instance?.CreateMap();
                _mapCreated = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out BallController player))
        {
            if (player is not null)
            {
                InfiniteMapManager.Instance?.UpHeight();

                if (_mapCreated == false)
                    InfiniteMapManager.Instance?.CreateMap();
                
                if (SaveManager.Exist("maxScore.json"))
                {
                    MaxScore savedMaxScore = SaveManager.Load<MaxScore>("maxScore.json");

                    if (savedMaxScore.maxScore < InfiniteMapManager.Instance?.height)
                    {
                        InfiniteMapManager.Instance.maxScore.maxScore = InfiniteMapManager.Instance.height;
                        SaveManager.Save(InfiniteMapManager.Instance.maxScore, "maxScore.json");
                    }
                }
                else
                {
                    InfiniteMapManager.Instance.maxScore = new MaxScore();
                    InfiniteMapManager.Instance.maxScore.maxScore = InfiniteMapManager.Instance.height;
                    SaveManager.Save(InfiniteMapManager.Instance.maxScore, "maxScore.json");
                }
            }

            _col.enabled = false;
        }
    }
}
