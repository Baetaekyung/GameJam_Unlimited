using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJetpack : ItemBase
{
    public override void EffectItem(BallController ball)
    {
        BallInputController.Instance.isJetpack = true;
        BallInputController.Instance.jetpackRemainTime = BallInputController.Instance.jetpackDuration;
    }
}
