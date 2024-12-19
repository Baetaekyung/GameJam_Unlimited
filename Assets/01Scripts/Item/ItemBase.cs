using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class ItemBase : MonoBehaviour
{
    protected BallController _ball;
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ball))
        {
            _ball = ball;
            EffectItem(_ball);

            SoundManager.Instance.PlayerSFX(SfxType.TAKEITEM);

            Destroy(this.gameObject);
        }
    }

    public abstract void EffectItem(BallController ball);
}
