using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOutCollision : MonoBehaviour
{
    private bool _isTriggered = false;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isTriggered) return;
        
        if (other.gameObject.TryGetComponent(out BallController player))
        {
            if (player is not null)
            {
                InfiniteMapManager.Instance.UpHeight();
                InfiniteMapManager.Instance.CreateMap();

                if (SaveManager.Exist("maxScore.json"))
                {
                    MaxScore savedMaxScore = SaveManager.Load<MaxScore>("maxScore.json");

                    if (savedMaxScore.maxScore < InfiniteMapManager.Instance.height)
                    {
                        InfiniteMapManager.Instance.maxScore.maxScore = InfiniteMapManager.Instance.height;
                        SaveManager.Save(InfiniteMapManager.Instance.maxScore, "maxScore.json");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    InfiniteMapManager.Instance.maxScore = new MaxScore();
                    InfiniteMapManager.Instance.maxScore.maxScore = InfiniteMapManager.Instance.height;
                    SaveManager.Save(InfiniteMapManager.Instance.maxScore, "maxScore.json");
                }
                _isTriggered = true;
            }
        }
    }
}
