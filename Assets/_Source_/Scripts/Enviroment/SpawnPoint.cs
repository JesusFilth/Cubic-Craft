using System;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Enviroment.Mineral;
using UnityEngine;

namespace Source.Scripts.Enviroment
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private LevelTypeMode _mode;

        public IMineralOrePoint Ore { get; private set; }

        public LevelTypeMode Mode => _mode;

        public bool IsBusy { get; private set; }

        public void ToBusy() => IsBusy = true;

        private void OnDisable()
        {
            if (Ore != null)
                Ore.Empty -= ToFreeBusy;
        }

        public void SetMineralOre(IMineralOrePoint ore)
        {
            if (ore == null)
                throw new ArgumentNullException(nameof(ore));

            Ore = ore;
            Ore.Empty += ToFreeBusy;
        }

        private void ToFreeBusy()
        {
            IsBusy = false;
            Ore.Empty -= ToFreeBusy;
        }
    }
}
