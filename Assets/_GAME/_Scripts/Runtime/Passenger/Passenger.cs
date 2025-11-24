using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class Passenger : MonoBehaviour
    {
        public PassengerStateBase CurrentState { get; private set; }
        public PassengerStateId CurrentStateId { get; private set; }
        
        [field : SerializeField] public PassengerSkinController SkinController { get; private set; }
        [field : SerializeField] public PassengerAnimator Animator { get; private set; }
        
        public Dictionary<PassengerStateId, PassengerStateBase> StatesById { get; private set; } = new()
        {
            { PassengerStateId.Initial, new PassengerInitialState() },
            { PassengerStateId.BaggageDeposit, new PassengerBaggageDepositState() },
        };

        private void Start()
        {
            SetState(PassengerStateId.Initial);
        }

        private void Update()
        {
            CurrentState?.UpdateState();
        }
        
        public void SetState(PassengerStateId stateId)
        {
            if (!StatesById.ContainsKey(stateId))
            {
                Debug.LogError($"State Id not exist {stateId}");
                return;
            }

            CurrentState?.ExitState();  
            CurrentStateId = stateId;
            CurrentState = StatesById[stateId];
            CurrentState.EnterState(this);
        }
    }
}
