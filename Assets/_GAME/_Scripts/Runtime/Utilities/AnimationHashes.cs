using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public static class AnimationHashes
    {
        public static int Idle = Animator.StringToHash(nameof(Idle));
        public static int Run = Animator.StringToHash(nameof(Run));
        public static int Speed = Animator.StringToHash(nameof(Speed));
    }
}
