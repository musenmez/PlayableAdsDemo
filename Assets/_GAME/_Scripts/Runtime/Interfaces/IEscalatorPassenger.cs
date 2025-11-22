using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public interface IEscalatorPassenger
    {
        Transform TargetTransform { get; }
        bool IsUsingEscalator { get; }
        void EnterEscalator(EscalatorStep step);
        void ExitEscalator();
    }
}
