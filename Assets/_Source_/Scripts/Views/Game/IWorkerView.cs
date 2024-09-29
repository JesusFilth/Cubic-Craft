using Source.Scripts.Characters.Player;

namespace Source.Scripts.Views.Game
{
    public interface IWorkerView
    {
        void Binding(IWorkerProcess workerProcess);

        void Unbinding();

        void SetIcon(PlayerWorker playerWorker);
    }
}
