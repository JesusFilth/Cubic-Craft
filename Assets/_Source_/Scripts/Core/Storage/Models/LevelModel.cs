using System;
using Source.Scripts.Core.Storage.Level;
using UnityEngine.Scripting;

namespace Source.Scripts.Core.Storage.Models
{
    [Serializable]
    public class LevelModel
    {
        [field: Preserve] public int Id;
        [field: Preserve] public int NeedStarForOpen;
        [field: Preserve] public bool IsOpen;
        [field: Preserve] public int Stars;
        [field: Preserve] public bool IsEndGame;
        [field: Preserve] public LevelTypeMode OpenMode;
    }
}
