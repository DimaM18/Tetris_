using Client.Scripts.Audio;
using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Enums;
using Client.Scripts.Tools.Pool;
using Client.Scripts.Tools.Services;
using Client.Scripts.Ui;

using UnityEngine;


namespace Client.Scripts.Game
{
    public abstract class LevelController : MonoBehaviour
    {
        private Service _service;
        private IGameController _gameController;

        private void Start()
        {
            InitData();
            InitServices();
            InitUiMapping();
            
            Data.Game.GameReady += OnBattleReady;
            Service.Sequence.StartSequence(SequenceType.Init);
        }

        private void OnDestroy()
        {
            _service.DeInit();
        }

        private void Update()
        {
            _service.OnUpdate();
            _gameController?.Update();
        }

        private void OnBattleReady(IGameController gameController)
        {
            Data.Game.GameReady -= OnBattleReady;

            _gameController = gameController;
            _gameController.GameWin += OnWin;
            _gameController.GameLose += OnLose;
            
            _gameController.Start();
        }

        private void OnWin()
        {
            Service.Sequence.StartSequence(SequenceType.Win);
        }

        private void OnLose()
        {
            Service.Sequence.StartSequence(SequenceType.Lose);
        }

        protected virtual void InitData()
        {
            
        }

        protected virtual void InitServices()
        {
            _service = Service.Create();
            
            Service.Register(new Pool());
            Service.Register(new TouchService());
            Service.Register(new Timer());
            Service.Register(new GlobalEventDispatcher());
            Service.Register(new TimeScale());
            Service.Register(new UiManager());
            Service.Register(AudioManager.Create());
            Service.Register(new SequenceService());
        }
        
        protected virtual void InitUiMapping()
        {
            
        }
    }
}