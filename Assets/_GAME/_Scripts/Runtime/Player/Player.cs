using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class Player : Singleton<Player>
    {
        [field : SerializeField] public PlayerMovement Movement { get; private set; }
    }
}
