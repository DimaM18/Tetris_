using Client.Scripts.Tools.Services;

using UnityEngine;


namespace Client.Scripts.Tools.Pool
{
    public class PoolItemParticleSystem : MonoBehaviour, IPoolItem
    {
        [SerializeField]
        private float _lifeTime;
        
        [SerializeField]
        private ParticleSystem[] _particleSystems;

        private Vector3 _localScale;
        private float _lifeTimer;
        
        public string PoolItemType { get; set; }

        private void Awake()
        {
            _localScale = transform.localScale;
        }

        private void Update()
        {
            _lifeTimer -= Time.deltaTime;

            if (_lifeTimer <= 0.0f)
            {
                ReturnToPool();
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void PrepareSpawn()
        {
            _lifeTimer = _lifeTime;
            gameObject.SetActive(true);
            transform.localScale = _localScale;

            foreach (ParticleSystem system in _particleSystems)
            {
                system.Emit(1);
            }
        }

        public void ReturnToPool()
        {
            foreach (ParticleSystem system in _particleSystems)
            {
                system.Clear();
            }
            gameObject.SetActive(false);
            Service.Pool.ReturnToPool(this);
        }
    }
}