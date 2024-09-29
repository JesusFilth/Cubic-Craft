using System;
using Source.Scripts.Core.Spawners;
using Source.Scripts.Core.Storage.Level;
using UnityEngine;

namespace Source.Scripts.Enviroment.Level
{
    public class LevelModeInit : MonoBehaviour
    {
        [SerializeField] private Transform _startPlayerPoint;
        [SerializeField] private ItemSpawner _itemSpawner;
        [SerializeField] private LevelMaps _levelMap;
        [SerializeField] private LevelTemples _levelTemple;

        public Transform StartPoint => _startPlayerPoint;

        private void OnValidate()
        {
            if (_startPlayerPoint == null)
                throw new ArgumentNullException(nameof(_startPlayerPoint));

            if (_levelMap == null)
                throw new ArgumentNullException(nameof(_levelMap));

            if (_levelTemple == null)
                throw new ArgumentNullException(nameof(_levelTemple));
        }

        public void Init(LevelTypeMode mode)
        {
            CreateMap(mode);
            CreateTemple(mode);

            if (_itemSpawner != null)
                _itemSpawner.Create(mode);
        }

        private void CreateMap(LevelTypeMode mode)
        {
            _levelMap.Init(mode);
        }

        private void CreateTemple(LevelTypeMode mode)
        {
            _levelTemple.Init(mode);
        }
    }
}