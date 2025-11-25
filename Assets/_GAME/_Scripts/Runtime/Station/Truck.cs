using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class Truck : MonoBehaviour
    {
        public bool IsAvailable { get; private set; } = true;
        
        [SerializeField] private int capacity;
        [SerializeField] private Transform baggageHolder;
        [SerializeField] private Transform scaleBody;

        private const float UNLOAD_OFFSET = 35f;
        private const float BAGGAGE_SPACING = 0.625f;
        
        private Tween _punchTween;
        private Sequence _unloadSeq;
        private Vector3 _defaultPosition;
        private readonly List<Baggage> _baggages = new List<Baggage>();
        
        private void OnEnable()
        {
            _defaultPosition = transform.position;
        }

        public void AddBaggage(Baggage baggage)
        {
            if (_baggages.Count >= capacity) return;
            
            _baggages.Add(baggage);
            IsAvailable = _baggages.Count < capacity;
            JumpTween(baggage, _baggages.Count - 1);
        }

        private void JumpTween(Baggage baggage, int index)
        {
            baggage.transform.DOKill();
            baggage.transform.SetParent(baggageHolder);
            baggage.transform.localScale = Vector3.one;
            baggage.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.2f).SetEase(Ease.Linear);
            baggage.transform.DOLocalJump(Vector3.zero + index * BAGGAGE_SPACING * Vector3.up, 2f, 1, 0.25f).SetEase(Ease.Linear).OnComplete(PunchEffect);
        }

        private void PunchEffect()
        {
            _punchTween.Complete();
            _punchTween = scaleBody.DOPunchScale(Vector3.one * 0.1f, 0.15f).SetEase(Ease.InOutSine).OnComplete(UnloadBaggages);
        }

        private void UnloadBaggages()
        {
            if (_baggages.Count < capacity)
                return;

            _unloadSeq?.Kill();
            _unloadSeq = DOTween.Sequence();
            _unloadSeq.Append(transform.DOMoveX(_defaultPosition.x + UNLOAD_OFFSET, 1f).SetEase(Ease.InOutSine))
                .AppendCallback(ClearBaggages)
                .AppendInterval(0.25f)
                .Append(transform.DOMoveX(_defaultPosition.x, 1f).SetEase(Ease.InOutSine)).OnComplete(() =>
                {
                    IsAvailable = true;
                });
        }

        private void ClearBaggages()
        {
            foreach (var baggage in _baggages)
            {
                baggage.gameObject.SetActive(false);
            }
            _baggages.Clear();
        }
    }
}
