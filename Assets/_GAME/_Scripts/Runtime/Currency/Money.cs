using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class Money : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; private set; }

        private const float COLLECT_OFFSET = 2f;

        private int _value;
        private IMoneyStack _stack;
        
        public void Initialize(int value, IMoneyStack stack = null)
        {
            IsInteractable = true;
            _value = value;
            _stack = stack;
            transform.DOPunchScale(Vector3.one * 0.3f, 0.2f).SetEase(Ease.InOutSine);
        }

        public void Interact(Interactor interactor)
        {
            if (!IsInteractable)
                return;
            
            IsInteractable = false;
            _stack?.RemoveFromStack(this);
            CollectTween(interactor.transform);
        }

        private void CollectTween(Transform target)
        {
            transform.DOComplete();
            transform.SetParent(target);
            var sequence = DOTween.Sequence();
            sequence.Join(transform.DOScale(0.75f, 0.2f).SetEase(Ease.Linear))
                .Join(transform.DOLocalRotateQuaternion(Random.rotation, 0.2f).SetEase(Ease.Linear))
                .Join(transform.DOLocalJump(Vector3.up * COLLECT_OFFSET, 2f, 1, 0.2f).SetEase(Ease.Linear))
                .OnComplete(() => 
                {
                    transform.SetParent(null);
                    gameObject.SetActive(false);
                    CurrencyManager.Instance.AddCurrency(_value);
                });
        }

        public void InteractorExit(Interactor interactor){}
    }
}
