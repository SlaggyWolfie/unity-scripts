using System.Collections.Generic;
using System.Linq;

using Slaggy.Unity.Singletons;

using UnityEngine;
//using UnityEngine.Serialization;

namespace Slaggy.Unity.Pooling
{
    /// <summary>
    /// Object pool implementation for using it with particles
    /// </summary>=]
    public class GameObjectFactory : SingletonStandalone<GameObjectFactory>
    {
        [System.Serializable]
        public class Pool
        {
            public string key;
            public GameObject prefab;
            public int size;
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            // Populate pools
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.key, objectPool);
            }
        }

        public GameObject Spawn(string key, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                Debug.LogWarning("Pool with tag " + key + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[key].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            //if (objectToSpawn.TryGetComponent(out IPooledObject o)) o.OnSpawn();

            poolDictionary[key].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        public bool IsObjectAvailable(string key)
        {
            GameObject go = null;

            while (poolDictionary[key].Count > 0 && go == null)
            {
                go = poolDictionary[key].Peek();
                if (go == null) go = poolDictionary[key].Dequeue();
            }

            foreach (var pool in pools.Where(pool => pool.key == key))
            {
                while (poolDictionary[key].Count < pool.size)
                {
                    // Repopulate pool?
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    poolDictionary[key].Enqueue(obj);
                }

                go = poolDictionary[key].Peek();
                break;
            }

            return go?.transform.parent == null;

            //if (go.transform.parent != null) return false;
            //else return true;
        }
    }
}