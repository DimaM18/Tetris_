using Client.Scripts.Configs;
using Client.Scripts.DataStorage;
using Client.Scripts.EmptyGame.LevelObjects;
using UnityEngine;

namespace Client.Scripts.Tools.Services.BoardService
{
    public class SpawnComponent
    {
        public void SpawnPiece()
        {
            BoardConfig board = Data.Configs.BoardConfig;
            TetrominoData data = board.TetrominoDatas[Data.Game.Empty.NextTetrominoIndex.Value];
            ActiveTetromino activeTetromino = Data.SceneLinks.ActiveTetromino;
            activeTetromino.Initialize(board.SpawnPosition, data);

            if (Service.BoardService.MapComponent.IsValidPosition(activeTetromino, activeTetromino.Position)) 
            {
                int random = Random.Range(0, board.TetrominoDatas.Length);
                Data.Game.Empty.NextTetrominoIndex.Value = random;
                Service.BoardService.MapComponent.Set(activeTetromino);
            } 
            else 
            {
                Data.SceneLinks.Tilemap.ClearAllTiles();
                Service.GlobalEvent.Dispatch(new GameLostEvent());
            }
        }
    }
}