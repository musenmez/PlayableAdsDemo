using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    [System.Serializable]
    public class PropRevealConfig
    {
        public float Delay;
        public float Duration;
        public Ease EaseType = Ease.Linear;
        public GameObject Body;
    }
}
