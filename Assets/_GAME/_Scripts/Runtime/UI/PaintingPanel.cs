using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PaintingPanel : PanelBase
    {
        [Header("Painting Panel")]
        [SerializeField] private P3dPaintDecal paintDecal;
        [SerializeField] private P3dHitScreen hitScreen;
        [SerializeField] private Slider brushSizeSlider;

        private void Start()
        {
            SetBrushSize(brushSizeSlider.value);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            brushSizeSlider.onValueChanged.AddListener(SetBrushSize);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            brushSizeSlider.onValueChanged.RemoveListener(SetBrushSize);
        }
        
        public void SetBrushColor(Color color)
        {
            paintDecal.Color = color;
        }

        public void CompletePainting()
        {
            hitScreen.enabled = false;
            GameManager.Instance.SetState(GameStateId.Final);
        }
        
        private void SetBrushSize(float size)
        {
            paintDecal.Radius = size;
        }
    }
}
