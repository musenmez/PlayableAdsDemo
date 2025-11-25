using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public abstract class PassengerStationBase : StationBase 
    {
        [Header("Line Settings")]
        [SerializeField] protected Transform lineStartPoint;
        [SerializeField] protected Vector3 lineDirection;
        [SerializeField] protected float lineSpacing;
        
        protected List<StationLineInfo> _passengerLineInfos = new();
        
        public StationLineInfo AddPassenger(Passenger passenger)
        {
            StationLineInfo lineInfo = new(_passengerLineInfos.Count, lineSpacing, lineDirection, passenger);
            _passengerLineInfos.Add(lineInfo);
            return lineInfo;
        }

        public void RemovePassenger(StationLineInfo lineInfo)
        {
            if (!_passengerLineInfos.Contains(lineInfo))
                return;
            
            _passengerLineInfos.Remove(lineInfo);
            SortPassengers();
        }

        public Transform GetPassengerLineTarget(StationLineInfo lineInfo)
        {
            return lineInfo.Index == 0 ? lineStartPoint : _passengerLineInfos[lineInfo.Index - 1].Passenger.transform;
        }

        public Passenger GetAvailablePassenger()
        {
            return IsPassengerAvailable() == false ? null : _passengerLineInfos[0].Passenger;
        }

        public bool IsPassengerAvailable()
        {
            return _passengerLineInfos.Count != 0 &&
                   _passengerLineInfos[0].Passenger.Movement.IsReached(lineStartPoint.position);
        }

        private void SortPassengers()
        {
            for (var i = 0; i < _passengerLineInfos.Count; i++)
            {
                _passengerLineInfos[i].Index = i;
            }
        }
    }

    [System.Serializable]
    public class StationLineInfo
    {
        public int Index;
        public float Spacing;
        public Vector3 Direction;
        public Passenger Passenger;

        public StationLineInfo(int index, float spacing, Vector3 direction, Passenger passenger)
        {
            Index = index;
            Spacing = spacing;
            Direction = direction;
            Passenger = passenger;
        }
    }
}
