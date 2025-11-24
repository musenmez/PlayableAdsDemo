using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class PassengerSkinController : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer skinRenderer;
        [SerializeField] private PassengerColorDataSO skinColorData;

        private MaterialPropertyBlock _propertyBlock;
        
        public void SetRandomColor()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _propertyBlock.SetColor("_Color", skinColorData.GetRandomSkinColor());
        }
    }
}
