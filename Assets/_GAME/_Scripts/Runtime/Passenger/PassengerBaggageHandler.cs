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
        
        private Baggage _baggage;

        public void CreateBaggage()
        {
            _baggage = PoolingManager.Instance.GetInstance(PoolId.Baggage, baggageParent.position, baggageParent.rotation).GetPoolComponent<Baggage>();
            _baggage.transform.SetParent(baggageParent);
            Passenger.Animator.SetLayerWeight(PassengerAnimator.CARRY_LAYER, 1);
        }

        public void DepositBaggage()
        {
            Player.Instance.BaggageHandler.TakeBaggage(_baggage);
            Passenger.Animator.SetLayerWeight(PassengerAnimator.CARRY_LAYER, 0, 0.2f);
            _baggage = null;
        }
    }
}
