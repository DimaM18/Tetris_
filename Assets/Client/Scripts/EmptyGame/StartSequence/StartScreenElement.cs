using System;
using System.Collections.Generic;
using Client.Scripts.Game;
using Client.Scripts.Tools.Services;
using Client.Scripts.Ui;
using Client.Scripts.Ui.Controllers;
using UnityEngine;

namespace Client.Scripts.EmptyGame.StartSequence
{
    public class StartScreenElement : ILevelSequenceElement
    {
        public event Action Finished;

        public void Start(Dictionary<string, object> context)
        {
            Service.UiManager.Show<StartScreen>(Panels.StartScreen);
            Service.Timer.AddUpdateListener(Action);
        }

        private void Action()
        {
            if (Input.anyKeyDown)
            {
                Service.Timer.RemoveUpdateListener(Action);
                Service.UiManager.Hide(Panels.StartScreen);
                Finished?.Invoke();
            }
        }
    }
}