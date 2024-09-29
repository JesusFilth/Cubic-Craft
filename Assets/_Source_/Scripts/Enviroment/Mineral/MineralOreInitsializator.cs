using System;
using UnityEngine;

namespace Source.Scripts.Enviroment.Mineral
{
    [RequireComponent(typeof(MineralOre))]
    public class MineralOreInitsializator : MonoBehaviour
    {
        [SerializeField] private MineralOre _ore;

        private void Awake()
        {
            _ore = GetComponent<MineralOre>();
        }

        private void OnEnable()
        {
            try
            {
                Validate();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
        }

        public void Init(MineralType type, int sizeOre, float maxProgress, float forceResist, GameObject view)
        {
            Instantiate(view, transform);
            _ore.SetForceResistance(forceResist);
            _ore.SetMaxProgress(maxProgress);
            _ore.SetCount(sizeOre);
            _ore.SetType(type);
        }

        public IMineralOrePoint GetMineraPoint() => _ore;

        private void Validate()
        {
            if (_ore == null)
                throw new ArgumentNullException(nameof(_ore));
        }
    }
}