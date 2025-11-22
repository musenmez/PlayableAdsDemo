using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Runtime
{
    public class Escalator : MonoBehaviour
    {
        [field : SerializeField] public EscalatorDirection Direction { get; private set; }
        [SerializeField] private Transform topPoint;
        [SerializeField] private Transform bottomPoint;

        private const int INITIAL_STEP_COUNT = 15;
        private const float STEP_SPEED = 5f;
        private readonly WaitForSeconds SpawnDelay = new WaitForSeconds(0.12f);
        
        private List<PoolableItem> _steps = new();
        private Coroutine _stepSpawnCo;

        private void Start()
        {
            InitializeSteps();
            _stepSpawnCo = StartCoroutine(StepSpawnCo());
        }
        
        [Button]
        private void InitializeSteps()
        {
            var spacing = 1f / INITIAL_STEP_COUNT;
            var start = Direction == EscalatorDirection.Up ? bottomPoint.position : topPoint.position;
            var end = Direction == EscalatorDirection.Up ? topPoint.position : bottomPoint.position;
            
            for (var i = 0; i < INITIAL_STEP_COUNT; i++)
            {
                var spawnPosition = Vector3.Lerp(start, end, spacing * i);
                var step = SpawnStep(spawnPosition);
                StepMovement(step);
            }
        }

        private IEnumerator StepSpawnCo()
        {
            yield return SpawnDelay;

            while (true)
            {
                var spawnPosition = Direction == EscalatorDirection.Up ? bottomPoint.position : topPoint.position;
                var step = SpawnStep(spawnPosition);
                StepMovement(step);
                yield return SpawnDelay;
            }
        }

        private PoolableItem SpawnStep(Vector3 spawnPosition)
        {
            var step = PoolingManager.Instance.GetInstance(PoolId.EscalatorStep, spawnPosition, Quaternion.identity);
            step.transform.SetParent(topPoint.parent);
            step.transform.localScale = Vector3.one;
            return step;
        }

        private void StepMovement(PoolableItem step)
        {
            var targetPosition = Direction == EscalatorDirection.Up ? topPoint.localPosition : bottomPoint.localPosition;
            step.transform.DOKill();
            step.transform.DOLocalMove(targetPosition, STEP_SPEED).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(step.Dispose);
        }
    }
}
