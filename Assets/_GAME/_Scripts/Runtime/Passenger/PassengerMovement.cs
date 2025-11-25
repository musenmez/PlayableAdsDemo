using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

namespace Game.Runtime
{
    public class PassengerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float rotationSpeed = 30f;

        private const float MOVEMENT_THRESHOLD = 0.05f;
        private const float ROTATION_THRESHOLD = 0.001f;

        public void MoveTowards(Vector3 targetPosition, float deltaTime)
        {
            if (IsReached(targetPosition)) return;
            
            var targetPos = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * deltaTime);
            transform.position = targetPos;
            
            var moveDirection =  (targetPosition - transform.position).normalized;
            if (moveDirection.sqrMagnitude < ROTATION_THRESHOLD)
                return;
            
            var targetRot = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        public void FollowPath(Vector3[] points, float duration, Action onComplete = null)
        {
            transform.DOPath(points, duration, PathType.CatmullRom).SetLookAt(0.001f).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        }

        public bool IsReached(Vector3 targetPosition)
        {
            var distance = Vector3.Distance(targetPosition, transform.position);
            return distance < MOVEMENT_THRESHOLD;
        }
    }
}
