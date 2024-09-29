using System;
using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using Source.Scripts.Core.GameSession;
using Source.Scripts.Enviroment;
using Source.Scripts.Enviroment.Mineral;
using Source.Scripts.Enviroment.Tample;
using Source.Scripts.Enviroment.Truck;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Core.Spawners
{
    public class MineralSpawner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _points = new List<SpawnPoint>();
        [SerializeField] private MineralOreSettings _settings;

        [Inject] private ILevelMineralOreSetting _oreSetting;
        [Inject] private ICurrentLevelInfo _currentLevelInfo;
        [Inject] private ITruckRemoveAllMinerals _truck;

        private void OnValidate()
        {
            if (_settings == null)
                throw new ArgumentNullException(nameof(_settings));
        }

        private void Awake()
        {
            Initialize();
        }

        private void OnDisable()
        {
            if (_truck != null)
                _truck.MineralsRemoved -= AddMinerals;
        }

        public void Create(IReadOnlyList<TempleBlockInterval> blocks)
        {
            foreach (TempleBlockInterval block in blocks)
            {
                while (block.Count != 0)
                {
                    MineralSizeType sizeType = _settings.GetBetweenSize(block.Count);
                    int sizeOre = _settings.GetRandomCount(sizeType);
                    SpawnPoint currentSpawnPoint = GetFreeRandomPoint();

                    MineralOreInitsializator ore = Instantiate(
                        _settings.MineralOre,
                        currentSpawnPoint.gameObject.transform.position,
                        Quaternion.identity);

                    ore.Init(
                        block.Type,
                        sizeOre,
                        _oreSetting.GetOreMaxProgress(),
                        _oreSetting.GetOreForceResistance(),
                        _settings.GetModelView(block.Type, sizeType));

                    currentSpawnPoint.SetMineralOre(ore.GetMineraPoint());

                    block.AddCount(-sizeOre);
                }
            }
        }

        private void Initialize()
        {
            GameLevelConteinerDI.Instance.InjectRecursive(gameObject);

            _truck.MineralsRemoved += AddMinerals;
            RemoveOverPoints();
        }

        private void RemoveOverPoints()
        {
            _points.RemoveAll(point => (int)point.Mode > (int)_currentLevelInfo.GetLevelType());
        }

        private SpawnPoint GetFreeRandomPoint()
        {
            if (_points == null || _points.Count == 0)
                throw new ArgumentNullException(nameof(_points));

            SpawnPoint[] freePoints = _points.Where(point => point.IsBusy == false).ToArray();
            int randomPoint = Random.Range(0, freePoints.Length);

            SpawnPoint tempPoint = freePoints[randomPoint];
            tempPoint.ToBusy();

            return tempPoint;
        }

        private void AddMinerals(IReadOnlyDictionary<MineralType, int> minerals)
        {
            List<TempleBlockInterval> blocks = new List<TempleBlockInterval>();

            foreach (var mineral in minerals)
            {
                SpawnPoint[] points = _points.Where(point => point.IsBusy && point.Ore.GetMineralType() == mineral.Key).ToArray();

                if (points != null && points.Length != 0)
                {
                    points[Random.Range(0, points.Length)].Ore.AddCount(mineral.Value);
                    continue;
                }

                blocks.Add(new TempleBlockInterval(mineral.Key, mineral.Value));
            }

            if (blocks.Count != 0)
                Create(blocks);
        }
    }
}