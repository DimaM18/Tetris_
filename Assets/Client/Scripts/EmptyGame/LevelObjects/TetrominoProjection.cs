using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Services;
using UnityEngine;

namespace Client.Scripts.EmptyGame.LevelObjects
{
    public class TetrominoProjection : MonoBehaviour
    {
        private const int TetrominoCellsCount = 4;
        
        private Vector3Int[] _cells;
        private Vector3Int _position;

        private void Awake()
        {
            _cells = new Vector3Int[TetrominoCellsCount];
        }
        
        public void Clear()
        {
            foreach (var offset in _cells)
            {
                Vector3Int tilePosition = offset + _position;
                Data.SceneLinks.GhostTilemap.SetTile(tilePosition, null);
            }
        }

        public void Copy(ActiveTetromino activeTetromino)
        {
            for (int i = 0; i < _cells.Length; i++) 
            {
                _cells[i] = activeTetromino.Cells[i];
            }
        }

        public void Drop(ActiveTetromino activeTetromino)
        {
            Vector3Int trackingPiecePosition = activeTetromino.Position;
            int current = trackingPiecePosition.y;
            int bottom = -Data.Configs.BoardConfig.BoardSize.y / 2 - 1;

            Service.BoardService.MapComponent.Clear(activeTetromino);
            for (int row = current; row >= bottom; row--)
            {
                trackingPiecePosition.y = row;

                if (Service.BoardService.MapComponent.IsValidPosition(activeTetromino, trackingPiecePosition))
                {
                    _position = trackingPiecePosition;
                } 
                else 
                {
                    break;
                }
            }

            Service.BoardService.MapComponent.Set(activeTetromino);
        }

        public void Set()
        {
            foreach (var offset in _cells)
            {
                Vector3Int tilePosition = offset + _position;
                Data.SceneLinks.GhostTilemap.SetTile(tilePosition, Data.Configs.BoardConfig.GhostTile);
            }
        }

    }
}