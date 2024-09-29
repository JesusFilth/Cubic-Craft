using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class GameLevelConteinerDI : MonoBehaviour
    {
        private Container _container;

        public static GameLevelConteinerDI Instance { get; private set; }

        private void Awake()
        {
            InitHot();
        }

        public void InitHot()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
        }

        public void InjectRecursive(GameObject gameObject)
        {
            GameObjectInjector.InjectRecursive(gameObject, _container);
        }

        private void Initialize()
        {
            _container = gameObject.scene.GetSceneContainer();
        }
    }
}
