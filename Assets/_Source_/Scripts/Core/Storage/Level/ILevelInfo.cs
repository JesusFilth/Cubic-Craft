using UnityEngine;

namespace Source.Scripts.Core.Storage.Level
{
    public interface ILevelInfo
    {
        public int GetNeedStars(int index);

        public Sprite GetIcon(int index);
    }
}
