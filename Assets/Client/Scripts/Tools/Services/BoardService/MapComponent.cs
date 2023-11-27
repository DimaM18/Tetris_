using Client.Scripts.DataStorage;
using Client.Scripts.EmptyGame.LevelObjects;
using UnityEngine;

namespace Client.Scripts.Tools.Services.BoardService
{
    public class MapComponent
    {
        public void Set(ActiveTetromino activeTetromino)
        {
            for (int i = 0; i < activeTetromino.Cells.Length; i++)
            {
                Vector3Int tilePosition = activeTetromino.Cells[i] + activeTetromino.Position;
                Data.SceneLinks.Tilemap.SetTile(tilePosition, activeTetromino.Data.Tile);
            }
        }

        public void Clear(ActiveTetromino activeTetromino)
        {
            for (int i = 0; i < activeTetromino.Cells.Length; i++)
            {
                Vector3Int tilePosition = activeTetromino.Cells[i] + activeTetromino.Position;
                Data.SceneLinks.Tilemap.SetTile(tilePosition, null);
            }
        }

        public bool IsValidPosition(ActiveTetromino activeTetromino, Vector3Int newPosition)
        {
            RectInt bounds = Data.Configs.BoardConfig.Bounds;
            foreach (var offset in activeTetromino.Cells)
            {
                Vector3Int tilePosition = offset + newPosition;
                if (!bounds.Contains((Vector2Int)tilePosition)) 
                {
                    return false;
                }

                if (Data.SceneLinks.Tilemap.HasTile(tilePosition)) 
                {
                    return false;
                }
            }

            return true;
        }
    }
}