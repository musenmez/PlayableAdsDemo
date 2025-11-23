using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Game.Runtime
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsEnabled { get; private set; }

        [Header("Movement Parameters")] 
        [SerializeField] private float movementSpeed = 15f;
        [SerializeField] private float rotationSpeed = 75f;
        
        [Header("Components")]
        [SerializeField] private Transform body;
        [SerializeField] private CharacterController characterController;

        private const float MOVEMENT_THRESHOLD = 0.01f;
        private const float MOVEMENT_ACCELERATION = 10f;
        private const float ROTATION_ACCELERATION = 10f;
        
        private float _currentMovementSpeed;
        private float _currentRotationSpeed;
        
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
            {
                ResetSpeeds();
                return;
            }
           
            var input = new Vector3(UIManager.Instance.Joystick.Horizontal, 0f, UIManager.Instance.Joystick.Vertical);
            if (input.sqrMagnitude < MOVEMENT_THRESHOLD)
            {
                ResetSpeeds();
                return;
            }
            
            Acceleration();
            var moveDirection =  _currentMovementSpeed * input.normalized;
            characterController.SimpleMove(moveDirection);
            
            var targetRot = Quaternion.LookRotation(moveDirection);
            body.rotation = Quaternion.Slerp(body.rotation, targetRot, _currentRotationSpeed * Time.deltaTime);
        }

        private void Acceleration()
        {
            _currentMovementSpeed = Mathf.Lerp(_currentMovementSpeed, movementSpeed, Time.deltaTime * MOVEMENT_ACCELERATION);
            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, rotationSpeed, Time.deltaTime * ROTATION_ACCELERATION);
        }

        private void ResetSpeeds()
        {
            _currentMovementSpeed = 0;
            _currentRotationSpeed = 0;
        }
    }
}
