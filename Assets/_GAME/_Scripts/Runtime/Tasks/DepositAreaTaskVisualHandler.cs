using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class DepositAreaTaskVisualHandler : MonoBehaviour
    {
        private DepositAreaBase _depositAreaBase;
        private DepositAreaBase DepositArea => _depositAreaBase == null ? _depositAreaBase = GetComponentInParent<DepositAreaBase>() : _depositAreaBase;

        [SerializeField] private GameObject arrow;
        
        private void OnEnable()
        {
            DepositArea.OnTaskActivated.AddListener(ActivateVisual);
            DepositArea.OnTaskDeactivated.AddListener(DeactivateVisual);
        }

        private void OnDisable()
        {
            DepositArea.OnTaskActivated.RemoveListener(ActivateVisual);
            DepositArea.OnTaskDeactivated.RemoveListener(DeactivateVisual);
        }

        private void ActivateVisual()
        {
            arrow.SetActive(true);
        }

        private void DeactivateVisual()
        {
            arrow.SetActive(false);
        }
    }
}
