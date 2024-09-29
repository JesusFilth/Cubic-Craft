using System;
using Source.Scripts.Core.Storage.Level;
using UnityEngine;

namespace Source.Scripts.Enviroment.Level
{
    public class LevelTemples : MonoBehaviour
    {
        private const int MaxCountMode = 3;

        [SerializeField] private LevelMapMode[] _tempels = new LevelMapMode[MaxCountMode];

        private void OnValidate()
        {
            if (_tempels.Length != MaxCountMode)
                _tempels = new LevelMapMode[MaxCountMode];

            foreach (var templ in _tempels)
            {
                if (templ == null)
                    throw new ArgumentNullException(nameof(_tempels));
            }
        }

        public void Init(LevelTypeMode mode)
        {
            foreach (var temple in _tempels)
            {
                if (temple.Mode == mode)
                    temple.gameObject.SetActive(true);
                else
                    Destroy(temple.gameObject);
            }
        }
    }
}