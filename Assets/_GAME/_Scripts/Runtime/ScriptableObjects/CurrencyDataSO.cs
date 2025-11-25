using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    [CreateAssetMenu(fileName = "Currency Data", menuName = "Game/Currency Data")]
    public class CurrencyDataSO : ScriptableObject
    {
        public int InitialCurrency;
    }
}
