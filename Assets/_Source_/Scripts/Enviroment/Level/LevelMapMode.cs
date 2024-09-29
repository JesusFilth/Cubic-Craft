using Source.Scripts.Core.Storage.Level;
using UnityEngine;

namespace Source.Scripts.Enviroment.Level
{
    public class LevelMapMode : MonoBehaviour
    {
        [SerializeField] private LevelTypeMode _mode;

        public LevelTypeMode Mode => _mode;
    }
}
