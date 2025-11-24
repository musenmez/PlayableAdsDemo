using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    [CreateAssetMenu(fileName = "Passenger Color Data", menuName = "Game/Skin Color Data")]
    public class PassengerColorDataSO : ScriptableObject
    {
        public List<Color> SkinColors = new();

        public Color GetRandomSkinColor()
        {
            List<Color> colors = new(SkinColors);
            colors.Shuffle();
            return colors[0];
        }
    }
}
