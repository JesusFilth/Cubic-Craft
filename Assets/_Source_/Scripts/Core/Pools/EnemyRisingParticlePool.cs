using UnityEngine;

namespace Source.Scripts.Core.Pools
{
    public class EnemyRisingParticlePool : Pool<PoolObject, Transform>
    {
        protected override void ActionOnGet(PoolObject obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = CreateParam.position;
        }
    }
}
