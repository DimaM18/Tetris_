using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Constants;
using Client.Scripts.Tools.Loading;
using Client.Scripts.Tools.Services;
using Client.Scripts.Ui;
using Client.Scripts.Ui.Controllers;
using UnityEngine;

namespace Client.Scripts.EmptyGame.Systems
{
    public class InputSystem : IBattleSystem
    {
        private PauseScreen _screen;
        
        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_screen != null)
                {
                    _screen = null;
                    Data.Game.Empty.Paused.Value = false;
                    Service.UiManager.Hide(Panels.PauseScreen);
                }
                else
                {
                    Data.Game.Empty.Paused.Value = true;
                    _screen = Service.UiManager.Show<PauseScreen>(Panels.PauseScreen);
                }
            }
            
            if (_screen != null && Input.GetKeyDown(KeyCode.R))
            {
                Service.UiManager.HideAll();
                SceneLoader.Load(Scenes.Game);
            }
        }
    }
}