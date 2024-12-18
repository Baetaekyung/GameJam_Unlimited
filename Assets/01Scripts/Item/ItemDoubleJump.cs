using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDoubleJump : ItemBase
{
    public override void EffectItem(BallController ball)
    {
        ball.SetShootCount(2);
    }
}
