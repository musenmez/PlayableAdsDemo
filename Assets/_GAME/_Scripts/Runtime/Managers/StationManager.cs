using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class StationManager : Singleton<StationManager>
    {
        [SerializeField] private List<StationBase> stations = new();

        private readonly Dictionary<StationId, StationBase> _stationsById = new();
        
        private void Awake()
        {
            SetCollection();
        }

        public StationBase GetStation(StationId stationId)
        {
            _stationsById.TryGetValue(stationId, out var station);
            return station;
        }

        private void SetCollection()
        {
            foreach (var station in stations)
            {
                _stationsById.TryAdd(station.StationId, station);
            }
        }
    }
}
