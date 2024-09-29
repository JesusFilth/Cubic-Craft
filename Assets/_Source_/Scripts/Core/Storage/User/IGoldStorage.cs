using System;

namespace Source.Scripts.Core.Storage.User
{
    public interface IGoldStorage
    {
        event Action<int> GoldChanged;

        int GetGold();

        void AddGold(int value);
    }
}
