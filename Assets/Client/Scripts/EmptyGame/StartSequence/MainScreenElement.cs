using System;
using System.Collections.Generic;

using Client.Scripts.Game;
using Client.Scripts.Tools.Services;
using Client.Scripts.Ui;
using Client.Scripts.Ui.Controllers;


namespace Client.Scripts.EmptyGame.StartSequence
{
    public class MainScreenElement : ILevelSequenceElement
    {
        public event Action Finished;

        public void Start(Dictionary<string, object> context)
        {
            Service.UiManager.Show<MainScreen>(Panels.MainScreen);
            Finished?.Invoke();
        }
    }
}