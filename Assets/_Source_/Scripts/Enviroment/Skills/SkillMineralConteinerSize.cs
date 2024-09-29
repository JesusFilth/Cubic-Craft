using Source.Scripts.Characters.Player;

namespace Source.Scripts.Enviroment.Skills
{
    public class SkillMineralConteinerSize : SkillStratigy
    {
        public override void AddProperty(PlayerStats stat)
        {
            stat.AddMaxMineralConteinerSize((int)Value);
        }
    }
}