using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class Escalator : MonoBehaviour
    {
        [field : SerializeField] public EscalatorDirection Direction { get; private set; }
        [field : SerializeField] public Transform TopPoint { get; private set; }
        [field : SerializeField] public Transform BottomPoint { get; private set; }
        
        [Header("Triggers")]
        [SerializeField] private GameObject topTrigger;
        [SerializeField] private GameObject bottomTrigger;
        
        [Header("Indicators")]
        [SerializeField] private GameObject topIndicator;
        [SerializeField] private GameObject bottomIndicator;
        
        [field : Header("Ground Options"), SerializeField] public float BottomFloorHeight { get; private set; }
        [field : SerializeField] public float TopFloorHeight { get; private set; }

        private const int INITIAL_STEP_COUNT = 15;
        private const float STEP_SPEED = 5f;
        private readonly WaitForSeconds SpawnDelay = new WaitForSeconds(0.12f);

        private Queue<IEscalatorPassenger> _passengers = new();
        private Coroutine _stepSpawnCo;

        private void Start()
        {
            SetTriggers();
            SetIndicators();
            InitializeSteps();
            _stepSpawnCo = StartCoroutine(StepSpawnCo());
        }

        public void TryAddPassenger(IEscalatorPassenger passenger)
        {
            if (passenger.IsUsingEscalator)
                return;
            
            _passengers.Enqueue(passenger);
        }
        
        private void InitializeSteps()
        {
            var spacing = 1f / INITIAL_STEP_COUNT;
            var start = Direction == EscalatorDirection.Up ? BottomPoint.position : TopPoint.position;
            var end = Direction == EscalatorDirection.Up ? TopPoint.position : BottomPoint.position;
            
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
                var spawnPosition = Direction == EscalatorDirection.Up ? BottomPoint.position : TopPoint.position;
                var step = SpawnStep(spawnPosition);
                StepMovement(step);
                CheckPassengers(step);
                yield return SpawnDelay;
            }
        }

        private EscalatorStep SpawnStep(Vector3 spawnPosition)
        {
            var step = PoolingManager.Instance.GetInstance(PoolId.EscalatorStep, spawnPosition, Quaternion.identity).GetPoolComponent<EscalatorStep>();
            step.transform.SetParent(TopPoint.parent);
            step.transform.localScale = Vector3.one;
            step.Initialize(this);
            return step;
        }

        private void StepMovement(EscalatorStep step)
        {
            var targetPosition = Direction == EscalatorDirection.Up ? TopPoint.localPosition : BottomPoint.localPosition;
            step.transform.DOKill();
            step.transform.DOLocalMove(targetPosition, STEP_SPEED).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(step.CompleteTravel);
        }
        
        private void CheckPassengers(EscalatorStep step)
        {
            if (_passengers.Count == 0) return;

            while (_passengers.Count > 0)
            {
                if (!_passengers.TryDequeue(out var passenger) || passenger.IsUsingEscalator)
                    return;
                
                step.SetPassenger(passenger);
                break;
            }
        }
        
        private void SetTriggers()
        {
            bottomTrigger.SetActive(Direction == EscalatorDirection.Up);
            topTrigger.SetActive(Direction == EscalatorDirection.Down);
        }

        private void SetIndicators()
        {
            topIndicator.SetActive(Direction == EscalatorDirection.Up);
            bottomIndicator.SetActive(Direction == EscalatorDirection.Down);
        }
    }
}
