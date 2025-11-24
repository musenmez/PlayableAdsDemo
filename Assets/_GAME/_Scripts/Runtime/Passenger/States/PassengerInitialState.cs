using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerInitialState : PassengerStateBase
    {
        public override void EnterState(Passenger passenger)
        {
            base.EnterState(passenger);
            Passenger.SkinController.SetRandomColor();
            Passenger.BaggageHandler.CreateBaggage();
            Passenger.SetState(PassengerStateId.BaggageDeposit);
        }
    }
}
