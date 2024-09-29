namespace Source.Scripts.Core.Storage.Level
{
    public interface IFindLevel
    {
        bool TryGetLevel(int index, LevelTypeMode mode, out LevelMode level);

        LevelMode GetEndGameLevelMode(LevelTypeMode mode);
    }
}
