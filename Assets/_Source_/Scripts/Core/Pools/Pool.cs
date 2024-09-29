using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts.Core.Pools
{
    public abstract class Pool<TObj, TCreateParam> : MonoBehaviour
        where TObj : PoolObject
    {
        [SerializeField] private TObj _obj;

        [SerializeField] private int _capasity = 10;
        [SerializeField] private int _maxSize = 10;

        protected TCreateParam CreateParam;

        private ObjectPool<TObj> _pool;

        private void Awake()
        {
            Initilize();
        }

        private void OnValidate()
        {
            if (_obj == null)
                throw new ArgumentNullException(nameof(_obj));
        }

        public TObj Create(TCreateParam param)
        {
            CreateParam = param;

            return _pool.Get();
        }

        public void Release(TObj obj)
        {
            _pool.Release(obj);
        }

        protected abstract void ActionOnGet(TObj obj);

        private void ActionOnRelease(TObj obj)
        {
            obj.gameObject.transform.parent = null;
            obj.gameObject.SetActive(false);
        }

        private void Initilize()
        {
            _pool = new ObjectPool<TObj>(
                createFunc: () => Instantiate(_obj),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => ActionOnRelease(obj),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: _capasity,
                maxSize: _maxSize);
        }
    }
}
