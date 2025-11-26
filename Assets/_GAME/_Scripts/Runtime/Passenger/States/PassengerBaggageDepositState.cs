using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerBaggageDepositState : PassengerStateBase
    {
        private PassengerStationBase _station;
        private StationLineInfo _lineInfo;
        
        public override void EnterState(Passenger passenger)
        {
            base.EnterState(passenger);
            _station = StationManager.Instance.GetStation(StationId.BaggageAdmission) as PassengerStationBase;
            _lineInfo = _station.AddPassenger(Passenger);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            var spacing = _lineInfo.Index == 0 ? 0 : _lineInfo.Spacing;
            var target = _station.GetPassengerLineTarget(_lineInfo);
            var position = target.position + spacing * _lineInfo.Direction;
            Passenger.Movement.MoveTowards(position, Time.deltaTime);
            Passenger.Movement.RotateTowards(position - _lineInfo.Direction, Time.deltaTime);
        }

        public override void CompleteStation()
        {
            _station.RemovePassenger(_lineInfo);
            Passenger.SetState(PassengerStateId.Escalator);
        }
    }
}
