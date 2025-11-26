using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerTicketControlStation : PassengerStationBase
    {
        [Header("Ticket Control Station")]
        [SerializeField] private TicketControlStationPaymentHandler paymentHandler;

        protected override void StationBehaviour()
        {
            if (!Airplane.Instance.IsAvailable)
                return;
            
            var passenger = GetAvailablePassenger();
            if (passenger is null)
                return;

            paymentHandler.ReceivePayment(passenger);
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
