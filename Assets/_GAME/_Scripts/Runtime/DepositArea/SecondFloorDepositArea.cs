using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class SecondFloorDepositArea : DepositAreaBase
    {
        protected override void Unlock()
        {
            if (IsCompleted) return;
            base.Unlock();
            GameManager.Instance.SetState(GameStateId.SecondFloorReveal);
        }
    }
}
