using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PathManager : Singleton<PathManager>
    {
        [SerializeField] private List<PassengerPath> paths = new();
        
        private readonly Dictionary<PathId, PassengerPath> _pathsById = new();
        
        private void Awake()
        {
            SetCollection();
        }

        public PassengerPath GetPath(PathId pathId)
        {
            _pathsById.TryGetValue(pathId, out var path);
            return path;
        }

        private void SetCollection()
        {
            foreach (var path in paths)
            {
                _pathsById.TryAdd(path.PathId, path);
            }
        }
    }
}
