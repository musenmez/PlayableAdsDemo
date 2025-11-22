using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class SecondFloorRevealState : GameStateBase
    {
        private const float REVEAL_OFFSET = 0f;
        
        private Coroutine _revealCo;
        
        public override void Enter()
        {
            StopCoroutine(_revealCo);
            _revealCo = GameManager.Instance.StartCoroutine(RevealCo());
        }

        public override void Exit()
        {
            base.Exit();
            StopCoroutine(_revealCo);
        }

        private IEnumerator RevealCo()
        {
            Player.Instance.Movement.DisableMovement();
            CameraManager.Instance.ActivateCamera(CameraId.SecondFloor, 1f);
            SecondFloorController.Instance.RevealProps();

            var delay = SecondFloorController.Instance.GetTotalRevealDuration() - REVEAL_OFFSET;
            delay = Mathf.Max(delay, 0.1f);
            yield return new WaitForSeconds(delay );
            
            CameraManager.Instance.ActivateCamera(CameraId.InGame, 0.75f);
            yield return new WaitForSeconds(0.25f);

            GameManager.Instance.SetState(GameStateId.InGame);
        }
    }
}
