using System;
using System.Collections.Generic;
using Client.Scripts.Game;
using Client.Scripts.Tools.Services;
using Client.Scripts.Ui;
using Client.Scripts.Ui.Controllers;
using UnityEngine;

namespace Client.Scripts.EmptyGame.LoseSequence
{
    public class LoadGameLoseScreen : ILevelSequenceElement
    {
        public event Action Finished;

        public void Start(Dictionary<string, object> context)
        {
            Service.UiManager.Show<LoseScreen>(Panels.LoseScreen);
            Service.Timer.AddUpdateListener(Action);
        }

        private void Action()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Service.Timer.RemoveUpdateListener(Action);
                Service.UiManager.Hide(Panels.LoseScreen);
                Finished?.Invoke();
            }
        }
    }
}