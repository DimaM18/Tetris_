using System;
using System.Collections.Generic;

using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Services;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Client.Scripts.Ui
{
    public class UiManager : IService
    {
        private readonly Transform _root;
        private readonly Dictionary<string, IPanelController> _shownScreens = new();
        private readonly Dictionary<string, Type> _controllersMapping = new();
        
        public UiManager()
        {
            UiPrefabs config = Data.Configs.Ui;

            var camera = Object.Instantiate(config.CameraPrefab).GetComponent<Camera>();
            var canvas = Object.Instantiate(config.CanvasPrefab).GetComponent<Canvas>();

            canvas.worldCamera = camera;
            _root = canvas.transform;
        }
        
        public void OnUpdate()
        {
        }

        public void DeInit()
        {
            _shownScreens.Clear();
            _controllersMapping.Clear();
        }

        public void AddMapping<T>(string panelName) where T : IPanelController
        {
            _controllersMapping.Add(panelName, typeof(T));
        }

        public void Show(string panelName)
        {
            Show<IPanelController>(panelName);
        }
        
        public T Show<T>(string panelName) where T : class, IPanelController
        {
            if (_shownScreens.ContainsKey(panelName))
            {
                return (T)_shownScreens[panelName];
            }

            if (_controllersMapping.ContainsKey(panelName) == false)
            {
                Debug.LogError("Panel " + panelName + " not mapped to controller. Check LevelController.InitUiMapping()");
                return null;
            }
            
            UiPrefabs config = Data.Configs.Ui;

            GameObject panelObject = Object.Instantiate(config.GetPanelPrefab(panelName), _root);
            var controller = (T)Activator.CreateInstance(_controllersMapping[panelName]);

            controller.Init(panelObject.GetComponent<Panel>());
            
            _shownScreens.Add(panelName, controller);

            return controller;
        }
        
        public void HideAll()
        {
            foreach (var uiPair in _shownScreens)
            {
                _shownScreens[uiPair.Key].DeInit();
            }
            _shownScreens.Clear();
        }

        public void Hide(string panelName)
        {
            if (_shownScreens.ContainsKey(panelName))
            {
                _shownScreens[panelName].DeInit();
                _shownScreens.Remove(panelName);
            }
        }

        public T CreateSubpanel<T>(string subpanelName, Transform parent) where T : class, ISubpanelController, new()
        {
            UiPrefabs config = Data.Configs.Ui;
            
            GameObject panelObject = Object.Instantiate(config.GetSubpanelPrefab(subpanelName), parent);
            var controller = new T();
            
            controller.Init(panelObject.GetComponent<Panel>());

            return controller;
        }
    }
}