﻿using Source.Scripts.Enviroment.Mineral;
using UnityEngine;

namespace Source.Scripts.Enviroment.Tample
{
    public class TempleBlockInterval
    {
        public TempleBlockInterval(MineralType type, int count)
        {
            Type = type;
            Count = count;
        }

        public MineralType Type { get; private set; }
        public int Count { get; private set; }

        public void AddCount() => Count++;

        public void RemoveCount() => Count--;

        public bool HasCount() => Count > 0;

        public void AddCount(int count)
        {
            Count = Mathf.Clamp(Count + count, 0, int.MaxValue);
        }
    }
}
