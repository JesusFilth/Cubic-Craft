using System;

namespace Source.Scripts.Views.Game.InterfaceStateMashine
{
    public abstract class UIState
    {
        private IGameLevelView _view;

        public UIState(IGameLevelView gameLevelView)
        {
            if (gameLevelView == null)
                throw new ArgumentNullException(nameof(gameLevelView));

            _view = gameLevelView;
        }

        public virtual void Open() => _view.Show();

        public virtual void Close() => _view.Hide();
    }
}
