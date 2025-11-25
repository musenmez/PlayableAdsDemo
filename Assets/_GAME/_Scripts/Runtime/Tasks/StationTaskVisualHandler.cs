using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class StationTaskVisualHandler : MonoBehaviour
    {
        private StationBase _station;
        private StationBase Station => _station == null ? _station = GetComponentInParent<StationBase>() : _station;

        [SerializeField] private Transform scaleBody;
        [SerializeField] private SpriteRenderer circleVisual;
        
        private void OnEnable()
        {
            Station.OnTaskActivated.AddListener(ActivateVisual);
            Station.OnTaskDeactivated.AddListener(DeactivateVisual);
        }

        private void OnDisable()
        {
            Station.OnTaskActivated.RemoveListener(ActivateVisual);
            Station.OnTaskDeactivated.RemoveListener(DeactivateVisual);
        }

        private void ActivateVisual()
        {
            ScaleTween(1.2f, 0.2f);
            ColorTween(Color.green, 0.2f);
        }

        private void DeactivateVisual()
        {
            ScaleTween(1f, 0.2f);
            ColorTween(Color.white, 0.2f);
        }

        private void ScaleTween(float endValue, float duration)
        {
            scaleBody.DOKill();
            scaleBody.DOScale(endValue, duration).SetEase(Ease.Linear);
        }

        private void ColorTween(Color color, float duration)
        {
            circleVisual.DOKill();
            circleVisual.DOColor(color, duration).SetEase(Ease.Linear);
        }
    }
}
