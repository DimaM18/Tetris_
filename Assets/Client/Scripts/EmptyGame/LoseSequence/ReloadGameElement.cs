using System;
using System.Collections.Generic;
using Client.Scripts.Game;
using Client.Scripts.Tools.Constants;
using Client.Scripts.Tools.Loading;
using Client.Scripts.Tools.Services;

namespace Client.Scripts.EmptyGame.LoseSequence
{
    public class ReloadGameElement : ILevelSequenceElement
    {
        public event Action Finished;

        public void Start(Dictionary<string, object> context)
        {
            Service.UiManager.HideAll();
            SceneLoader.Load(Scenes.Game);
            Finished?.Invoke();
        }
    }
}