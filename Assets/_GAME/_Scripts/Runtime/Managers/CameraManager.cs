using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Game.Runtime
{
    public class CameraManager : Singleton<CameraManager>
    {
        public Dictionary<CameraId, IVirtualCamera> VirtualCameras { get; private set; } = new();
        public CameraBrain CameraBrain { get; private set; }
        public IVirtualCamera CurrentActiveCamera { get; private set; }

        public const int ACTIVE_PRIORITY = 20;
        public const int PASSIVE_PRIORITY = 9;
        public const float DEFAULT_BLEND_DURATION = 0.5f;
        public const CinemachineBlendDefinition.Style BRAIN_BLEND_EASE = CinemachineBlendDefinition.Style.EaseInOut;

        public void AddCamera(IVirtualCamera virtualCamera)
        {
            if (!VirtualCameras.TryAdd(virtualCamera.CameraId, virtualCamera))
                return;

            SetVirtualCameraPriority(virtualCamera, virtualCamera.ActivateOnAwake ? ACTIVE_PRIORITY : PASSIVE_PRIORITY);
        }

        public void RemoveCamera(IVirtualCamera virtualCamera)
        {
            if (!VirtualCameras.ContainsKey(virtualCamera.CameraId))
                return;

            VirtualCameras.Remove(virtualCamera.CameraId);
        }

        public void ActivateCamera(CameraId cameraID, float blendDuration = DEFAULT_BLEND_DURATION)
        {
            if (!VirtualCameras.ContainsKey(cameraID) || (CurrentActiveCamera != null && CurrentActiveCamera.CameraId == cameraID))
                return;

            SetBrainBlend(blendDuration);
            var targetVirtualCamera = VirtualCameras[cameraID];

            foreach (var virtualCamera in VirtualCameras.Values)
            {
                var priority = virtualCamera == targetVirtualCamera ? ACTIVE_PRIORITY : PASSIVE_PRIORITY;
                SetVirtualCameraPriority(virtualCamera, priority);
            }

            CurrentActiveCamera = targetVirtualCamera;
        }        

        public void SetCameraBrain(CameraBrain cameraBrain)
        {
            CameraBrain = cameraBrain;
        }        

        private void SetBrainBlend(float blendDuration)
        {
            if (CameraBrain == null)
                return;

            CameraBrain.CinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(BRAIN_BLEND_EASE, blendDuration);
        }

        private void SetVirtualCameraPriority(IVirtualCamera virtualCamera, int priority)
        {
            virtualCamera.CinemachineVirtualCamera.Priority = priority;
        }       
    }
}
