using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerEscalatorHandler : EscalatorPassengerBase
    {
        public override Transform RotationBody => body;

        [SerializeField] private Transform body;
        [SerializeField] private PlayerMovement movement;
        
        private Escalator _lastEscalator;
        
        public override void EnterEscalator(EscalatorStep step)
        {
            base.EnterEscalator(step);
            movement.DisableMovement();
            _lastEscalator = step.Escalator;
        }

        public override void ExitEscalator()
        {
            base.ExitEscalator();
            movement.EnableMovement();
            
            var floorType = _lastEscalator.Direction == EscalatorDirection.Up ? FloorType.Second : FloorType.First;
            Player.Instance.SetFloor(floorType);
        }
    }
}
