using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class Money : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; private set; }

        private int _value;
        private IMoneyStack _stack;
        
        public void Initialize(int value, IMoneyStack stack = null)
        {
            IsInteractable = true;
            _value = value;
            _stack = stack;
        }

        public void Interact(Interactor interactor)
        {
            IsInteractable = false;
            _stack?.RemoveFromStack(this);
            CollectTween();
        }

        private void CollectTween()
        {
            CurrencyManager.Instance.AddCurrency(_value);
            gameObject.SetActive(false);
        }

        public void InteractorExit(Interactor interactor){}
    }
}
