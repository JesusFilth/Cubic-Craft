using Source.Scripts.Characters.Player;

namespace Source.Scripts.Enviroment.Skills
{
    public class SkillDamage : SkillStratigy
    {
        public override void AddProperty(PlayerStats stat)
        {
            stat.AddDamage(Value);
        }
    }
}