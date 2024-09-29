using System;

namespace Source.Scripts.Views.Game
{
    public interface IWorkerProcess
    {
        event Action<int> ChangeCount;

        event Action<float> ChangeProgress;

        void ToWork();
    }
}