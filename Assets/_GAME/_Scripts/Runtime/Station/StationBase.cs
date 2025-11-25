using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public abstract class StationBase : TaskBase, IInteractable
    {
        public bool IsInteractable { get; protected set; }
        public bool IsWorking { get; private set; }
        
        [field : Header("Station Settings"), SerializeField] public StationId StationId { get; protected set; }

        public virtual void Interact(Interactor interactor)
        {
            if (IsWorking || !IsInteractable) 
                return;
            
            IsWorking = true;
            StartStation();
        }

        public virtual void InteractorExit(Interactor interactor)
        {
            if (!IsWorking) 
                return;
            
            IsWorking = false;
            StopStation();
        }

        protected abstract void StartStation();
        
        protected virtual void StopStation(){}
        
        public override void ActivateTask()
        {
            IsInteractable = true;
            base.ActivateTask();
        }

        public override void DeactivateTask()
        {
            IsInteractable = false;
            base.DeactivateTask();
        }
    }
}
