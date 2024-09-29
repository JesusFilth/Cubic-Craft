using System;
using UnityEngine;

namespace Source.Scripts.Enviroment.Mineral
{
    [Serializable]
    public class MineralOreSettingModel
    {
        public MineralType Type;
        public MineralSizeType SizeType;
        public GameObject Model;
    }
}