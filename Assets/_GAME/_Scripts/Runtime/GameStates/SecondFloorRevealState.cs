using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class SecondFloorRevealState : GameStateBase
    {
        private Coroutine _revealCo;
        
        public override void Enter()
        {
            StopCoroutine(_revealCo);
            _revealCo = GameManager.Instance.StartCoroutine(RevealCo());
        }

        public override void Exit()
        {
            base.Exit();
            if (_revealCo != null)
                StopCoroutine(_revealCo);
        }

        private IEnumerator RevealCo()
        {
            Player.Instance.Movement.DisableMovement();
            CameraManager.Instance.ActivateCamera(CameraId.SecondFloor, 1f);
            SecondFloorController.Instance.RevealProps();
            PassengerCreator.Instance.SpawnPassengers(6);

            var delay = SecondFloorController.Instance.GetTotalRevealDuration();
            yield return new WaitForSeconds(delay);
            
            CameraManager.Instance.ActivateCamera(CameraId.InGame, 0.75f);
            yield return new WaitForSeconds(0.25f);

            GameManager.Instance.SetState(GameStateId.InGame);
        }
    }
}
