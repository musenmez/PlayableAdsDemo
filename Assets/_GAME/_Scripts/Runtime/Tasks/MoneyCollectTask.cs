using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class MoneyCollectTask : TaskBase
    {
        [SerializeField] private TicketControlStationPaymentHandler paymentHandler;

        public override void ActivateTask()
        {
            base.ActivateTask();
            paymentHandler.OnPaymentClaimed.AddListener(CompleteTask);
            
            if(paymentHandler.GetStackCount() == 0)
                CompleteTask();
        }

        public override void DeactivateTask()
        {
            base.DeactivateTask();
            paymentHandler.OnPaymentClaimed.RemoveListener(CompleteTask);
        }

        private void CompleteTask()
        {
            TaskManager.Instance.CompleteTask(this);
        }
    }
}
