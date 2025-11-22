using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class SecondFloorController : MonoBehaviour
    {
        public static SecondFloorController Instance = null;
        
        [SerializeField] private List<PropRevealConfig> props = new();

        private void Awake()
        {
            Instance = this;
            DisableAllProps();
        }

        public void RevealProps()
        {
            foreach (var prop in props)
            {
                RevealProp(prop);
            }
        }

        public float GetTotalRevealDuration()
        {
            var maxDuration = 0f;
            foreach (var prop in props)
            {
                var duration = prop.Delay + prop.Duration;
                if (duration > maxDuration)
                {
                    maxDuration = duration;
                }
            }
            return maxDuration;
        }

        private void RevealProp(PropRevealConfig prop)
        {
            prop.Body.SetActive(true);
            prop.Body.transform.DOKill();
            prop.Body.transform.DOScale(Vector3.one, prop.Duration).SetDelay(prop.Delay).SetEase(prop.EaseType);
        }

        private void DisableAllProps()
        {
            foreach (var prop in props)
            {
                prop.Body.transform.localScale = Vector3.zero;
                prop.Body.SetActive(false);
            }
        }
    }
}
