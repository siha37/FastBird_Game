using Assets.MyFolder._01.Script._02.Object.Object_Pooling;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Object_Pooling
{
    public class ScorePipeObjectPool : MonoBehaviour
    {
        private static ScorePipeObjectPool _instance;
        public static ScorePipeObjectPool Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindFirstObjectByType<ScorePipeObjectPool>();
                    if (!_instance)
                    {
                        GameObject obj = new GameObject("ScorePipeObjectPool");
                        _instance = obj.AddComponent<ScorePipeObjectPool>();
                    }
                }
                return _instance;
            }
        }
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private int poolSize = 10;
        private ObjectPool objectPool;
        private bool isInitialized = false;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                //DontDestroyOnLoad(gameObject);
                InitializePool();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        private void InitializePool()
        {
            if (isInitialized) return;

            if (!pipePrefab)
            {
                Debug.LogError("Pipe prefab is not assigned!");
                return;
            }

            objectPool = gameObject.AddComponent<ObjectPool>();
            var pools = new System.Collections.Generic.List<ObjectPool.Pool>
            {
                new ObjectPool.Pool
                {
                    tag = pipePrefab.name,
                    prefab = pipePrefab,
                    size = poolSize
                }
            };
            objectPool.SetPools(pools);
            objectPool.InitializePool();

            isInitialized = true;
        }

        public GameObject GetPipe(Vector3 position, Quaternion rotation)
        {
            if (!isInitialized)
            {
                Debug.LogError("PipeObjectPool is not initialized!");
                return null;
            }

            GameObject pipe = objectPool.SpawnFromPool(pipePrefab.name, position, rotation);
            if (!pipe)
            {
                Debug.LogError("Failed to spawn pipe from pool!");
            }
            return pipe;
        }

        public void ReturnPipe(GameObject pipe)
        {
            if (!isInitialized)
            {
                Debug.LogError("PipeObjectPool is not initialized!");
                return;
            }

            if (!pipe)
            {
                Debug.LogError("Cannot return null pipe to pool!");
                return;
            }

            objectPool.ReturnToPool(pipePrefab.name, pipe);
        }
    }
}
