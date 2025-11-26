using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Runtime
{
    public class PlayerAnimator : MonoBehaviour
    {
        public const int CARRY_LAYER = 1;

        private const float DEFAULT_WALK_SPEED = 1f;
        private const float CARRY_WALK_SPEED = 0.5f;
        private const float CARRY_SPEED_THRESHOLD = 0.2f;

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
            CheckWalkSpeed(layerIndex, weight);
        }

        private void SetFloat(int hash, float value)
        {
            animator.SetFloat(hash, value);
        }

        private void CheckWalkSpeed(int layerIndex, float weight)
        {
            if (layerIndex != CARRY_LAYER)
                return;
            
            SetFloat(AnimationHashes.WalkSpeedMultiplier, weight > CARRY_SPEED_THRESHOLD ? CARRY_WALK_SPEED : DEFAULT_WALK_SPEED);
        }

        private void SetSpeed()
        {
            var input = new Vector3(UIManager.Instance.Joystick.Horizontal, 0f, UIManager.Instance.Joystick.Vertical);
            animator.SetFloat(AnimationHashes.Speed, Player.Instance.Movement.IsEnabled ? input.sqrMagnitude : 0);
        }
    }
}
