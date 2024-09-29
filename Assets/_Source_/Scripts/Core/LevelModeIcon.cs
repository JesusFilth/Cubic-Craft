using Source.Scripts.Core.Storage.Level;
using UnityEngine;

namespace Source.Scripts.Core
{
    [CreateAssetMenu(fileName = "LevelModeIcon", menuName = "World of Cubes/LevelModeIcon", order = 2)]

    public class LevelModeIcon : ScriptableObject
    {
        [SerializeField] private LevelTypeMode _type;
        [SerializeField] private Sprite _icon;

        public LevelTypeMode Type => _type;
        public Sprite Icon => _icon;
    }
}
