using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class EscalatorTrigger : MonoBehaviour
    {
        [SerializeField] private Escalator escalator;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEscalatorPassenger passenger))
            {
                escalator.TryAddPassenger(passenger);
            }
        }
    }
}
