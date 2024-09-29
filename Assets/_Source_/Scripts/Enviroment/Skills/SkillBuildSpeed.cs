using Source.Scripts.Characters.Player;

namespace Source.Scripts.Enviroment.Skills
{
    public class SkillBuildSpeed : SkillStratigy
    {
        public override void AddProperty(PlayerStats stat)
        {
            stat.AddBuildPower(Value);
        }
    }
}
