using Client.Scripts.EmptyGame.LevelObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Client.Scripts.DataStorage
{
    public class SceneLinks
    {
        public Tilemap Tilemap { get; private set; }
        public Tilemap GhostTilemap { get; private set; }
        public ActiveTetromino ActiveTetromino { get; private set; }
        public TetrominoProjection TetrominoProjection { get; private set; }

        public void Init()
        {
            var adapter = Object.FindObjectOfType<SceneLinksAdapter>();
            if (!adapter)
            {
                Debug.LogError("SceneLinksAdapter not found on scene");
                return;
            }

            Tilemap = adapter.Tilemap;
            GhostTilemap = adapter.GhostTilemap;
            ActiveTetromino = adapter.ActiveActiveTetromino;
            TetrominoProjection = adapter.TetrominoProjection;
        }
    }
}