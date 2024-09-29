using Source.Scripts.Enviroment.Mineral;

namespace Source.Scripts.Enviroment.Tample
{
    public interface ITempleProgressView
    {
        void ChangeBuildProgressBar(float percent, int blockCount);

        void ChangeCurrentMineral(MineralType type);

        void ChangeNextMineral(bool hasMineral, MineralType type);
    }
}
