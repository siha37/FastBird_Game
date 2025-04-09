using UnityEngine;
using System.Collections.Generic;

namespace Assets.MyFolder._01.Script._02.Object.Object_Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        [SerializeField] protected List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> poolDictionary;
        private Dictionary<string, List<GameObject>> activeObjects;
        private bool isInitialized = false;

        public void SetPools(List<Pool> newPools)
        {
            if (isInitialized)
            {
                Debug.LogWarning("Cannot set pools after initialization!");
                return;
            }
            pools = newPools;
        }

        public void InitializePool()
        {
            if (isInitialized) return;

            if (pools == null || pools.Count == 0)
            {
                Debug.LogError("No pools defined!");
                return;
            }

            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            activeObjects = new Dictionary<string, List<GameObject>>();

            foreach (Pool pool in pools)
            {
                if (!pool.prefab)
                {
                    Debug.LogError($"Prefab is null for pool with tag: {pool.tag}");
                    continue;
                }

                Queue<GameObject> objectPool = new Queue<GameObject>();
                List<GameObject> activeList = new List<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
                activeObjects.Add(pool.tag, activeList);
            }

            isInitialized = true;
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!isInitialized)
            {
                Debug.LogError("ObjectPool is not initialized!");
                return null;
            }

            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            Queue<GameObject> pool = poolDictionary[tag];
            List<GameObject> activeList = activeObjects[tag];

            if (pool.Count == 0)
            {
                // 풀이 비어있으면 새로운 오브젝트 생성
                GameObject newObj = Instantiate(pools.Find(p => p.tag == tag).prefab);
                newObj.SetActive(true);
                newObj.transform.position = position;
                newObj.transform.rotation = rotation;
                activeList.Add(newObj);
                return newObj;
            }

            GameObject objectToSpawn = pool.Dequeue();
            if (!objectToSpawn)
            {
                Debug.LogError($"Spawned object is null for tag: {tag}");
                return null;
            }

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            activeList.Add(objectToSpawn);
            
            return objectToSpawn;
        }

        public void ReturnToPool(string tag, GameObject obj)
        {
            if (!isInitialized || !poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Cannot return object to pool with tag: {tag}");
                return;
            }

            if (!obj)
            {
                Debug.LogError("Cannot return null object to pool");
                return;
            }

            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
            activeObjects[tag].Remove(obj);
        }
    }
} 