using System;
using Client.Scripts.Tools.Enums;
using UnityEngine;

namespace Client.Scripts.Configs
{
    [CreateAssetMenu(fileName = "TetrominoVisualConfig", menuName = "Configs/TetrominoVisualConfig")]
    public class TetrominoVisualConfig : ScriptableObject
    {
        [Serializable]
        public class TetrominoVisualItem
        {
            public Sprite Sprite;
            public TetrominoType Tetromino;
        }

        [SerializeField] 
        private TetrominoVisualItem[] _items;

        public Sprite GetSpriteByType(TetrominoType tetrominoType)
        {
            foreach (var item in _items)
            {
                if (item.Tetromino == tetrominoType)
                {
                    return item.Sprite;
                }
            }

            return _items[^1].Sprite;
        }
    }
}
