using System;
using System.Collections.Generic;

namespace Source.Scripts.Enviroment.Mineral
{
    public interface IMineralView
    {
        event Action<IReadOnlyDictionary<MineralType, int>> BagChanged;
    }
}
