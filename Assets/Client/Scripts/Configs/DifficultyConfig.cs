using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Scripts.Configs
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Configs/DifficultyConfig")]
    public class DifficultyConfig : ScriptableObject
    {
        [Serializable]
        public class DifficultyConfigItem
        {
            public int Level;
            public int FramesToMove;
        }

        [FormerlySerializedAs("Items")] [SerializeField] 
        private DifficultyConfigItem[] _items;

        [SerializeField]
        private int _linesToLevelUP;

        public int LinesToLevelUp => _linesToLevelUP;
        
        public float GetTimeToMoveByLevel(int currentLevel)
        {
            foreach (var item in _items)
            {
                if (item.Level == currentLevel)
                {
                    return item.FramesToMove / (float)Application.targetFrameRate;
                }
            }

            return _items[^1].FramesToMove / (float)Application.targetFrameRate;
        }
    }
}