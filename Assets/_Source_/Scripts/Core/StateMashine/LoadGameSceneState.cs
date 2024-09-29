using System;
using IJunior.TypedScenes;
using Source.Scripts.Core.Storage.Level;

namespace Source.Scripts.Core.StateMashine
{
    public class LoadGameSceneState : IGameState<LevelMode>
    {
        private LevelMode _levelMode;

        public void Execute()
        {
            if (_levelMode == null)
                throw new ArgumentNullException(nameof(_levelMode));

            GameLevel.Load(_levelMode);
        }

        public void SetParam(LevelMode level)
        {
            if (level == null)
                throw new ArgumentNullException(nameof(level));

            _levelMode = level;
        }
    }
}