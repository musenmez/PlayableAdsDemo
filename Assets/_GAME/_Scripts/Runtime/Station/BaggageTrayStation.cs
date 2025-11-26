using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Runtime
{
    public class BaggageTrayStation : StationBase
    {
        public List<BaggagePair> BaggagePairs { get; private set; } = new();
        
        [Header("Tray Station"), SerializeField] private Transform baggageParent;

        private const float BAGGAGE_SPACING = 0.625f;
        private const float SCALE_EFFECT_DELAY = 0.05f;

        private PlayerBaggageHandler _baggageHandler;
        
        /// <summary>
        /// Returns null if baggage placement is not finished
        /// </summary>
        public Baggage PopBottomBaggage()
        {
            if (BaggagePairs.Count == 0 || BaggagePairs[0].IsPlaced == false)
                return null;

            var baggagePair = BaggagePairs[0];
            BaggagePairs.RemoveAt(0);
            SortBaggages();
            return baggagePair.Baggage;
        }
        
        protected override void StartStation()
        {
            _baggageHandler = Player.Instance.BaggageHandler;
            base.StartStation();
        }
        
        protected override void StationBehaviour()
        {
            var baggagePair = _baggageHandler.PopBaggage();
            if (baggagePair == null)
                return;

            baggagePair.Holder = baggageParent;
            baggagePair.Baggage.transform.SetParent(baggageParent);
            baggagePair.Baggage.transform.localScale = Vector3.one;
            BaggagePairs.Add(baggagePair);
            
            PlacementTween(baggagePair, BaggagePairs.Count - 1);
        }
        
        private void PlacementTween(BaggagePair pair, int index)
        {
            pair.Baggage.transform.DOComplete();
            pair.Baggage.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.25f).SetEase(Ease.Linear);
            pair.Baggage.transform.DOLocalJump(Vector3.zero + index * BAGGAGE_SPACING * Vector3.up, 2f, 1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                pair.IsPlaced = true;
                ScaleEffect();
                CheckTask();
            });
        }
        
        private void ScaleEffect()
        {
            for (var i = 0; i < BaggagePairs.Count; i++)
            {
                if(!BaggagePairs[i].IsPlaced) continue;

                BaggagePairs[i].Baggage.transform.DOComplete();
                BaggagePairs[i].Baggage.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f).SetEase(Ease.InOutSine).SetDelay(i * SCALE_EFFECT_DELAY);
            }
        }

        private void SortBaggages()
        {
            for (var i = 0; i < BaggagePairs.Count; i++)
            {
                BaggagePairs[i].Baggage.transform.DOComplete();
                BaggagePairs[i].Baggage.transform.DOLocalMoveY(i * BAGGAGE_SPACING, 0.2f).SetDelay(0.2f).SetEase(Ease.OutBack);
            }
        }
        
        private void CheckTask()
        {
            if (_baggageHandler.GetBaggageCount > 0)
                return;
            
            StopProgressing();
            TaskManager.Instance.CompleteTask(this);
        }
    }
}
