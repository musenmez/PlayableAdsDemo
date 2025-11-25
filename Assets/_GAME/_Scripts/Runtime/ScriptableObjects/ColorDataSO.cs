using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    [CreateAssetMenu(fileName = "Color Data", menuName = "Game/Color Data")]
    public class ColorDataSO : ScriptableObject
    {
        [FormerlySerializedAs("SkinColors")] public List<Color> Colors = new();

        public Color GetRandomSkinColor()
        {
            List<Color> colors = new(Colors);
            colors.Shuffle();
            return colors[0];
        }
    }
}
