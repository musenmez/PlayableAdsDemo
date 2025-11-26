using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using TMPro;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PaintingPanel : PanelBase
    {
        [Header("Painting Panel")]
        [SerializeField] private P3dPaintDecal paintDecal;
        [SerializeField] private Slider brushSizeSlider;

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
            GameManager.Instance.SetState(GameStateId.Final);
        }
        
        private void SetBrushSize(float size)
        {
            paintDecal.Radius = size;
        }
    }
}
