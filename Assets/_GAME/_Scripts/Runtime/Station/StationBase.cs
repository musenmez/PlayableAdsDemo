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
        [SerializeField] protected float progressDelay = 0.5f;
        
        private Coroutine _progressCo;
        private WaitForSeconds _progressDelay;
        
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
        
        protected virtual void StartStation()
        {
            StopProgressing();
            _progressDelay = new WaitForSeconds(progressDelay);
            _progressCo = StartCoroutine(ProgressCo());
        }

        protected virtual void StopStation()
        {
            StopProgressing();
        }

        protected virtual IEnumerator ProgressCo()
        {
            while (true)
            {
                StationBehaviour();
                yield return _progressDelay;
            }
        }
        
        protected virtual void StopProgressing()
        {
            if (_progressCo != null)
                StopCoroutine(_progressCo);
        }

        protected abstract void StationBehaviour();
    }
}
