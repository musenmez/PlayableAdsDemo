using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerMoveTowardsXrayState : PassengerStateBase
    {
        private const float PATH_DURATION = 1f;
        
        public override void EnterState(Passenger passenger)
        {
            base.EnterState(passenger);
            MoveTowardsXray();
        }

        private void MoveTowardsXray()
        {
            var xrayPath = PathManager.Instance.GetPath(PathId.Xray);
            var startPos = Passenger.transform.position;
            var path = xrayPath.GetPath(startPos);
            Passenger.Movement.FollowPath(path, PATH_DURATION, onComplete: CompleteState);
        }

        private void CompleteState()
        {
            Passenger.SetState(PassengerStateId.Xray);
        }
    }
}
