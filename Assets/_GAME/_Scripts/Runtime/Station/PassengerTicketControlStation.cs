using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerTicketControlStation : PassengerStationBase
    {
        [Header("Ticket Control Station")]
        [SerializeField] private TicketControlStationPaymentHandler paymentHandler;
        
        private Coroutine _progressCo;

        private readonly WaitForSeconds DELAY = new WaitForSeconds(0.5f);

        protected override void StartStation()
        {
            StopProgressing();
            _progressCo = StartCoroutine(ProgressCo());
        }

        protected override void StopStation()
        {
            base.StopStation();
            StopProgressing();
        }

        private IEnumerator ProgressCo()
        {
            while (true)
            {
                ApprovePassenger();
                yield return DELAY;
            }
        }

        private void ApprovePassenger()
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

        private void StopProgressing()
        {
            if (_progressCo != null)
                StopCoroutine(_progressCo);
        }
    }
}
