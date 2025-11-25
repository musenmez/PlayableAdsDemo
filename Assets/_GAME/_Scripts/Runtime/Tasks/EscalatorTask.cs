using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class EscalatorTask : TaskBase
    {
        [Space, SerializeField] private FloorType targetFloor;
        [SerializeField] private Transform indicatorBody;
        [SerializeField] private SpriteRenderer indicatorRenderer;
        [SerializeField] private List<Collider> triggers = new();

        private void Awake()
        {
            SetTriggers(false);
        }

        private void OnEnable()
        {
            if (Managers.Instance == null) return;
            
            EscalatorManager.Instance.OnPlayerFloorChanged.AddListener(CheckFloor);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null) return;
            
            EscalatorManager.Instance.OnPlayerFloorChanged.RemoveListener(CheckFloor);
        }

        public override void ActivateTask()
        {
            base.ActivateTask();
            SetTriggers(true);
            ScaleTween(1.2f, 0.2f);
            ColorTween(Color.green, 0.2f);
        }

        public override void DeactivateTask()
        {
            base.DeactivateTask();
            ScaleTween(1f, 0.2f);
            ColorTween(Color.white, 0.2f);
        }

        private void CheckFloor(FloorType floorType)
        {
            if (TaskManager.Instance.CurrentTask == null) 
                return;
            
            if (TaskManager.Instance.CurrentTask == this)
            {
                CompleteTask(floorType);
                return;
            }
            
            if (TaskManager.Instance.CurrentTask.TaskFloorType == floorType) 
                return;
            
            TaskManager.Instance.InsertTask(this);
        }
        
        private void CompleteTask(FloorType floorType)
        {
            if (targetFloor != floorType)
                return;
            
            TaskManager.Instance.CompleteTask(this);
        }
        
        private void ScaleTween(float endValue, float duration)
        {
            indicatorBody.DOKill();
            indicatorBody.DOScale(endValue, duration).SetEase(Ease.Linear);
        }

        private void ColorTween(Color color, float duration)
        {
            indicatorRenderer.DOKill();
            indicatorRenderer.DOColor(color, duration).SetEase(Ease.Linear);
        }

        private void SetTriggers(bool isEnabled)
        {
            foreach (var trigger in triggers)
            {
                trigger.enabled = isEnabled;
            }
        }
    }
}
