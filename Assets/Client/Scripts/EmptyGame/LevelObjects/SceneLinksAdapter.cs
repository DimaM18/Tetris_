using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Client.Scripts.EmptyGame.LevelObjects
{
    public class SceneLinksAdapter : MonoBehaviour
    {
        [SerializeField] 
        private Tilemap _tilemap;
        
        [SerializeField] 
        private Tilemap _ghostTilemap;
        
        [FormerlySerializedAs("_activePiece")] [SerializeField] 
        private ActiveTetromino activeActiveTetromino;
        
        [FormerlySerializedAs("_ghostPiece")] [SerializeField] 
        private TetrominoProjection tetrominoProjectionPiece;
        
        public Tilemap Tilemap => _tilemap;
        public ActiveTetromino ActiveActiveTetromino => activeActiveTetromino;
        public TetrominoProjection TetrominoProjection => tetrominoProjectionPiece;
        public Tilemap GhostTilemap => _ghostTilemap;
    }
}