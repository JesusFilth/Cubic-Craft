using System;
using UnityEngine;

namespace Source.Scripts.Enviroment.Mineral
{
    [Serializable]
    public class MineralSize
    {
        public MineralSizeType Type;
        [Range(1, 100)] public int MinCount;
        [Range(2, 100)] public int MaxCount;
    }
}