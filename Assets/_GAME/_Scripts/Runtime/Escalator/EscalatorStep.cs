using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class EscalatorStep : PoolableItem
    {
        public Escalator Escalator  { get; private set; }
        public bool IsOccupied { get; private set; }

        [Header("Escalator Step"), SerializeField] private Transform passengerPoint;

        private IEscalatorPassenger _passenger;
        private Transform _defaultParent;

        public void Initialize(Escalator escalator)
        {
            Escalator = escalator;
        }

        public void SetPassenger(IEscalatorPassenger passenger)
        {
            if (IsOccupied) return;
            
            IsOccupied = true;
            _passenger = passenger;
            _defaultParent = passenger.TargetTransform.parent;
            _passenger.TargetTransform.SetParent(passengerPoint.parent);
            _passenger.EnterEscalator(this);
        }

        public void CompleteTravel()
        {
            if (_passenger is not null)
            {
                _passenger.ExitEscalator();
                _passenger.TargetTransform.SetParent(_defaultParent);
            }
            IsOccupied = false;
            Escalator = null;
            _passenger = null;
            Dispose();
        }
    }
}
