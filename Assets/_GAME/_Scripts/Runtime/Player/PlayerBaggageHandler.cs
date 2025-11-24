using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerBaggageHandler : MonoBehaviour
    {
        [SerializeField] private Transform baggagePivotPoint;

        private List<BaggagePair> _baggagePairs = new();
        private List<Transform> _baggageHolders = new();

        private const float HOLDER_SPACING = 0.625f;
        private const float MOVEMENT_LERP = 50f;
        private const float ROTATION_LERP = 30f;
        private const float SCALE_EFFECT_DELAY = 0.05f;
        
        public void TakeBaggage(Baggage baggage)
        {
            var pair = CreatePair(baggage);
            PlacementTween(pair);
        }

        private void Update()
        {
            SmoothFollow();
        }

        private BaggagePair CreatePair(Baggage baggage)
        {
            var holder = GetHolder(_baggagePairs.Count);
            baggage.transform.SetParent(holder);
            
            var pair = new BaggagePair(holder, baggage);
            _baggagePairs.Add(pair);
            return pair;
        }

        private Transform GetHolder(int index)
        {
            if (_baggageHolders.Count <= index)
            {
                var baggageHolder = new GameObject($"Baggage Holder {_baggagePairs.Count + 1}")
                {
                    transform =
                    {
                        position = baggagePivotPoint.position + HOLDER_SPACING * index * Vector3.up,
                        rotation = baggagePivotPoint.rotation
                    }
                };
                _baggageHolders.Add(baggageHolder.transform);
            }
            return _baggageHolders[index];
        }

        private void SmoothFollow()
        {
            if (_baggagePairs.Count == 0) return;

            for (var i = 0; i < _baggagePairs.Count; i++)
            {
                if (i == 0)
                {
                    _baggagePairs[i].Holder.position = baggagePivotPoint.position;
                    _baggagePairs[i].Holder.rotation = baggagePivotPoint.rotation;
                    continue;
                }
                
                var targetPosition = _baggagePairs[i - 1].Holder.position + Vector3.up * HOLDER_SPACING;
                _baggagePairs[i].Holder.position = Vector3.Lerp(_baggagePairs[i].Holder.position, targetPosition, Time.deltaTime * MOVEMENT_LERP);
                _baggagePairs[i].Holder.rotation = Quaternion.Slerp(_baggagePairs[i].Holder.rotation, _baggagePairs[i - 1].Holder.rotation, Time.deltaTime * ROTATION_LERP);
            }
        }
        
        private void PlacementTween(BaggagePair pair)
        {
            pair.Baggage.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.25f).SetEase(Ease.Linear);
            pair.Baggage.transform.DOLocalJump(Vector3.zero, 2f, 1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                pair.IsPlaced = true;
                ScaleEffect();
            });
        }
        
        private void ScaleEffect()
        {
            for (var i = 0; i < _baggagePairs.Count; i++)
            {
                if(!_baggagePairs[i].IsPlaced) continue;

                _baggagePairs[i].Holder.DOComplete();
                _baggagePairs[i].Holder.DOPunchScale(Vector3.one * 0.1f, 0.2f).SetEase(Ease.InOutSine).SetDelay(i * SCALE_EFFECT_DELAY);
            }
        }
    }

    public class BaggagePair
    {
        public bool IsPlaced;
        public Transform Holder;
        public Baggage Baggage;

        public BaggagePair(Transform holder, Baggage baggage, bool isPlaced = false)
        {
            Holder = holder;
            Baggage = baggage;
            IsPlaced = isPlaced;
        }
    }
}
