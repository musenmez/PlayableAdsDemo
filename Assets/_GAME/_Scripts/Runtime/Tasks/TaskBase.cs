using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public abstract class TaskBase : MonoBehaviour, ITask
    {
        public bool IsTaskActive { get; protected set; }
        public UnityEvent OnTaskActivated { get; } = new();
        public UnityEvent OnTaskDeactivated { get; } = new();
        
        [field : Header("Task Settings"), SerializeField] public FloorType TaskFloorType { get; protected set; }
        [field : SerializeField] public Transform IndicatorTarget { get; protected set; }

        public virtual void ActivateTask()
        {
            IsTaskActive = true;
            OnTaskActivated.Invoke();
        }

        public virtual void DeactivateTask()
        {
            IsTaskActive = false;
            OnTaskDeactivated.Invoke();
        }
    }
}
