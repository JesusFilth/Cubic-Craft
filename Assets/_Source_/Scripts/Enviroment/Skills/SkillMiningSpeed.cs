using Source.Scripts.Characters.Player;

namespace Source.Scripts.Enviroment.Skills
{
    public class SkillMiningSpeed : SkillStratigy
    {
        public override void AddProperty(PlayerStats stat)
        {
            stat.AddMiningPower(Value);
        }
    }
}