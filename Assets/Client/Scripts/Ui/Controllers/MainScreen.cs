using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Enums;
using Client.Scripts.Ui.Bind;
using Object = UnityEngine.Object;

namespace Client.Scripts.Ui.Controllers
{
    public class MainScreen : IPanelController
    {
        private Panel _panel;
        private LabelBind _scoreLabel;
        private LabelBind _linesLabel;
        private LabelBind _levelLabel;
        private ImageBind _imageBind;
        
        public void Init(Panel panel)
        {
            _panel = panel;

            _scoreLabel = _panel.Bind<LabelBind>("ScoreLabel");
            _linesLabel = _panel.Bind<LabelBind>("LinesLabel");
            _levelLabel = _panel.Bind<LabelBind>("LevelLabel");
            _imageBind = _panel.Bind<ImageBind>("NextItemIcon");

            SetupText();
            SetupIcon();
        }

        private void SetupIcon()
        {
            Data.Game.Empty.NextTetrominoIndex.Changed += NextTetrominoIndexOnChanged;
            _imageBind.Sprite = Data.Configs.TetrominoVisualConfig.GetSpriteByType((TetrominoType)Data.Game.Empty.NextTetrominoIndex.Value);
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
            Data.Game.Empty.NextTetrominoIndex.Changed -= NextTetrominoIndexOnChanged;
            
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
        
        private void NextTetrominoIndexOnChanged(int arg1, int arg2)
        {
            _imageBind.Sprite = Data.Configs.TetrominoVisualConfig.GetSpriteByType((TetrominoType)Data.Game.Empty.NextTetrominoIndex.Value);
        }
    }
}