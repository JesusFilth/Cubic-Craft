using System;
using System.Linq;
using Source.Scripts.Enviroment.Level;
using UnityEngine;

namespace Source.Scripts.Core.Storage.Level
{
    [CreateAssetMenu(fileName = "LevelSetting", menuName = "World of Cubes/LevelSetting", order = 2)]
    public class LevelSetting : ScriptableObject
    {
        private const int MaxMode = 3;

        [SerializeField] private int _needStars;
        [SerializeField] private Sprite _icon;
        [SerializeField] private LevelModeInit _levelMap;
        [SerializeField] private bool _isEndGame;
        [SerializeField] private LevelMode[] _mods = new LevelMode[MaxMode];

        public int NeedStars => _needStars;
        public Sprite Icon => _icon;

        public LevelModeInit LevelMap => _levelMap;

        public bool IsEndGame => _isEndGame;

        private void OnValidate()
        {
            for (int i = 0; i < _mods.Length; i++)
            {
                _mods[i].SetType((LevelTypeMode)i);
            }

            if (_mods.Length != MaxMode)
                Array.Resize(ref _mods, MaxMode);
        }

        public LevelMode GetLevelMode(LevelTypeMode mode)
        {
            return _mods.FirstOrDefault(levelMode => levelMode.Type == mode);
        }
    }
}
