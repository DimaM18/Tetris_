using Client.Scripts.EmptyGame.LevelObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Client.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "Configs/BoardConfig")]
    public class BoardConfig : ScriptableObject
    {
        [SerializeField]
        private TetrominoData[] _tetrominoes;
        
        [SerializeField]
        private Tile _ghostTile;
        
        [SerializeField]
        private Vector2Int _boardSize = new Vector2Int(10, 20);

        [SerializeField]
        private Vector3Int _spawnPosition = new Vector3Int(-1, 8, 0);

        public TetrominoData[] TetrominoDatas => _tetrominoes;
        public Vector2Int BoardSize => _boardSize;
        public Vector3Int SpawnPosition => _spawnPosition;
        public Tile GhostTile => _ghostTile;
        
        public RectInt Bounds 
        {
            get
            {
                Vector2Int position = new Vector2Int(-_boardSize.x / 2, -_boardSize.y / 2);
                return new RectInt(position, _boardSize);
            }
        }
        public void Init()
        {
            for (int i = 0; i < _tetrominoes.Length; i++) 
            {
                _tetrominoes[i].Initialize();
            }
        }
    }
}
