using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PassengerPath : MonoBehaviour
    {
        [field : SerializeField] public PathId PathId { get; private set; }
        [SerializeField] private List<Transform> waypoints = new();

        public Vector3[] GetPath(Vector3 startPosition, Vector3 endPosition)
        {
            var points = new List<Vector3> { startPosition };

            foreach (var point in waypoints)
            {
                points.Add(point.position);
            }
            
            points.Add(endPosition);
            return points.ToArray();
        }

        private void OnDrawGizmos()
        {
            if (waypoints == null || waypoints.Count < 2)
                return;

            for (var i = 0; i < waypoints.Count - 1; i++)
            {
                if (waypoints[i] == null)
                    continue;
                
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);
            }
            
            Gizmos.DrawSphere(waypoints[^1].position, 0.2f);
        }
    }
}
