using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class StationBase : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; } = true;
        
        [field : Header("Station Settings"), SerializeField] public StationId StationId { get; protected set; }

        public virtual void Interact(Interactor interactor)
        {
            StartStation();
        }

        public virtual void InteractorExit(Interactor interactor)
        {
            StopStation();
        }

        protected abstract void StartStation();
        
        protected virtual void StopStation(){}
    }
}
