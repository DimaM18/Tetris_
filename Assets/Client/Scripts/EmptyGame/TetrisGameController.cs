using System;

using Client.Scripts.EmptyGame.Systems;
using Client.Scripts.Game;
using Client.Scripts.Tools.Services;
using Client.Scripts.Tools.Services.GlobalEvents;

public class GameLostEvent : GlobalEvent
{
}

namespace Client.Scripts.EmptyGame
{
    public class TetrisGameController : IGameController
    {
        public event Action GameWin;
        public event Action GameLose;
        
        private readonly IBattleSystem[] _systems;
        private bool _canUpdate;

        public TetrisGameController()
        {
            _systems = new IBattleSystem[]
            {
                new StartGameSystem(),
                new InputSystem(),
                new StepTetrominoSystem(),
                new HighlightSystem(),
            };
        }

        public void Start()
        {
            foreach (IBattleSystem system in _systems)
            {
                system.Start();
            }

            _canUpdate = true;
            Service.GlobalEvent.AddListener<GameLostEvent>(OnEndGame);
        }

        public void Update()
        {
            if (!_canUpdate)
            {
                return;
            }
            
            foreach (IBattleSystem system in _systems)
            {
                system.OnUpdate();
            }
        }

        private void OnEndGame(GameLostEvent obj)
        {
            _canUpdate = false;
            
            foreach (IBattleSystem system in _systems)
            {
                system.Stop();
            }

            GameLose?.Invoke();
        }
    }
}