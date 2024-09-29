using Source.Scripts.Characters;
using Source.Scripts.Characters.Player;

namespace Source.Scripts.Enviroment.Skills
{
    public class SkillGold : SkillStratigy
    {
        private const int GoldUpValue = 10;

        public override void AddProperty(PlayerStats stat)
        {
            if (stat.gameObject.TryGetComponent(out IWallet wallet))
            {
                wallet.AddCoin((int)Value);
            }
        }

        public override void SetValue(float value)
        {
            base.SetValue(value * GoldUpValue);
        }
    }
}
