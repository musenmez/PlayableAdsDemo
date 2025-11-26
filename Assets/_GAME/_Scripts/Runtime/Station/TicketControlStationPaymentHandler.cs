using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class TicketControlStationPaymentHandler : MonoBehaviour, IMoneyStack, IInteractable
    {
        public bool IsInteractable { get; } = true;
        public UnityEvent OnPaymentClaimed { get; } = new();

        [SerializeField] private int stackRow = 2;
        [SerializeField] private int stackColumn = 3;
        [SerializeField] private Vector3 stackSpacing;
        [SerializeField] private Transform stackHolder;
        
        private const int TICKET_PRICE = 20;
        private const int INSTANCE_COUNT = 2;

        private const float MONEY_SPAWN_OFFSET = 2f;
        private readonly Vector3 MONEY_ROTATION = new Vector3(0f, 35f, 0f);
        private readonly WaitForSeconds ClaimDelay = new WaitForSeconds(0.01f);

        private Coroutine _claimCo;
        private readonly List<Money> _monies = new();
        
        public void ReceivePayment(Passenger passenger)
        {   
            var valuePerItem = TICKET_PRICE / INSTANCE_COUNT;
            var remainder = TICKET_PRICE % INSTANCE_COUNT;

            for (var i = 0; i < INSTANCE_COUNT; i++)
            {
                var money = SpawnMoney(passenger);
                var value = valuePerItem;
                if (i == INSTANCE_COUNT - 1) value += remainder;
                MoneyAnimation(money, value, _monies.Count - 1);
            }
        }
        
        public void Interact(Interactor interactor)
        {
            if (!IsInteractable) 
                return;
            
            ClaimAll(interactor);
        }

        public void RemoveFromStack(Money money)
        {
            if (!_monies.Contains(money))
                return;
            
            _monies.Remove(money);
        }
        
        public int GetStackCount() => _monies.Count;

        private void ClaimAll(Interactor interactor)
        {
            if (_monies.Count == 0)
                return;
            
            if(_claimCo != null)
                StopCoroutine(_claimCo);

            _claimCo = StartCoroutine(ClaimCo(interactor));
        }

        private IEnumerator ClaimCo(Interactor interactor)
        {
            var monies = new List<Money>(_monies);
            foreach (var money in monies)
            {
                money.Collect(interactor.transform);
                yield return ClaimDelay;
            }
            OnPaymentClaimed.Invoke();
        }

        private void MoneyAnimation(Money money, int value, int index)
        {
            var seq = DOTween.Sequence();
            seq.Join(money.transform.DOScale(1, 0.2f).SetEase(Ease.OutBounce))
                .Join(money.transform.DOLocalRotate(MONEY_ROTATION, 0.2f).SetEase(Ease.Linear))
                .Join(money.transform.DOLocalJump(GetStackPosition(index), 2f, 1, 0.25f).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    money.Initialize(value, this);
                });
        }

        private Money SpawnMoney(Passenger passenger)
        {
            var money = PoolingManager.Instance.GetInstance(PoolId.Money).GetPoolComponent<Money>();
            money.transform.DOKill();
            money.transform.position = passenger.transform.position + Vector3.up * MONEY_SPAWN_OFFSET;
            money.transform.localScale = Vector3.zero;
            money.transform.SetParent(stackHolder);
            _monies.Add(money);
            return money;
        }
        
        private Vector3 GetStackPosition(int index)
        {
            var perFloor = stackRow * stackColumn;

            var floor = index / perFloor;
            var cellIndex = index % perFloor;

            var x = cellIndex / stackRow; 
            var z = cellIndex % stackRow;

            return new Vector3(
                x * stackSpacing.x,
                floor * stackSpacing.y,
                z * stackSpacing.z
            );
        }

        public void InteractorExit(Interactor interactor){}
    }
}
