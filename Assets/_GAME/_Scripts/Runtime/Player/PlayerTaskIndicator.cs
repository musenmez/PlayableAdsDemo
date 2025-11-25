using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerTaskIndicator : MonoBehaviour
    {
        [SerializeField] private Transform indicatorBody;
        [SerializeField] private GameObject indicatorVisual;
        [SerializeField] private PlayerEscalatorHandler escalatorHandler;

        private const float OFFSET = 3f;
        
        private void Update()
        {
            SetIndicator();
        }

        private void SetIndicator()
        {
            if (TaskManager.Instance.CurrentTask == null 
                || TaskManager.Instance.IsAllTaskCompleted
                || escalatorHandler.IsUsingEscalator)
            {
                indicatorVisual.SetActive(false);
                return;
            }

            var targetPos = TaskManager.Instance.CurrentTask.IndicatorTarget.position;
            targetPos.y = transform.position.y;
            
            var direction =  (targetPos - transform.position).normalized;
            indicatorBody.rotation = Quaternion.LookRotation(direction);
            
            var distance = Vector3.Distance(transform.position, targetPos);
            indicatorVisual.SetActive(distance > OFFSET);
        }
    }
}
