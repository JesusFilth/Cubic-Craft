using Source.Scripts.Enviroment.Mineral;

namespace Source.Scripts.Core.Pools
{
    public class MineralCubeViewPool : Pool<MineralCubeView, MineralMovementSettings>
    {
        protected override void ActionOnGet(MineralCubeView mineral)
        {
            mineral.gameObject.SetActive(true);
            mineral.Init(CreateParam);
        }
    }
}
