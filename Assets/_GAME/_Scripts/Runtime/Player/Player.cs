using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Runtime
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        public Transform PassengerTransform => transform;
        public bool IsUsingEscalator { get; private set; }
        
        [field : SerializeField] public PlayerAnimator Animator { get; private set; }
        [field : SerializeField] public PlayerMovement Movement { get; private set; }
        [field : SerializeField] public PlayerBaggageHandler BaggageHandler { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
