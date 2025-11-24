using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class PassengerStateBase
    {
        public Passenger Passenger { get; protected set; }
        
        public virtual void EnterState(Passenger passenger)
        {
            Passenger = passenger;
        }
        
        public virtual void UpdateState(){}
        
        public virtual void CompleteStation(){}
        
        public virtual void ExitState(){}
    }
}
