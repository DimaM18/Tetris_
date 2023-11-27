using Client.Scripts.Tools.Enums;
using Client.Scripts.Tools.Services;
using UnityEngine;

namespace Client.Scripts.EmptyGame.LevelObjects
{
    public class ActiveTetromino : MonoBehaviour
    {
        [SerializeField]
        private float moveDelay = 0.1f;

        private int _rotationIndex;
        
        public TetrominoData Data { get; private set; }
        public Vector3Int[] Cells { get; private set; }
        public Vector3Int Position { get; private set; }
        public float StepTime { get; private set; }
        public float MoveTime { get; private set; }

        public void Initialize(Vector3Int position, TetrominoData data)
        {
            Data = data;
            Position = position;

            var configData = DataStorage.Data.Configs;
            var gameData = DataStorage.Data.Game.Empty;
            
            _rotationIndex = 0;
            float speed = configData.DifficultyConfig.GetTimeToMoveByLevel(gameData.Level.Value);
            StepTime = Time.time + speed;
            MoveTime = Time.time + moveDelay;

            Cells ??= new Vector3Int[data.Cells.Length];

            for (int i = 0; i < Cells.Length; i++) 
            {
                Cells[i] = (Vector3Int)data.Cells[i];
            }
        }

        public void HandleMoveInputs()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                Move(Vector2Int.left);
            } 
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
            {
                Move(Vector2Int.right);
            }
        }

        public void Step()
        {
            var configData = DataStorage.Data.Configs;
            var gameData = DataStorage.Data.Game.Empty;
            StepTime = Time.time + configData.DifficultyConfig.GetTimeToMoveByLevel(gameData.Level.Value);

            if(!Move(Vector2Int.down))
            {
                Lock();
            }
        }

        public void HardDrop()
        {
            while (Move(Vector2Int.down)) 
            {
            }

            Lock();
        }

        private void Lock()
        {
            Service.BoardService.MapComponent.Set(this);
            Service.BoardService.LinesComponent.ClearLines();
            Service.BoardService.SpawnComponent.SpawnPiece();
        }
        
        public void Rotate(int direction)
        {
            int originalRotation = _rotationIndex;
            _rotationIndex = Wrap(_rotationIndex + direction, 0, 4);
            ApplyRotationMatrix(direction);

            if (!CheckWallKicks(_rotationIndex, direction))
            {
                _rotationIndex = originalRotation;
                ApplyRotationMatrix(-direction);
            }
        }

        private bool Move(Vector2Int translation)
        {
            Vector3Int newPosition = Position;
            newPosition.x += translation.x;
            newPosition.y += translation.y;

            bool valid = Service.BoardService.MapComponent.IsValidPosition(this, newPosition);
            if (valid)
            {
                Position = newPosition;
                MoveTime = Time.time + moveDelay;
            }

            return valid;
        }

        private void ApplyRotationMatrix(int direction)
        {
            float[] matrix = DataStorage.Data.Meta.RotationMatrix;
            for (int i = 0; i < Cells.Length; i++)
            {
                Vector3 cell = Cells[i];
                int x;
                int y;
                switch (Data.Tetromino)
                {
                    case TetrominoType.I:
                    case TetrominoType.O:
                        cell.x -= 0.5f;
                        cell.y -= 0.5f;
                        x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;

                    default:
                        x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;
                }

                Cells[i] = new Vector3Int(x, y, 0);
            }
        }

        private bool CheckWallKicks(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
            for (int i = 0; i < Data.WallKicks.GetLength(1); i++)
            {
                Vector2Int translation = Data.WallKicks[wallKickIndex, i];
                if (Move(translation)) 
                {
                    return true;
                }
            }

            return false;
        }

        private int GetWallKickIndex(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = rotationIndex * 2;
            if (rotationDirection < 0) 
            {
                wallKickIndex--;
            }

            return Wrap(wallKickIndex, 0, Data.WallKicks.GetLength(0));
        }

        private int Wrap(int input, int min, int max)
        {
            if (input < min) 
            {
                return max - (min - input) % (max - min);
            }

            return min + (input - min) % (max - min);
        }
    }
}
