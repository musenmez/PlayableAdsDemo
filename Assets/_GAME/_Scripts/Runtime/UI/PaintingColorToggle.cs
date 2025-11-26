using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PaintingColorToggle : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Transform body;
        [SerializeField] private PaintingPanel paintingPanel;

        private const float DEFAULT_SCALE = 1f;
        private const float SELECTED_SCALE = 1.2f;
        
        private Tweener _scaleTween;

        private void Awake()
        {
            CheckToggleStatus(toggle.isOn);
        }

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(CheckToggleStatus);
        }

        private void OnDisable()
        {
          toggle.onValueChanged.RemoveListener(CheckToggleStatus);
        }

        private void CheckToggleStatus(bool isSelected)
        {
            if(isSelected) Select();
            else Deselect();
        }

        private void Select()
        {
            ScaleTween(SELECTED_SCALE, 0.1f);
            paintingPanel.SetBrushColor(color);
        }

        private void Deselect()
        {
            ScaleTween(DEFAULT_SCALE, 0.1f);
        }

        private void ScaleTween(float endValue, float duration)
        {
            _scaleTween.Kill();
            _scaleTween = body.DOScale(endValue, duration).SetEase(Ease.Linear);
        }
    }
}
