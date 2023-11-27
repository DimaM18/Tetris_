using System;


namespace Client.Scripts.Game
{
    public interface IGameController
    {
        event Action GameWin;
        event Action GameLose;

        void Start();
        void Update();
    }
}