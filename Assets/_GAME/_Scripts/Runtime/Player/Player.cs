using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        public FloorType CurrentFloor { get; private set; } = FloorType.First;
        
        [field : SerializeField] public PlayerAnimator Animator { get; private set; }
        [field : SerializeField] public PlayerMovement Movement { get; private set; }
        [field : SerializeField] public PlayerBaggageHandler BaggageHandler { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SetFloor(FloorType floorType)
        {
            CurrentFloor = floorType;
            EscalatorManager.Instance.OnPlayerFloorChanged.Invoke(CurrentFloor);
        }
    }
}
