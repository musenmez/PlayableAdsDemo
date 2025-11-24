using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerBaggageHandler : MonoBehaviour
    {
        private Passenger _passenger;
        private Passenger Passenger => _passenger == null ? _passenger = GetComponentInParent<Passenger>() : _passenger;
        
        [SerializeField] private Transform baggageParent;

        public void CreateBaggage()
        {
            var baggage = PoolingManager.Instance.GetInstance(PoolId.Baggage, baggageParent.position, baggageParent.rotation).transform;
            baggage.SetParent(baggageParent);
            Passenger.Animator.SetLayerWeight(PassengerAnimator.CARRY_LAYER, 1);
        }
    }
}
