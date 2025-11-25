using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Runtime
{
    public class PlayerAnimator : MonoBehaviour
    {
        public const int CARRY_LAYER = 1;

        [SerializeField] private Animator animator;
        
        private Tween _layerTween;

        private void Update()
        {
            SetSpeed();
        }
        
        public void SetLayerWeight(int layerIndex, float weight, float duration = 0f)
        {
            _layerTween.Kill();
            _layerTween = DOVirtual.Float(animator.GetLayerWeight(layerIndex), weight, duration: duration, (x) => animator.SetLayerWeight(layerIndex, x));
        }


        private void SetSpeed()
        {
            var input = new Vector3(UIManager.Instance.Joystick.Horizontal, 0f, UIManager.Instance.Joystick.Vertical);
            animator.SetFloat(AnimationHashes.Speed, Player.Instance.Movement.IsEnabled ? input.sqrMagnitude : 0);
        }
    }
}
