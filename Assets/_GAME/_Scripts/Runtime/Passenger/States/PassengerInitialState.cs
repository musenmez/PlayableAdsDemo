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
            Passenger.Animator.SetLayerWeight(PassengerAnimator.CARRY_LAYER, 1);
            //Spawn Baggage
            Passenger.SetState(PassengerStateId.BaggageDeposit);
        }
    }
}
