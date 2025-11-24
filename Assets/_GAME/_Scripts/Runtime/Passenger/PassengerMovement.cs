using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float rotationSpeed = 30f;
        [Space, SerializeField] private Transform body;

        private const float THRESHOLD = 0.05f;

        public void MoveTowards(Vector3 targetPosition, float deltaTime)
        {
            if (IsReached(targetPosition)) return;
            
            var targetPos = Vector3.MoveTowards(body.position, targetPosition, movementSpeed * deltaTime);
            transform.position = targetPos;
            
            var moveDirection =  (targetPosition - transform.position).normalized;
            var targetRot = Quaternion.LookRotation(moveDirection);
            body.rotation = Quaternion.Slerp(body.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        public bool IsReached(Vector3 targetPosition)
        {
            var distance = Vector3.Distance(targetPosition, body.position);
            return distance < THRESHOLD;
        }
    }
}
