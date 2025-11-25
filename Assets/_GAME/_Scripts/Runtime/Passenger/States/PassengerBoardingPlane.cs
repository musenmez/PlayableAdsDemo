using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerBoardingPlane : PassengerStateBase
    {
        private const float PATH_DURATION = 1f;
        
        public override void EnterState(Passenger passenger)
        {
            base.EnterState(passenger);
            MoveToPlane();
        }

        private void MoveToPlane()
        {
            var planePath = PathManager.Instance.GetPath(PathId.Plane);
            var startPos = Passenger.transform.position;
            var endPos = Airplane.Instance.DoorPoint.transform.position;
            var path = planePath.GetPath(startPos, endPos);
            Passenger.Movement.FollowPath(path, PATH_DURATION, onComplete: CompleteState);
        }

        private void CompleteState()
        {
            Airplane.Instance.AddPassenger();
            Passenger.gameObject.SetActive(false);
        }
    }
}
