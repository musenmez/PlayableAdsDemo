using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Update()
        {
            SetSpeed();
        }

        private void SetSpeed()
        {
            var input = new Vector3(UIManager.Instance.Joystick.Horizontal, 0f, UIManager.Instance.Joystick.Vertical);
            animator.SetFloat(AnimationHashes.Speed, Player.Instance.Movement.IsEnabled ? input.sqrMagnitude : 0);
        }
    }
}
