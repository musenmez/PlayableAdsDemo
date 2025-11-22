using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Game.Runtime
{
    public interface IVirtualCamera
    {
        bool ActivateOnAwake { get; }
        CameraId CameraId { get; }
        CinemachineVirtualCamera CinemachineVirtualCamera { get; }
    }
}
