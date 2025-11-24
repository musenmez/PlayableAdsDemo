using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class BaggageAdmissionStation : PassengerStationBase
    {
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
                // if (!IsPassengerAvailable())
                //     yield return null;

                DepositBaggage();
                yield return DELAY;
            }
        }

        private void DepositBaggage()
        {
            var passenger = GetAvailablePassenger();
            if (passenger is null)
                return;
            
            passenger.BaggageHandler.DepositBaggage();
            passenger.CompleteStation();
        }

        private void StopProgressing()
        {
            if(_progressCo != null)
                StopCoroutine(_progressCo);
        }
    }
}
