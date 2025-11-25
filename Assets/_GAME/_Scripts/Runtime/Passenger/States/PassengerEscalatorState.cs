using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerEscalatorState : PassengerStateBase
    {
        private Escalator _escalator;

        private const float PATH_DURATION = 2f;
        
        public override void EnterState(Passenger passenger)
        {
            base.EnterState(passenger);
            MoveToEscalator();
        }

        private void MoveToEscalator()
        {
            var escalatorPath = PathManager.Instance.GetPath(PathId.Escalator);
            _escalator = EscalatorManager.Instance.GetEscalator(EscalatorDirection.Up);
            
            var startPos = Passenger.transform.position;
            var endPos = _escalator.BottomPoint.transform.position;
            endPos.y = _escalator.BottomFloorHeight;
            
            var path = escalatorPath.GetPath(startPos, endPos);
            Passenger.Movement.FollowPath(path, PATH_DURATION, onComplete: EnterEscalator);
        }

        private void EnterEscalator()
        {
            _escalator.TryAddPassenger(Passenger.EscalatorHandler);
            Passenger.EscalatorHandler.OnExited.AddListener(CompleteState);
        }

        private void CompleteState()
        {
            Passenger.EscalatorHandler.OnExited.RemoveListener(CompleteState);
            Passenger.SetState(PassengerStateId.MoveTowardsXray);
        }
    }
}
