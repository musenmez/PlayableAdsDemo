using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Game.Runtime
{
    public abstract class DepositAreaBase : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; protected set; }
        public bool IsCompleted { get; protected set; }

        [SerializeField] protected int cost;
        
        [Space, SerializeField] protected Transform body;
        [SerializeField] protected CanvasGroup canvasGroup;
        
        [Space, SerializeField] protected Image fillImage;
        [SerializeField] protected TextMeshProUGUI costText;

        protected const Ease SCALE_EASE = Ease.Linear;
        protected readonly WaitForSeconds DepositDelay = new WaitForSeconds(0.02f);
        
        protected int _remainingCost;
        protected Tween _scaleTween;
        protected Coroutine _depositCo = null;

        protected void Awake()
        {
            _remainingCost = cost;
            SetCostText();
        }

        public void Interact(Interactor interactor)
        {
            if(_depositCo != null)
                StopCoroutine(_depositCo);

            _depositCo = StartCoroutine(DepositCo());
        }

        public void InteractorExit(Interactor interactor)
        {
            if(_depositCo != null)
                StopCoroutine(_depositCo);
            
            TryUnlock();
        }

        protected virtual void Unlock()
        {
            if (IsCompleted) return;
            
            if(_depositCo != null)
                StopCoroutine(_depositCo);
            
            IsCompleted = true;
            IsInteractable = false;
            fillImage.fillAmount = 1;
            ScaleTween(0, 0.2f);
        }

        protected virtual IEnumerator DepositCo()
        {
            while (true)
            {
                if (!CurrencyManager.Instance.IsAffordable(1) || _remainingCost <= 0)
                    break;
                
                CurrencyManager.Instance.SubtractCurrency(1);
                _remainingCost--;
                SetCostText();
                SetFillImage();
                TryUnlock();
                yield return DepositDelay;
            }
        }

        private void TryUnlock()
        {
            if (_remainingCost > 0)
                return;

            Unlock();
        }

        private void SetCostText()
        {
            costText.SetText(_remainingCost.ToString());
        }

        private void SetFillImage()
        {
            var fillAmount = 1f - (float)_remainingCost / cost;
            fillImage.fillAmount = fillAmount;
        }
        
        private void ScaleTween(float scale, float duration)
        {
            _scaleTween.Kill();
            _scaleTween = body.DOScale(scale, duration).SetEase(SCALE_EASE);
        }
    }
}
