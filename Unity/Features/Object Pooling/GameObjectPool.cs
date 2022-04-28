// #define USE_NAUGHTY_ATTRIBUTES

using System;
using System.Collections.Generic;

#if USE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

using Slaggy.Unity.CoroutineUtility;
using Slaggy.Unity.Singletons;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Slaggy.Unity.Pooling
{
    public class GameObjectPool : SingletonStandalone<GameObjectPool>
    {
        [Serializable]
        private class Pool
        {
            public string key = string.Empty;
            public GameObject prefab = null;
            public int size = -1;
        }

        private class RuntimePool
        {
            public Transform parent = null;
            public readonly Queue<GameObject> inactive = new Queue<GameObject>();
            public readonly List<GameObject> active = new List<GameObject>();
        }

#if USE_NAUGHTY_ATTRIBUTES
        [Scene]
#endif
        [SerializeField] private List<string> _initOnScenes = null;
        [SerializeField] private List<Pool> _pools = null;

        private bool _initialized = false;
        private Transform _globalParent = null;
        private Dictionary<string, RuntimePool> _runtimePools = new Dictionary<string, RuntimePool>();

        private void Start()
        {
            if (_initOnScenes.Contains(SceneManager.GetActiveScene().name))
                Initialize();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene newScene, LoadSceneMode loadMode)
        {
            if (!_initOnScenes.Contains(newScene.name)) return;

            switch (loadMode)
            {
                case LoadSceneMode.Single:
                    Initialize();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(loadMode),
                                                          loadMode,
                                                          $"Unsupported scene mode: {Enum.GetName(typeof(LoadSceneMode), loadMode)}");
            }
        }

        private void Initialize()
        {
            _globalParent = new GameObject("[GameObjectPool]").transform;

            //_runtimePools = new Dictionary<string, RuntimePool>();

            InitializePools();
            _initialized = true;
        }

        private void OnSceneUnloaded(Scene newScene) => Clear();

        private void Clear()
        {
            if (!_initialized) return;

            if (_globalParent != null) Destroy(_globalParent.gameObject);

            //_runtimePools?.Clear();
            //_runtimePools = null;
            _runtimePools.Clear();

            _initialized = false;
        }

        private void InitializePools()
        {
            foreach (Pool pool in _pools)
            {
                RuntimePool runtimePool = new RuntimePool();
                runtimePool.parent = new GameObject($"[Pool:{pool.key}]").transform;
                runtimePool.parent.SetParent(_globalParent);

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject gameObjectInstance = Instantiate(pool.prefab, runtimePool.parent);
                    gameObjectInstance.SetActive(false);
                    runtimePool.inactive.Enqueue(gameObjectInstance);
                }

                _runtimePools.Add(pool.key, runtimePool);
            }
        }

        public GameObject Get(string key)
        {
            RuntimePool runtimePool = _runtimePools[key];
            GameObject pooledGameObject = runtimePool.inactive.Dequeue();
            runtimePool.active.Add(pooledGameObject);

            pooledGameObject.transform.SetParent(null, false);
            pooledGameObject.SetActive(true);

            if (pooledGameObject.TryGetComponent(out IPooledObject pooledObjectComponent))
                pooledObjectComponent.OnGetFromPool();

            return pooledGameObject;
        }

        public bool Return(string key, GameObject pooledGameObject)
        {
            if (pooledGameObject == null) return false;

            if (!_runtimePools.ContainsKey(key) || !_runtimePools[key].active.Contains(pooledGameObject)) return false;

            RuntimePool runtimePool = _runtimePools[key];

            if (pooledGameObject.TryGetComponent(out IPooledObject pooledObjectComponent))
                pooledObjectComponent.OnReturnToPool();

            pooledGameObject.SetActive(false);
            pooledGameObject.transform.SetParent(runtimePool.parent, false);
            pooledGameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            runtimePool.active.Remove(pooledGameObject);
            runtimePool.inactive.Enqueue(pooledGameObject);
            return true;
        }

        public void TryReturnAfterTime(string key, GameObject pooledGameObject, float time)
        {
            StartCoroutine(Coroutines.Wait(time, () => Return(key, pooledGameObject)));
        }

        public bool TryGet(string key, out GameObject pooledGameObject)
        {
            if (CanGet(key))
            {
                pooledGameObject = Get(key);
                return true;
            }

            pooledGameObject = null;
            return false;
        }

        public bool CanGet(string key)
        {
            if (_runtimePools.ContainsKey(key))
                return _runtimePools[key].inactive.Count != 0;

            return false;
        }
    }
}