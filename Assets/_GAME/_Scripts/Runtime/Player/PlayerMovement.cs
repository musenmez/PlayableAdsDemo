using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Game.Runtime
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsEnabled { get; private set; } = true;

        [Header("Movement Parameters")] 
        [SerializeField] private float movementSpeed = 15f;
        [SerializeField] private float rotationSpeed = 75f;
        
        [Header("Components")]
        [SerializeField] private Transform body;
        [SerializeField] private CharacterController characterController;

        private const float MOVEMENT_THRESHOLD = 0.01f;
        
        private void Update()
        {
            Movement();
        }
        
        public void EnableMovement()
        {
            IsEnabled = true;
        }

        public void DisableMovement()
        {
            IsEnabled = false;
        }

        private void Movement()
        {
            if (!IsEnabled)
                return;
           
            var input = new Vector3(UIManager.Instance.Joystick.Horizontal, 0f, UIManager.Instance.Joystick.Vertical);
            if (input.sqrMagnitude < MOVEMENT_THRESHOLD)
                return;

            var moveDirection =  movementSpeed * input.normalized;
            characterController.SimpleMove(moveDirection);
            
            var targetRot = Quaternion.LookRotation(moveDirection);
            body.rotation = Quaternion.Slerp(body.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }
}
