using System;

namespace Source.Scripts.Enviroment.Mineral
{
    public interface IMineralOrePoint
    {
        event Action Empty;

        MineralType GetMineralType();

        void AddCount(int count);
    }
}
