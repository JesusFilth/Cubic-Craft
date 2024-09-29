namespace Source.Scripts.Characters.Player
{
    public interface IPlayerStats
    {
        PlayerStats GetStats();

        void Resurrect();
    }
}