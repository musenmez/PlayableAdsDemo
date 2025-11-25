using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Runtime
{
    public class BaggageTrayStation : StationBase
    {
        [SerializeField] private Transform baggageParent;

        private const float BAGGAGE_SPACING = 0.625f;
        private const float SCALE_EFFECT_DELAY = 0.05f;
        private readonly WaitForSeconds DELAY = new WaitForSeconds(0.5f);

        private readonly List<BaggagePair> _baggagePairs = new();
        private PlayerBaggageHandler _baggageHandler;
        private Coroutine _progressCo;
        
        /// <summary>
        /// Returns null if baggage placement is not finished
        /// </summary>
        public Baggage PopBottomBaggage()
        {
            if (_baggagePairs.Count == 0 || _baggagePairs[0].IsPlaced == false)
                return null;

            var baggagePair = _baggagePairs[0];
            _baggagePairs.RemoveAt(0);
            SortBaggages();
            return baggagePair.Baggage;
        }
        
        protected override void StartStation()
        {
            StopProgressing();
            _baggageHandler = Player.Instance.BaggageHandler;
            _progressCo = StartCoroutine(ProgressCo());
        }

        protected override void StopStation()
        {
            base.StopStation();
            StopProgressing();
        }

        private IEnumerator ProgressCo()
        {
            while (true)
            {
                LoadTray();
                yield return DELAY;
            }
        }
        
        private void LoadTray()
        {
            var baggagePair = _baggageHandler.PopBaggage();
            if (baggagePair == null)
                return;

            baggagePair.Holder = baggageParent;
            baggagePair.Baggage.transform.SetParent(baggageParent);
            baggagePair.Baggage.transform.localScale = Vector3.one;
            _baggagePairs.Add(baggagePair);
            
            PlacementTween(baggagePair, _baggagePairs.Count - 1);
        }
        
        private void PlacementTween(BaggagePair pair, int index)
        {
            pair.Baggage.transform.DOComplete();
            pair.Baggage.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.25f).SetEase(Ease.Linear);
            pair.Baggage.transform.DOLocalJump(Vector3.zero + index * BAGGAGE_SPACING * Vector3.up, 2f, 1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
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

                _baggagePairs[i].Baggage.transform.DOComplete();
                _baggagePairs[i].Baggage.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f).SetEase(Ease.InOutSine).SetDelay(i * SCALE_EFFECT_DELAY);
            }
        }

        private void SortBaggages()
        {
            for (var i = 0; i < _baggagePairs.Count; i++)
            {
                _baggagePairs[i].Baggage.transform.DOComplete();
                _baggagePairs[i].Baggage.transform.DOLocalMoveY(i * BAGGAGE_SPACING, 0.2f).SetDelay(0.2f).SetEase(Ease.OutBack);
            }
        }
        
        private void StopProgressing()
        {
            if(_progressCo != null)
                StopCoroutine(_progressCo);
        }
    }
}
