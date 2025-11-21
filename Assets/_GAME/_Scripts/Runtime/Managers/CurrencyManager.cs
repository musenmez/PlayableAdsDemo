using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        public int CurrencyAmount { get; private set; }
        public UnityEvent OnCurrencyAmountChanged { get; } = new();

        [SerializeField] private CurrencyDataSO currencyData;

        private void Awake()
        {
            CurrencyAmount = currencyData.InitialCurrency;
        }

        public void AddCurrency(int amount)
        {
            CurrencyAmount += amount;
            OnCurrencyAmountChanged.Invoke();
        }

        public void SubtractCurrency(int amount)
        {
            CurrencyAmount -= amount;
            CurrencyAmount = Mathf.Max(CurrencyAmount, 0);
            OnCurrencyAmountChanged.Invoke();
        }

        public bool IsAffordable(int amount) => CurrencyAmount >= amount;
    }
}
