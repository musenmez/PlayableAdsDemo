using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Runtime
{
    public abstract class DepositAreaBase : TaskBase, IInteractable
    {
        public bool IsInteractable { get; protected set; }
        public bool IsCompleted { get; protected set; }

        [SerializeField] protected int cost;
        
        [Space, SerializeField] protected Transform body;
        [SerializeField] protected CanvasGroup canvasGroup;
        
        [Space, SerializeField] protected Image fillImage;
        [SerializeField] protected TextMeshProUGUI costText;

        protected const Ease SCALE_EASE = Ease.Linear;
        protected const float MIN_ALPHA = 0.5f;
        protected readonly WaitForSeconds DepositDelay = new WaitForSeconds(0.02f);
        
        protected int _remainingCost;
        protected Tween _scaleTween;
        protected Coroutine _depositCo = null;

        protected virtual void Awake()
        {
            _remainingCost = cost;
            SetStatus();
        }

        protected virtual void OnEnable()
        {
            if (Managers.Instance == null) return;
            CurrencyManager.Instance.OnCurrencyAmountChanged.AddListener(SetStatus);
        }

        protected virtual void OnDisable()
        {
            if (Managers.Instance == null) return;
            CurrencyManager.Instance.OnCurrencyAmountChanged.RemoveListener(SetStatus);
        }
        
        public void Interact(Interactor interactor)
        {
            if (!IsInteractable) return;
            
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

        public override void ActivateTask()
        {
            IsInteractable = true;
            base.ActivateTask();
        }

        public override void DeactivateTask()
        {
            IsInteractable = false;
           base.DeactivateTask();
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
            TaskManager.Instance.CompleteTask(this);
        }

        protected virtual IEnumerator DepositCo()
        {
            while (true)
            {
                if (!CurrencyManager.Instance.IsAffordable(1) || _remainingCost <= 0)
                    break;
                
                _remainingCost--;
                SetCostText();
                SetFillImage();
                TryUnlock();
                CurrencyManager.Instance.SubtractCurrency(1);
                yield return DepositDelay;
            }
        }

        private void TryUnlock()
        {
            if (_remainingCost > 0)
                return;

            Unlock();
        }

        private void SetStatus()
        {
            SetCostText();
            SetCanvasAlpha();
        }

        private void SetCanvasAlpha()
        {
            if (IsCompleted)
                return;
            
            var alpha = CurrencyManager.Instance.IsAffordable(_remainingCost) ? 1 : MIN_ALPHA;
            canvasGroup.DOKill();
            canvasGroup.DOFade(alpha, 0.25f);
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
