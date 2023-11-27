using System;

using Client.Scripts.Tools.ObjectPathLink;

using Sirenix.OdinInspector;

using UnityEngine;


namespace Client.Scripts.Tools.Pool
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Configs/PoolConfig")]
    public class PoolConfig : ScriptableObject
    {
        [Serializable]
        public class PoolItemInfo
        {
            [SerializeField]
            private string _type;
            
            [SerializeField]
            private GameObjectLink _prefabLink;
            
            public string Type => _type;
            public GameObjectLink Prefab => _prefabLink;
        }
        
        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "Type")]
        private PoolItemInfo[] _poolItemInfos;
        
        public PoolItemInfo[] PoolItemInfos => _poolItemInfos;
    }
}