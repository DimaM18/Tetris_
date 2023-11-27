using Client.Scripts.EmptyGame.LevelObjects;
using Client.Scripts.Tools.Services;

namespace Client.Scripts.EmptyGame.Systems
{
    public class ClearActiveTetrominoCells : IBattleSystem
    {
        public void Start()
        {
            Service.Timer.AddUpdateListener(OnUpdate);
        }
        public void Stop()
        {
            Service.Timer.RemoveDelayListener(OnUpdate);
        }

        public void OnUpdate()
        {
            ActiveTetromino activeTetromino = DataStorage.Data.SceneLinks.ActiveTetromino;
        }
    }
}