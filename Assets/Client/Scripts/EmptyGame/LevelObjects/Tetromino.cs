using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Client.Scripts.EmptyGame.LevelObjects
{
    [System.Serializable]
    public class TetrominoData
    {
        public Tile Tile;
        public TetrominoType Tetromino;

        public Vector2Int[] Cells { get; private set; }
        public Vector2Int[,] WallKicks { get; private set; }

        public void Initialize()
        {
            Cells = Data.Meta.Cells[Tetromino];
            WallKicks = Data.Meta.WallKicks[Tetromino];
        }
    }
}