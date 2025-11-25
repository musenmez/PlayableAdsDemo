using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class LoopMovement : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private Vector3 movementOffset;

        private Vector3 _defaultPosition;
        
        private void Awake()
        {
            _defaultPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            transform.DOKill();
            transform.localPosition = _defaultPosition;
            transform.DOLocalMove(transform.localPosition + movementOffset, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}
