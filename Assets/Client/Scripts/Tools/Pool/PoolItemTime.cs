using Client.Scripts.Tools.Services;

using UnityEngine;


namespace Client.Scripts.Tools.Pool
{
    public class PoolItemTime : MonoBehaviour, IPoolItem
    {
        [SerializeField]
        private float _lifeTime;

        [SerializeField]
        private bool _resetScaleOnDespawn;
        
        private float _lifeTimer;
        
        public string PoolItemType { get; set; }

        private void Awake()
        {
            _lifeTimer = _lifeTime;
        }

        private void Update()
        {
            _lifeTimer -= Time.deltaTime;

            if (_lifeTimer <= 0.0f)
            {
                ReturnToPool();
            }
        }

        private void ResetScale()
        {
            transform.localScale = Vector3.one;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void PrepareSpawn()
        {
            _lifeTimer = _lifeTime;
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            if (_resetScaleOnDespawn)
            {
                ResetScale();
            }
            
            gameObject.SetActive(false);
            Service.Pool.ReturnToPool(this);
        }
    }
}