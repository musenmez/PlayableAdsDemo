using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class StationBase : MonoBehaviour
    {
        [field : Header("Station Base Settings"), SerializeField] public StationId StationId { get; protected set; }
        [SerializeField] private float progressDelay;

        protected WaitForSeconds _progressDelay;
        protected Coroutine _progressCoroutine;

        protected virtual void Awake()
        {
            _progressDelay = new WaitForSeconds(progressDelay);
        }

        public virtual void StartProgress(object parameter)
        {
            StopProgressCoroutine();
            _progressCoroutine = StartCoroutine(ProgressCo());
        }

        public virtual void StopProgress()
        {
            StopProgressCoroutine();
        }

        protected IEnumerator ProgressCo()
        {
            while (true)
            {
                StationBehaviour();
                yield return _progressDelay;
            }
        }

        protected virtual void StopProgressCoroutine()
        {
            if (_progressCoroutine != null)
            {
                StopCoroutine(_progressCoroutine);
            }
        }
        
        protected abstract void StationBehaviour();
        
    }
}
