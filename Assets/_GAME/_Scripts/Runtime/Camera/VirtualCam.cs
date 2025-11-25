using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Runtime
{
    public class VirtualCam : MonoBehaviour, IVirtualCamera
    {
        private CinemachineVirtualCamera _virtualCamera;
        public CinemachineVirtualCamera CinemachineVirtualCamera => _virtualCamera == null ? _virtualCamera = GetComponent<CinemachineVirtualCamera>() : _virtualCamera;
       
        [field : SerializeField] public CameraId CameraId { get; private set; }
        [field : SerializeField] public bool ActivateOnAwake { get; private set; }

        protected virtual void OnEnable() 
        {
            if (Managers.Instance == null)
                return;

            CameraManager.Instance.AddCamera(this);
        }

        protected virtual void OnDisable() 
        {
            if (Managers.Instance == null)
                return;

            CameraManager.Instance.RemoveCamera(this);
        }
    }
}
