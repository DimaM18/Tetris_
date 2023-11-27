using Client.Scripts.DataStorage;
using Client.Scripts.EmptyGame.LevelObjects;

namespace Client.Scripts.EmptyGame.Systems
{
    public class HighlightSystem : IBattleSystem
    {
        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void OnUpdate()
        {
            if (Data.Game.Empty.Paused.Value)
            {
                return;
            }
            
            TetrominoProjection tetrominoProjection = Data.SceneLinks.TetrominoProjection;
            ActiveTetromino activeActiveTetromino = Data.SceneLinks.ActiveTetromino;
            
            tetrominoProjection.Clear();
            tetrominoProjection.Copy(activeActiveTetromino);
            tetrominoProjection.Drop(activeActiveTetromino);
            tetrominoProjection.Set();
        }
    }
}