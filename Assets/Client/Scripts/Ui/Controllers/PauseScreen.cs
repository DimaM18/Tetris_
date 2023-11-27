using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Constants;
using Client.Scripts.Tools.Loading;
using Client.Scripts.Tools.Services;
using Client.Scripts.Ui.Bind;
using UnityEngine;

namespace Client.Scripts.Ui.Controllers
{
    public class PauseScreen: IPanelController
    {
        private Panel _panel;
        private LabelBind _scoreLabel;
        private LabelBind _linesLabel;
        private LabelBind _levelLabel;
        private ActionBind _restartButton;
        private ActionBind _continueButton;
        
        public void Init(Panel panel)
        {
            _panel = panel;

            _scoreLabel = _panel.Bind<LabelBind>("ScoreLabel");
            _linesLabel = _panel.Bind<LabelBind>("LinesLabel");
            _levelLabel = _panel.Bind<LabelBind>("LevelLabel");
            _restartButton = _panel.Bind<ActionBind>("ContinueGame");
            _continueButton = _panel.Bind<ActionBind>("RestartGame");

            SetupText();
            SetupButtons();
        }

        private void SetupButtons()
        {
            _restartButton.Triggered += RestartButtonOnTriggered;
            _continueButton.Triggered += ContinueButtonOnTriggered;
        }

        private void SetupText()
        {
            _scoreLabel.Text = Data.Game.Empty.Score.Value.ToString();
            Data.Game.Empty.Score.Changed += ScoreOnChanged;
            
            _linesLabel.Text = Data.Game.Empty.LinesCount.Value.ToString();
            Data.Game.Empty.LinesCount.Changed += LinesCountOnChanged;
            
            _levelLabel.Text = Data.Game.Empty.Level.Value.ToString();
            Data.Game.Empty.Level.Changed += LevelOnChanged;
        }

        public void DeInit()
        {
            Data.Game.Empty.Level.Changed -= LevelOnChanged;
            Data.Game.Empty.LinesCount.Changed -= LevelOnChanged;
            Data.Game.Empty.Score.Changed -= LevelOnChanged;
            
            _restartButton.Triggered -= RestartButtonOnTriggered;
            _continueButton.Triggered -= ContinueButtonOnTriggered;
            
            Object.Destroy(_panel.gameObject);
        }

        private void LinesCountOnChanged(int arg1, int arg2)
        {
            _linesLabel.Text = Data.Game.Empty.LinesCount.Value.ToString();
        }

        private void LevelOnChanged(int arg1, int arg2)
        {
            _levelLabel.Text = Data.Game.Empty.Level.Value.ToString();
        }

        private void ScoreOnChanged(int arg1, int arg2)
        {
            _scoreLabel.Text = Data.Game.Empty.Score.Value.ToString();
        }
        
        private void ContinueButtonOnTriggered()
        {
            Data.Game.Empty.Paused.Value = false;
            Service.UiManager.Hide(Panels.PauseScreen);
        }

        private void RestartButtonOnTriggered()
        {
            Service.UiManager.HideAll();
            SceneLoader.Load(Scenes.Game);
        }
    }
}