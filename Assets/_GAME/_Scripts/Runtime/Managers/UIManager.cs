using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class UIManager : Singleton<UIManager>
    {
        [field : SerializeField] public FloatingJoystick Joystick { get; private set; }
    }
}
