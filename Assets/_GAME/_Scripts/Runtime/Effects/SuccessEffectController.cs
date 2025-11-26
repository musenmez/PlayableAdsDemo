using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class SuccessEffectController : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> effects = new();

        private void OnEnable()
        {
            if (Managers.Instance == null) return;
            
            GameManager.Instance.OnLevelCompleted.AddListener(PlaySuccessEffect);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null) return;
            
            GameManager.Instance.OnLevelCompleted.RemoveListener(PlaySuccessEffect);
        }

        private void PlaySuccessEffect()
        {
            foreach (var effect in effects)
            {
                effect.Play();
            }   
        }
    }
}
