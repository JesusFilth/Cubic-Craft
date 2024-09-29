using System;
using System.Collections.Generic;
using Source.Scripts.Enviroment.Mineral;

namespace Source.Scripts.Enviroment.Truck
{
    public interface ITruckRemoveAllMinerals
    {
        event Action<IReadOnlyDictionary<MineralType, int>> MineralsRemoved;

        void ClearAllMinerals();
    }
}
