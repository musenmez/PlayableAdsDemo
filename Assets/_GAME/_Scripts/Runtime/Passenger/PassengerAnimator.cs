using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerAnimator : MonoBehaviour
    {
        private Passenger _passenger;
        private Passenger Passenger => _passenger == null ? _passenger = GetComponentInParent<Passenger>() : _passenger;
        
        public const int CARRY_LAYER = 1;
        
        [SerializeField] private Animator animator;

        private Tween _layerTween;
        private Vector3 _oldPosition;

        private void OnEnable()
        {
            _oldPosition = transform.position;
        }

        private void Update()
        {
            SetSpeed();
        }

        public void SetTrigger(int triggerHash)
        {
            animator.SetTrigger(triggerHash);
        }
        
        public void SetLayerWeight(int layerIndex, float weight, float duration = 0f)
        {
            _layerTween.Kill();
            _layerTween = DOVirtual.Float(animator.GetLayerWeight(layerIndex), weight, duration: duration, (x) => animator.SetLayerWeight(layerIndex, x));
        }
        
        private void SetSpeed()
        {
            var distance = Passenger.EscalatorHandler.IsUsingEscalator ? 0 : Vector3.Distance(_oldPosition, transform.position);
            animator.SetFloat(AnimationHashes.Speed, distance);
            _oldPosition = transform.position;
        }
    }
}
