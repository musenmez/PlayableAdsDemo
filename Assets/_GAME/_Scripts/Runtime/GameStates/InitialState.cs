using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class InitialState : GameStateBase
    {
        private Coroutine _initialCo;
        
        private const float INITIAL_DELAY = 0.2f;
        
        public override void Enter()
        {
            StopCoroutine(_initialCo);
            _initialCo = GameManager.Instance.StartCoroutine(InitializeCo());
        }

        public override void Exit()
        {
            base.Exit();
            if (_initialCo != null)
                StopCoroutine(_initialCo);
        }

        private IEnumerator InitializeCo()
        {
            PoolingManager.Instance.Initialize();
            CurrencyManager.Instance.Initialize();
            TaskManager.Instance.Initialize();
            yield return new WaitForSeconds(INITIAL_DELAY);
            DOVirtual.DelayedCall(0.1f, () => GameManager.Instance.SetState(GameStateId.InGame));
        }
    }
}
