using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class GameStateBase
    {
        public abstract void Enter();
        public virtual void Exit(){}
        protected void StopCoroutine(Coroutine routine)
        {
            if (routine != null)
            {
                GameManager.Instance.StopCoroutine(routine);
            }
        }
    }
}
