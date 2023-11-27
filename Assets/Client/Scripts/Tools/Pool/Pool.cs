using System.Collections.Generic;

using Client.Scripts.DataStorage;
using Client.Scripts.Tools.ObjectPathLink;
using Client.Scripts.Tools.Services;

using UnityEngine;


namespace Client.Scripts.Tools.Pool
{
    public class Pool : IService
    {
        private readonly Dictionary<string, List<IPoolItem>> _pool = new();
        private readonly Dictionary<string, GameObjectLink> _itemPrototypesLinks = new();
        private readonly Dictionary<string, GameObject> _itemPrototypes = new();

        private Transform _rootTransform;
        private readonly Dictionary<string, Transform> _itemTransform = new();
        
        public Pool()
        {
            PoolConfig config = Data.Configs.Pool;
            foreach (PoolConfig.PoolItemInfo itemInfo in config.PoolItemInfos)
            {
                _itemPrototypesLinks.Add(itemInfo.Type, itemInfo.Prefab);
            }

            var rootObject = new GameObject("Pool");
            _rootTransform = rootObject.transform;

            PreloadItems();
        }

        public void OnUpdate()
        {
        }

        public void DeInit()
        {
            foreach (KeyValuePair<string, List<IPoolItem>> poolList in _pool)
            {
                poolList.Value.Clear();
            }
            _pool.Clear();
            _itemPrototypesLinks.Clear();
            _itemPrototypes.Clear();
        }

        private void PreloadItems()
        {
            List<IPoolItem> tempList = new(5);

            // TODO: preload
            //PreloadItem(PoolItemType.EnemyBlood, 3, tempList);
        }
        
        private void PreloadItem(string type, int count, List<IPoolItem> tempList)
        {
            for (int i = 0; i < count; i++)
            {
                tempList.Add(GetItem(type));
            }
            for (int i = 0; i < count; i++)
            {
                tempList[i].ReturnToPool();
            }
            
            tempList.Clear();
        }

        private IPoolItem GetItem(string itemType)
        {
            if (string.IsNullOrEmpty(itemType))
            {
                return null;
            }
            
            IPoolItem poolItem = null;
            
            if (_pool.ContainsKey(itemType) && _pool[itemType].Count > 0)
            {
                List<IPoolItem> poolList = _pool[itemType];
                poolItem = poolList[poolList.Count - 1];
                poolList.RemoveAt(poolList.Count - 1);
            }
            
            if (poolItem == null)
            {
                if (_itemPrototypes.TryGetValue(itemType, out GameObject itemPrototype) == false)
                {
                    itemPrototype = _itemPrototypesLinks[itemType].Load();
                    _itemPrototypes.Add(itemType, itemPrototype);
                }
                
                GameObject poolItemObject = Object.Instantiate(itemPrototype);
                poolItem = poolItemObject.GetComponent<IPoolItem>();
                poolItem.PoolItemType = itemType;
            }

            return poolItem;
        }

        public GameObject SpawnObject(string itemType)
        {
            IPoolItem poolItem = GetItem(itemType);
            if (poolItem == null)
            {
                return null;
            }
            
            GameObject itemObject = poolItem.GetGameObject();
            itemObject.transform.SetParent(null, false);
            
            poolItem.PrepareSpawn();

            return itemObject;
        }

        public GameObject SpawnObject(string itemType, Transform parent)
        {
            IPoolItem poolItem = GetItem(itemType);
            if (poolItem == null)
            {
                return null;
            }
            
            GameObject itemObject = poolItem.GetGameObject();
            
            itemObject.transform.SetParent(parent, false);
            itemObject.transform.localPosition = Vector3.zero;
            itemObject.transform.localRotation = Quaternion.identity;
            
            poolItem.PrepareSpawn();

            return itemObject;
        }

        public GameObject SpawnObject(string itemType, Vector3 position, Quaternion rotation)
        {
            IPoolItem poolItem = GetItem(itemType);
            if (poolItem == null)
            {
                return null;
            }
            
            GameObject itemObject = poolItem.GetGameObject();
            
            itemObject.transform.SetParent(null, false);
            itemObject.transform.position = position;
            itemObject.transform.rotation = rotation;

            poolItem.PrepareSpawn();
            
            return itemObject;
        }
        
        public void ReturnToPool(IPoolItem poolItem)
        {
            string itemType = poolItem.PoolItemType;
            List<IPoolItem> poolList;
            Transform poolRoot;

            if (_pool.ContainsKey(itemType))
            {
                poolList = _pool[itemType];
                poolRoot = _itemTransform[itemType];
            }
            else
            {
                poolList = new List<IPoolItem>();
                _pool.Add(itemType, poolList);

                var poolRootObject = new GameObject(itemType);
                poolRoot = poolRootObject.transform;
                poolRoot.parent = _rootTransform;
                _itemTransform.Add(itemType, poolRoot);
            }
            
            GameObject itemObject = poolItem.GetGameObject();
            itemObject.transform.parent = poolRoot;
            
            poolList.Add(poolItem);
        }
    }
}