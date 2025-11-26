using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class BaggageAdmissionStation : PassengerStationBase
    {
        protected override void StationBehaviour()
        {
            var passenger = GetAvailablePassenger();
            if (passenger is null)
                return;
            
            passenger.BaggageHandler.DepositBaggage();
            passenger.CompleteStation();
            CheckTask();
        }

        private void CheckTask()
        {
            if (_passengerLineInfos.Count > 0)
                return;
            
            TaskManager.Instance.CompleteTask(this);
        }
    }
}
