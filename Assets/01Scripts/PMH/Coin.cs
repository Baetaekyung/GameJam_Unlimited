using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] CoinStatSO coinStat;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = coinStat.coinImage;
        CircleCollider2D coll = GetComponent<CircleCollider2D>();
        coll.isTrigger = true;
        coll.offset = coinStat.collOffset;
        coll.radius = coinStat.collSize;
    }

    private void AddCoin()
    {
        int currentCoin = coinStat.coinValue;
        //CoinManager.Instance.AddCoin(currentCoin);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AddCoin();
        }
    }
}
