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

        public override void EnterEscalator(EscalatorStep step)
        {
            base.EnterEscalator(step);
            movement.DisableMovement();
        }

        public override void ExitEscalator()
        {
            base.ExitEscalator();
            movement.EnableMovement();
        }
    }
}
