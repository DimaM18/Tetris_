using System;

using UnityEngine;

using Object = UnityEngine.Object;


namespace Client.Scripts.Tools.Loading
{
    public interface IAssetAsyncLoader
    {
        void StartLoading(Action completeAction);
    }
    
    public class AssetAsyncLoader<T> : IAssetAsyncLoader where T : Object
    {
        private readonly string _assetPath;
        private readonly Action<T> _assignAction;
        private Action _completeAction;
        
        private ResourceRequest _loadingRequest;

        public AssetAsyncLoader(string assetPath, Action<T> assignAction)
        {
            _assetPath = assetPath;
            _assignAction = assignAction;
        }

        public void StartLoading(Action completeAction)
        {
            _completeAction = completeAction;
            
            _loadingRequest = Resources.LoadAsync(_assetPath);
            _loadingRequest.completed += OnEndLoading;
        }

        private void OnEndLoading(AsyncOperation operation)
        {
            _loadingRequest.completed -= OnEndLoading;

            if (_loadingRequest.asset == null)
            {
                Debug.LogError("Asset not load: " + _assetPath);
            }
            
            _assignAction.Invoke(_loadingRequest.asset as T);
            _completeAction.Invoke();
            
            _loadingRequest = null;
        }
    }
}