using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public interface ITask
    {
        bool IsTaskActive { get; }
        Transform IndicatorTarget { get; }
        void ActivateTask();
        void DeactivateTask();
        
        UnityEvent OnTaskActivated { get; }
        UnityEvent OnTaskDeactivated { get; }
    }
}
