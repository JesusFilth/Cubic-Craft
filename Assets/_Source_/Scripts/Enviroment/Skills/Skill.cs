using Source.Scripts.Characters.Player;
using UnityEngine;

namespace Source.Scripts.Enviroment.Skills
{
    [CreateAssetMenu(fileName = "Skill", menuName = "World of Cubes/Skill", order = 2)]
    public class Skill : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private SkillStatType _type;

        private SkillStratigy _skillStratigy;

        public Sprite Icon => _icon;
        public float Value => _skillStratigy.Value;
        public SkillStatType Type => _type;

        private void Awake()
        {
            Initialize();
        }

        public void ActiveSkill(PlayerStats stats)
        {
            _skillStratigy.AddProperty(stats);
        }

        public void SetValue(float value)
        {
            _skillStratigy.SetValue(value);
        }

        private void Initialize()
        {
            SkillStratigyFactory skillStratigyFactory = new SkillStratigyFactory();

            _skillStratigy = skillStratigyFactory.GetSkill(_type);
        }
    }
}
