using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public enum PassengerStateId
    {
        None = 0,
        Initial = 1,
        BaggageDeposit = 2,
        Escalator = 3,
        MoveTowardsXray = 4,
        Xray = 5
    }
}
