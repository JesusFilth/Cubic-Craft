using System;
using Reflex.Attributes;
using Source.Scripts.Characters.Player;
using Source.Scripts.Core.Localization;
using Source.Scripts.Core.Pools;
using Source.Scripts.Sounds;
using Source.Scripts.Views;
using UnityEngine;

namespace Source.Scripts.Enviroment.Mineral
{
    public class MineralOre : WorkProcess<PlayerMiner>, IMineralOrePoint
    {
        [SerializeField] private MineralType _type;
        [SerializeField] private int _count;

        [Inject] private MineralCubeViewPool _cubeViewPool;
        [Inject] private IMineralWorkSounds _sound;
        [Inject] private MessageBox _messageBox;
        [Inject] private LocalizationTranslate _localizationTranslate;
        [Inject] private MiningParticlePool _particlePool;

        public event Action Empty;

        public MineralType Type => _type;

        private void Start()
        {
            SetCurrentCount(_count);
        }

        public void AddCount(int count)
        {
            _count += count;
            SetCurrentCount(_count);
        }

        public MineralType GetMineralType()
        {
            return Type;
        }

        public void SetType(MineralType type) => _type = type;

        public void SetCount(int count)
        {
            _count = count;
            SetCurrentCount(_count);
        }

        public override void ToWork()
        {
            if (Player == null)
                throw new ArgumentNullException(nameof(Player));

            if (Player.CanExtract() == false)
            {
                _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.ConteinerFull));
                return;
            }

            _sound.CraftMineral();
            AddProgress(Player.GetWorkPower());

            Player.Transform.LookAt(gameObject.transform);
            _particlePool.Create(Transform);

            CheckComplete();
        }

        protected override void Complete()
        {
            _count--;
            Player.AddMineral(_type);
            SendMineralCubeToTrack();
            CheckDestroy();

            SetCurrentCount(_count);

            _sound.ExtractionMineral();
        }

        private void CheckDestroy()
        {
            if (_count == 0)
            {
                Empty?.Invoke();
                Destroy(gameObject);
            }
        }

        private void SendMineralCubeToTrack()
        {
            MineralMovementSettings mineralSettings = new MineralMovementSettings(
                Transform,
                Player.GetTrackPoint(),
                _type,
                Player.GetTruckEndPoint());

            _cubeViewPool.Create(mineralSettings);
        }
    }
}
