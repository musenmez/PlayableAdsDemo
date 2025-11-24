using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class StationBase : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; } = true;
        public bool IsWorking { get; private set; }
        
        [field : Header("Station Settings"), SerializeField] public StationId StationId { get; protected set; }

        public virtual void Interact(Interactor interactor)
        {
            if (IsWorking) 
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
    }
}
