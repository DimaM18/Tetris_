using Client.Scripts.DataStorage;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Client.Scripts.Tools.Services.BoardService
{
    public class LinesComponent
    {
        public void ClearLines()
        {
            RectInt bounds = Data.Configs.BoardConfig.Bounds;
            int row = bounds.yMin;
            int linesCleared = 0;
            while (row < bounds.yMax)
            {
                if (IsLineFull(row))
                {
                    linesCleared++;
                    LineClear(row);
                } 
                else 
                {
                    row++;
                }
            }

            if (linesCleared > 0)
            {
                Data.Game.Empty.LinesCount.Value += linesCleared;
                Data.Game.Empty.Level.Value = GameCalculator.GetCurrentLevel();
                int scoreToAdd = GameCalculator.GetScoreToAdd(linesCleared, Data.Game.Empty.Level.Value);
                Data.Game.Empty.Score.Value += scoreToAdd;
            }
        }

        private bool IsLineFull(int row)
        {
            RectInt bounds = Data.Configs.BoardConfig.Bounds;
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                if (!Data.SceneLinks.Tilemap.HasTile(position)) 
                {
                    return false;
                }
            }
        
            return true;
        }

        private void LineClear(int row)
        {
            RectInt bounds = Data.Configs.BoardConfig.Bounds;
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                Data.SceneLinks.Tilemap.SetTile(position, null);
            }
        
            while (row < bounds.yMax)
            {
                for (int col = bounds.xMin; col < bounds.xMax; col++)
                {
                    Vector3Int position = new Vector3Int(col, row + 1, 0);
                    TileBase above = Data.SceneLinks.Tilemap.GetTile(position);
        
                    position = new Vector3Int(col, row, 0);
                    Data.SceneLinks.Tilemap.SetTile(position, above);
                }
        
                row++;
            }
        }
    }
}