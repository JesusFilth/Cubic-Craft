using System;
using Source.Scripts.Core.Storage.Models;

namespace Source.Scripts.Core.Storage.User
{
    public interface IStateStorage
    {
        event Action<UserStatsModel> StatsChanged;

        bool AddStat(ref float value);

        UserStatsModel GetStats();

        int GetPurchaseGold(float value);
    }
}
