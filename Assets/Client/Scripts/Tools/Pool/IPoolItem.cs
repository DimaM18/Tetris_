using UnityEngine;


namespace Client.Scripts.Tools.Pool
{
    public interface IPoolItem
    {
        string PoolItemType { get; set; }
        GameObject GetGameObject();
        void PrepareSpawn();
        void ReturnToPool();
    }
}