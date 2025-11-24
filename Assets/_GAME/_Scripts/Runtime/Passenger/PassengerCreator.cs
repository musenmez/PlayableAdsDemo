using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerCreator : MonoBehaviour
    {
        public static PassengerCreator Instance;
        
        [SerializeField] private Transform creationPoint;

        private Coroutine _spawnCo;

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnPassengers(int count, float delayPerSpawn = 0.1f)
        {
            if (_spawnCo != null)
                StopCoroutine(_spawnCo);
            
            _spawnCo = StartCoroutine(SpawnCo(count, new WaitForSeconds(delayPerSpawn)));
        }

        private IEnumerator SpawnCo(int count, WaitForSeconds delay)
        {
            for (var i = 0; i < count; i++)
            {
                var passenger = PoolingManager.Instance.GetInstance(PoolId.Passenger, creationPoint.position, creationPoint.rotation).GetPoolComponent<Passenger>();
                passenger.Initialize();
                yield return delay;
            }
        }
    }
}
