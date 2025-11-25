using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class Airplane : MonoBehaviour
    {
        public static Airplane Instance { get; private set; }
        public bool IsAvailable { get; private set; } = true;

        [SerializeField] private int capacity;
        [SerializeField] private TextMeshPro capacityText;
        [field : SerializeField] public Transform DoorPoint { get; private set; }
        
        private const float UNLOAD_OFFSET = 35f;

        private int _currentCapacity;
        private Tween _punchTween;
        private Sequence _flySeq;
        private Vector3 _defaultPosition;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void OnEnable()
        {
            _currentCapacity = 0;
            _defaultPosition = transform.position;
        }

        public void AddPassenger()
        {
            if (_currentCapacity >= capacity) return;

            _currentCapacity++;
            IsAvailable = _currentCapacity < capacity;
            SetText();
            PunchEffect();
        }

        private void SetText()
        {
            capacityText.SetText($"{_currentCapacity}/{capacity}");
        }
        
        private void Fly()
        {
            if (_currentCapacity < capacity)
                return;

            _flySeq?.Kill();
            _flySeq = DOTween.Sequence();
            _flySeq.Append(transform.DOMoveX(_defaultPosition.x + UNLOAD_OFFSET, 1f).SetEase(Ease.InOutSine))
                .AppendCallback(() =>
                {
                    _currentCapacity = 0;
                    SetText();
                })
                .AppendInterval(0.25f)
                .Append(transform.DOMoveX(_defaultPosition.x, 1f).SetEase(Ease.InOutSine)).OnComplete(() =>
                {
                    IsAvailable = true;
                });
        }
        
        private void PunchEffect()
        {
            _punchTween.Complete();
            _punchTween = capacityText.transform.DOPunchScale(Vector3.one * 0.3f, 0.15f).SetEase(Ease.InOutSine).OnComplete(Fly);
        }
    }
}
