﻿using Source.Scripts.Enviroment.Mineral;
using Source.Scripts.Enviroment.Tample;

namespace Source.Scripts.Characters.Player
{
    public class PlayerBuilder : PlayerWorker
    {
        public bool CanBuild(MineralType type) => Truck.HasMineral(type);

        public void BuildBlock(TempleBlock block) => Truck.ToBuild(block);

        public override float GetWorkPower()
        {
            Animation.ToCraft();

            return Stats.BuildPower;
        }
    }
}
