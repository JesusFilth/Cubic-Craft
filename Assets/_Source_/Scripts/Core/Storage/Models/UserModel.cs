using System;
using UnityEngine.Scripting;

namespace Source.Scripts.Core.Storage.Models
{
    [Serializable]
    public class UserModel
    {
        [field: Preserve] public string Name;

        [field: Preserve] public int Gold;

        [field: Preserve] public int Stars;

        [field: Preserve] public UserStatsModel PlayerStats;

        [field: Preserve] public LevelModel[] Levels;
    }
}
