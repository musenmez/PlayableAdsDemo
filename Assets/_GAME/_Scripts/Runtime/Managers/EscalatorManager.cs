using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public class EscalatorManager : Singleton<EscalatorManager>
    {
        public UnityEvent<FloorType> OnPlayerFloorChanged { get; } = new();
        
        [SerializeField] private List<Escalator> escalators = new();
        
        private readonly Dictionary<EscalatorDirection, Escalator> _escalatorsByDirection = new();
        
        private void Awake()
        {
            SetCollection();
        }

        public Escalator GetEscalator(EscalatorDirection direction)
        {
            _escalatorsByDirection.TryGetValue(direction, out var path);
            return path;
        }

        private void SetCollection()
        {
            foreach (var escalator in escalators)
            {
                _escalatorsByDirection.TryAdd(escalator.Direction, escalator);
            }
        }
    }
}
