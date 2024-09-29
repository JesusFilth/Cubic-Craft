using System;
using Reflex.Attributes;
using Source.Scripts.Core.Pools;
using Source.Scripts.Enviroment.Mineral;
using Source.Scripts.Sounds;
using UnityEngine;

namespace Source.Scripts.Enviroment.Tample
{
    [RequireComponent(typeof(Renderer))]
    public class TempleBlock : MonoBehaviour, IMineralCubeViewFinalPosition
    {
        [SerializeField] private MineralType _type;
        [SerializeField] private Material _empty;

        private Material _origin;
        private Renderer _renderer;

        [Inject] private ITempleBuildSounds _sounds;
        [Inject] private MineralCubeViewPool _cubeViewPool;

        public MineralType Type => _type;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            SetEmptyMaterial();
        }

        private void OnValidate()
        {
            if (_empty == null)
                throw new ArgumentNullException(nameof(_empty));
        }

        public void RemoveCube(MineralCubeView mineralCube)
        {
            _cubeViewPool.Release(mineralCube);
            Open();
        }

        private void Open()
        {
            _sounds.ToCompetedBlockBuild();
            SetOriginMaterial();
        }

        private void SetEmptyMaterial()
        {
            _origin = _renderer.material;
            _renderer.material = _empty;
        }

        private void SetOriginMaterial()
        {
            _renderer.material = _origin;
        }
    }
}
