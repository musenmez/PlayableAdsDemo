using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public interface IMoneyStack
    {
        void RemoveFromStack(Money money);
    }
}
