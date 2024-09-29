using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.Models;

namespace Source.Scripts.Core.Storage.User
{
    public interface ILevelStorage
    {
        LevelModel GetLastOpenLevel();

        LevelModel GetLevel(int index);

        LevelModel[] GetLevels();

        int GetLevelCount();

        int GetAllStars();

        void AddStar(int indexLevel, LevelTypeMode mode);

        bool TryGetUpValueProperty(int index, out int upValue);
    }
}
