using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class Baggage : MonoBehaviour
    {
        [SerializeField] private ColorDataSO colorData;
        [SerializeField] private MeshRenderer baggageRenderer;
        
        private MaterialPropertyBlock _propertyBlock;

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }
        
        private void OnEnable()
        {
            SetRandomColor();
        }

        private void SetRandomColor()
        {
            _propertyBlock.SetColor("_Color", colorData.GetRandomSkinColor());
            baggageRenderer.SetPropertyBlock(_propertyBlock);
        }
    }
}
