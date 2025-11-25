using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerTicketControlStation : PassengerStationBase
    {
        private Coroutine _progressCo;

        private const int TICKET_PRICE = 20;
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

            SpawnCurrency();
            passenger.CompleteStation();
        }

        private void SpawnCurrency()
        {
            
        }

        private void StopProgressing()
        {
            if (_progressCo != null)
                StopCoroutine(_progressCo);
        }
    }
}
