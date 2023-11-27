using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace  Client.Scripts.Tools.Loading
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Image _fadeImage;

        private bool _isStarted;
        private bool _isLoadingScene;
        
        private bool _isExitFromScene;
        private bool _skipFrame;
        private string _sceneName;
        private float _fadeTime;
        private float _transitionTime;
        private Color _currentColor;
        
        private Action _onLoadedCallback;

        private static bool _isActive;
        public static bool IsActive => _isActive;

        public static void Load(string sceneName, float fadeTime = 0.4f, Action onLoadedCallback = null)
        {
            var loaderObject = Instantiate(Resources.Load<GameObject>("Prefabs/Loading/SceneLoader"));
            loaderObject.GetComponent<SceneLoader>().FadeToScene(sceneName, fadeTime, onLoadedCallback);
            _isActive = true;
        }

        private void FadeToScene(string sceneName, float fadeTime, Action onLoadedCallback)
        {
            _sceneName = sceneName;
            _fadeTime = fadeTime;

            _isStarted = true;
            _isExitFromScene = true;
            _transitionTime = 0.0f;
            _currentColor = new Color(0.05f, 0.05f, 0.05f, 0.0f);
            _fadeImage.color = _currentColor;

            _onLoadedCallback = onLoadedCallback;
            
            DontDestroyOnLoad(this);
        }
        
        private void Update()
        {
            if (_isStarted == false || _isLoadingScene)
            {
                return;
            }

            if (_skipFrame)
            {
                _skipFrame = false;
                return;
            }

            _transitionTime += Time.deltaTime / Mathf.Max(Time.timeScale, 0.01f);
            float progress = Mathf.Clamp01(_transitionTime / _fadeTime);
            _currentColor.a = _isExitFromScene ? progress : 1.0f - progress;
            _fadeImage.color = _currentColor;

            if (_transitionTime >= _fadeTime)
            {
                if (_isExitFromScene)
                {
                    _isLoadingScene = true;
                    SceneManager.sceneLoaded += OnLevelFinishedLoading;
                    SceneManager.LoadScene(_sceneName);
                }
                else
                {
                    Destroy(gameObject);
                    _isActive = false;
                }
            }
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
            _isLoadingScene = false;
            _isExitFromScene = false;
            
            // need to skip frame, because scene loading could take lot of unscaled time
            _skipFrame = true;
            _transitionTime = 0;
            
            _onLoadedCallback?.Invoke();
            _onLoadedCallback = null;
        }
    }
}