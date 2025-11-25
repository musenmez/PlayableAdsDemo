using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class BaggageLoadingStation : StationBase
    {
        [Header("Baggage Loading Station")]
        [SerializeField] private Transform trayEndPoint;
        [SerializeField] private Truck truck;

        [Header("Platform Settings")] 
        [SerializeField] private float defaultHeight;
        [SerializeField] private float maxHeight;
        
        [Space, SerializeField] private Transform platformMovementBody;
        [SerializeField] private Transform platformBaggageHolder;
        
        private readonly WaitForSeconds DELAY = new WaitForSeconds(0.6f);
        private Coroutine _progressCo;
        private BaggageTrayStation _trayStation;
        
        protected override void StartStation()
        {
            _trayStation = StationManager.Instance.GetStation(StationId.BaggageTray) as BaggageTrayStation;
            StopProgressing();
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
                LoadTruck();
                yield return DELAY;
            }
        }

        private void LoadTruck()
        {
            if (!truck.IsAvailable) return;

            var baggage= _trayStation.PopBottomBaggage();
            if (baggage is null) return;
            
            LoadingTween(baggage);
            CheckTask();
        }

        private void LoadingTween(Baggage baggage)
        {
            baggage.transform.DOKill();
            var loadingSeq = DOTween.Sequence();

            loadingSeq.Append(baggage.transform.DOMove(trayEndPoint.position, 0.15f).SetEase(Ease.Linear))
                .AppendCallback(() => baggage.transform.SetParent(platformBaggageHolder))
                .Append(baggage.transform.DOLocalJump(Vector3.zero, 2f, 1, 0.25f).SetEase(Ease.Linear))
                .Join(baggage.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.25f).SetEase(Ease.Linear))
                .Append(platformMovementBody.DOMoveY(maxHeight, 0.2f).SetEase(Ease.OutBack))
                .JoinCallback(() => truck.AddBaggage(baggage)).SetDelay(0.15f)
                .Append(platformMovementBody.DOMoveY(defaultHeight, 0.2f).SetEase(Ease.InOutSine));
        }
        
        private void CheckTask()
        {
            if (_trayStation.BaggagePairs.Count > 0)
                return;
            
            TaskManager.Instance.CompleteTask(this);
        }
        
        private void StopProgressing()
        {
            if(_progressCo != null)
                StopCoroutine(_progressCo);
        }
    }
}
