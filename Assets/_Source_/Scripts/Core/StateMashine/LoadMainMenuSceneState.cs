using IJunior.TypedScenes;

namespace Source.Scripts.Core.StateMashine
{
    public class LoadMainMenuSceneState : IGameState
    {
        public void Execute()
        {
            MainMenu.Load();
        }
    }
}
