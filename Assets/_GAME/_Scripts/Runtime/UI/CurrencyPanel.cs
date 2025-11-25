using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class CurrencyPanel : PanelBase
    {
        [Header("Currency Panel")]
        [SerializeField] private TextMeshProUGUI currencyText;

        private void Start()
        {
            SetCurrencyText();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Managers.Instance == null) return;
            CurrencyManager.Instance.OnCurrencyAmountChanged.AddListener(SetCurrencyText);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (Managers.Instance == null) return;
            CurrencyManager.Instance.OnCurrencyAmountChanged.RemoveListener(SetCurrencyText);
        }

        private void SetCurrencyText()
        {
            currencyText.SetText(CurrencyManager.Instance.CurrencyAmount.ToString());
        }
    }
}
