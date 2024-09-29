using UnityEngine;

namespace Source.Scripts.Core.Pools
{
    public class MiningParticlePool : Pool<PoolObject, Transform>
    {
        protected override void ActionOnGet(PoolObject obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = CreateParam.position;
        }
    }
}
