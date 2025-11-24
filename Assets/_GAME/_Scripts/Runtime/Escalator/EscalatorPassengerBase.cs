using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public abstract class EscalatorPassengerBase : MonoBehaviour, IEscalatorPassenger
    {
        public Transform TargetTransform => transform;
        public virtual Transform RotationBody => TargetTransform;
        public bool IsUsingEscalator { get; protected set; }

        public UnityEvent OnEntered { get; } = new();
        public UnityEvent OnExited { get; } = new();
        
        protected EscalatorStep _step;
        
        public virtual void EnterEscalator(EscalatorStep step)
        {
            _step = step;
            IsUsingEscalator = true;
            
            RotationBody.DOKill();
            TargetTransform.DOKill();
            
            var rotation = _step.Escalator.Direction == EscalatorDirection.Up ? Vector3.zero : Vector3.down * 180f;
            RotationBody.DOLocalRotate(rotation, 0.1f).SetEase(Ease.Linear);
            TargetTransform.DOLocalMove(Vector3.zero, 0.1f).SetEase(Ease.Linear);
            OnEntered.Invoke();
        }

        public virtual void ExitEscalator()
        {
            IsUsingEscalator = false;
            var floorHeight = _step.Escalator.Direction == EscalatorDirection.Up ? _step.Escalator.TopFloorHeight : _step.Escalator.BottomFloorHeight;
            TargetTransform.DOMoveY(floorHeight, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                OnExited.Invoke();
            });
        }
    }
}
