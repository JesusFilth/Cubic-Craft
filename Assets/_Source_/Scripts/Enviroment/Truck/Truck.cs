using System;
using System.Collections.Generic;
using Source.Scripts.Core.Pools;
using Source.Scripts.Enviroment.Mineral;
using Source.Scripts.Enviroment.Tample;
using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Enviroment.Truck
{
    [RequireComponent(typeof(TruckMovement))]
    public class Truck : MonoBehaviour,
        ITruckView,
        IMineralView,
        IMineralCubeViewFinalPosition,
        ITruckIsFull,
        ITruckRemoveAllMinerals
    {
        [SerializeField] private Transform _cubeEndPoint;
        [SerializeField] private TruckBagView _bagView;
        [SerializeField] private Transform _target;

        private int _currentMineralCount = 0;
        private int _maxMineralCount;
        private Dictionary<MineralType, int> _minerals = new Dictionary<MineralType, int>();
        private TruckMovement _movement;

        [Inject] private MineralCubeViewPool _viewPool;

        public event Action<int, int> ValueChanged;
        public event Action<IReadOnlyDictionary<MineralType, int>> BagChanged;
        public event Action<IReadOnlyDictionary<MineralType, int>> MineralsRemoved;
        public event Action Fulled;

        public Transform CubeEndPoint => _cubeEndPoint;
        public bool IsFull => _currentMineralCount == _maxMineralCount;

        private void Awake()
        {
            _movement = GetComponent<TruckMovement>();
            _movement.SetTarget(_target);
        }

        private void OnEnable()
        {
            try
            {
                Validate();
            }
            catch (Exception ex)
            {
                enabled = false;
                throw ex;
            }
        }

        public void ClearAllMinerals()
        {
            MineralsRemoved?.Invoke(_minerals);
            _minerals.Clear();
            _currentMineralCount = 0;

            UpdateMinerals();
        }

        public void RemoveCube(MineralCubeView mineralCube)
        {
            _viewPool.Release(mineralCube);
        }

        public void ToBuild(TempleBlock block)
        {
            MineralMovementSettings mineralSettings = new MineralMovementSettings(
                _cubeEndPoint,
                block.transform,
                block.Type,
                block);

            RemoveMineral(block.Type);
            _viewPool.Create(mineralSettings);
        }

        public void SetMaxMineral(int value)
        {
            _maxMineralCount = Mathf.Clamp(value, 0, int.MaxValue);
            ValueChanged?.Invoke(_currentMineralCount, _maxMineralCount);
        }

        public void SetMovementTarget(Transform target)
        {
            _movement.SetTarget(target);
        }

        public bool HasMineral(MineralType type)
        {
            return _minerals.ContainsKey(type);
        }

        public void AddMineral(MineralType type)
        {
            if (IsFull)
                return;

            if (_minerals.ContainsKey(type) == false)
                _minerals[type] = 0;

            _minerals[type]++;
            _currentMineralCount++;

            UpdateMinerals();

            if (IsFull)
                Fulled?.Invoke();
        }

        private void UpdateMinerals()
        {
            ValueChanged?.Invoke(_currentMineralCount, _maxMineralCount);
            BagChanged?.Invoke(_minerals);

            _bagView.UpdateBagConteiner(_currentMineralCount, _maxMineralCount);
        }

        private void RemoveMineral(MineralType mineral)
        {
            _minerals[mineral]--;

            if (_minerals[mineral] == 0)
                _minerals.Remove(mineral);

            _currentMineralCount--;

            UpdateMinerals();
        }

        private void Validate()
        {
            if (_cubeEndPoint == null)
                throw new ArgumentNullException(nameof(_cubeEndPoint));

            if (_bagView == null)
                throw new ArgumentNullException(nameof(_bagView));

            if (_target == null)
                throw new ArgumentNullException(nameof(_target));
        }
    }
}
