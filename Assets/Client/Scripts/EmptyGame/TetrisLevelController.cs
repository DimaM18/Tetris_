using Client.Scripts.DataStorage;
using Client.Scripts.Game;
using Client.Scripts.Tools.Services;
using Client.Scripts.Tools.Services.BoardService;
using Client.Scripts.Ui;
using Client.Scripts.Ui.Controllers;

namespace Client.Scripts.EmptyGame
{
    public class TetrisLevelController : LevelController
    {
        protected override void InitData()
        {
            base.InitData();
            Data.SceneLinks.Init();
            Data.Game.Empty.Reset();
        }

        protected override void InitServices()
        {
            base.InitServices();
            Service.Register(new BoardService());
        }

        protected override void InitUiMapping()
        {
            base.InitUiMapping();
            Service.UiManager.AddMapping<MainScreen>(Panels.MainScreen);
            Service.UiManager.AddMapping<StartScreen>(Panels.StartScreen);
            Service.UiManager.AddMapping<PauseScreen>(Panels.PauseScreen);
            Service.UiManager.AddMapping<LoseScreen>(Panels.LoseScreen);
        }
    }
}