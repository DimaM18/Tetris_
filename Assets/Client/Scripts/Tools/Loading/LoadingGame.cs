using System.Collections;
using System.Collections.Generic;

using Client.Scripts.Audio;
using Client.Scripts.Configs;
using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Constants;
using Client.Scripts.Tools.DebugTools.DynamicDebugMenu;
using Client.Scripts.Tools.Pool;
using Client.Scripts.Tools.StateMachine.Core;
using Client.Scripts.Ui;

using UnityEngine;


namespace Client.Scripts.Tools.Loading
{
    public class LoadingGame : MonoBehaviour
    {
        [SerializeField]
        private LoadingGameProgress _uiProgress;
        
        private readonly List<IAssetAsyncLoader> _resources = new();
        private int _countResourcesComplete;
        private bool _resourcesComplete;

        private bool _uiComplete;
        
        private bool _loading;

        private void Start()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            Debug.unityLogger.filterLogType = LogType.Log;

            Data.Create();

            _resources.Add(new AssetAsyncLoader<PoolConfig>(ConfigPath.Pool, config => Data.Configs.Pool = config));
            _resources.Add(new AssetAsyncLoader<DifficultyConfig>(ConfigPath.DifficultyConfig, config => Data.Configs.DifficultyConfig = config));
            _resources.Add(new AssetAsyncLoader<TetrominoVisualConfig>(ConfigPath.TetrominoVisualConfig, config => Data.Configs.TetrominoVisualConfig = config));
            _resources.Add(new AssetAsyncLoader<BoardConfig>(ConfigPath.BoardConfig, config =>
            {
                Data.Configs.BoardConfig = config;
                Data.Configs.BoardConfig.Init();
            }));
            _resources.Add(new AssetAsyncLoader<UiPrefabs>(ConfigPath.Ui, config => Data.Configs.Ui = config));

            _uiProgress.ProgressCompleted += OnUiProgressComplete;

            StartCoroutine(LoadProgress());
            _loading = true;
        }
        
        private void Update()
        {
            if (_loading == false)
            {
                return;
            }

            if (_uiComplete && _resourcesComplete)
            {
                _loading = false;
                SceneLoader.Load(Scenes.Game);
            }
        }

        private void OnUiProgressComplete()
        {
            _uiComplete = true;
        }

        private IEnumerator LoadProgress()
        {
            yield return new WaitForEndOfFrame();

            _countResourcesComplete = 0;
            foreach (IAssetAsyncLoader asyncLoader in _resources)
            {
                asyncLoader.StartLoading(OnLoadingComplete);
            }
        }

        private void OnLoadingComplete()
        {
            _countResourcesComplete++;

            if (_countResourcesComplete == _resources.Count)
            {
                _resourcesComplete = true;
            }
        }
    }
}
