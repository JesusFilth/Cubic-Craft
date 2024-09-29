namespace Source.Scripts.Core.GameSession
{
    public interface ILevelEnemySettings
    {
        float GetSpawnChance();

        float GetSpawnDelay();

        float GetImproveStats();
    }
}