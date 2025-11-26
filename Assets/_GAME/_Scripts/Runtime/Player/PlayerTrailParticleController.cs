using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerTrailParticleController : MonoBehaviour
    {
        [SerializeField] private PlayerEscalatorHandler escalatorHandler;
        [SerializeField] private ParticleSystem trailParticle;

        private void OnEnable()
        {
            escalatorHandler.OnEntered.AddListener(StopTrail);
            escalatorHandler.OnExited.AddListener(StartTrail);
        }

        private void OnDisable()
        {
            escalatorHandler.OnEntered.RemoveListener(StopTrail);
            escalatorHandler.OnExited.RemoveListener(StartTrail);
        }

        private void StartTrail()
        {
            trailParticle.Play();
        }

        private void StopTrail()
        {
            trailParticle.Stop();
        }
    }
}
