using System;
using Reflex.Attributes;
using System.Collections.Generic;
using Source.Scripts.Core.GameSession;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Enviroment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Core.Spawners
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _points = new List<SpawnPoint>();

        [Inject] private SpawnItemPool _pool;
        [Inject] private ILevelSpawnItemSetting _spawnCount;

        private int _count;

        private void Awake()
        {
            GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
            _count = Mathf.Clamp(_spawnCount.GetCount(), 0, int.MaxValue);
        }

        public void Create(LevelTypeMode mode)
        {
            _points.RemoveAll(point => (int)point.Mode > (int)mode);

            for (int i = 0; i < _count; i++)
            {
                _pool.Create(GetFreeRandomPoint());
            }
        }

        private Transform GetFreeRandomPoint()
        {
            if (_points == null || _points.Count == 0)
                throw new ArgumentNullException(nameof(_points));

            int randomPoint = Random.Range(0, _points.Count);
            SpawnPoint tempPoint = _points[randomPoint];

            Transform point = tempPoint.transform;
            _points.Remove(tempPoint);

            return point;
        }
    }
}