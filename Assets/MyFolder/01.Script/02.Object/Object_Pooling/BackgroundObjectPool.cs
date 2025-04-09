using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Object_Pooling
{
    public class BackgroundObjectPool : MonoBehaviour
    {
        private static BackgroundObjectPool instance;
        public static BackgroundObjectPool Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindFirstObjectByType<BackgroundObjectPool>();
                    if (!instance)
                    {
                        GameObject obj = new GameObject("BackgroundObjectPool");
                        instance = obj.AddComponent<BackgroundObjectPool>();
                    }
                }
                return instance;
            }
        }

        [SerializeField] private GameObject backgroundPrefab;
        [SerializeField] private int poolSize = 3;
        private ObjectPool objectPool;
        private bool isInitialized = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializePool();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void InitializePool()
        {
            if (isInitialized) return;

            if (!backgroundPrefab)
            {
                Debug.LogError("Background prefab is not assigned!");
                return;
            }

            objectPool = gameObject.AddComponent<ObjectPool>();
            var pools = new System.Collections.Generic.List<ObjectPool.Pool>
            {
                new ObjectPool.Pool
                {
                    tag = "Background",
                    prefab = backgroundPrefab,
                    size = poolSize
                }
            };
            objectPool.SetPools(pools);
            objectPool.InitializePool();

            isInitialized = true;
        }

        public GameObject GetBackground(Vector3 position, Quaternion rotation)
        {
            if (!isInitialized)
            {
                Debug.LogError("BackgroundObjectPool is not initialized!");
                return null;
            }

            GameObject background = objectPool.SpawnFromPool("Background", position, rotation);
            if (!background)
            {
                Debug.LogError("Failed to spawn background from pool!");
            }
            return background;
        }

        public void ReturnBackground(GameObject background)
        {
            if (!isInitialized)
            {
                Debug.LogError("BackgroundObjectPool is not initialized!");
                return;
            }

            if (!background)
            {
                Debug.LogError("Cannot return null background to pool!");
                return;
            }

            objectPool.ReturnToPool("Background", background);
        }
    }
} 