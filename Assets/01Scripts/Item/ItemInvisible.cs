using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvisible : ItemBase
{
    public override void EffectItem(BallController ball)
    {
        ball.IsInvisible = true;
    }
}
