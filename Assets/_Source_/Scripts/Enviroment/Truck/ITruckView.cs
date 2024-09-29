using System;

namespace Source.Scripts.Enviroment.Truck
{
    public interface ITruckView
    {
        event Action<int, int> ValueChanged;
    }
}
