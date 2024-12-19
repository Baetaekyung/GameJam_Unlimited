using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ball))
        {
            SoundManager.Instance.PlayerSFX(SfxType.LASERHIT);
            ball.Dead();
        }
    }
}
