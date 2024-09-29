using Source.Scripts.Core.Storage.Level;

namespace Source.Scripts.Core.GameSession
{
    public interface ICurrentLevelInfo
    {
        int GetLevelNumber();

        LevelTypeMode GetLevelType();

        int GetPrice();
    }
}
