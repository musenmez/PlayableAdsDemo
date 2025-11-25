using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerXrayState : PassengerStateBase
    {
        private PassengerStationBase _station;
        private StationLineInfo _lineInfo;
        
        public override void EnterState(Passenger passenger)
        {
            base.EnterState(passenger);
            _station = StationManager.Instance.GetStation(StationId.PassengerXray) as PassengerStationBase;
            _lineInfo = _station.AddPassenger(Passenger);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            var spacing = _lineInfo.Index == 0 ? 0 : _lineInfo.Spacing;
            var target = _station.GetPassengerLineTarget(_lineInfo);
            var position = target.position + spacing * _lineInfo.Direction;
            Passenger.Movement.MoveTowards(position, Time.deltaTime);
        }

        public override void CompleteStation()
        {
            _station.RemovePassenger(_lineInfo);
            Passenger.gameObject.SetActive(false);
            //Spawn Money
            //Trigger Plane On board state
        }
    }
}
