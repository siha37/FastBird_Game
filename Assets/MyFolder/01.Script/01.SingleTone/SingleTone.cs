using UnityEngine;

namespace MyFolder._01.Script._01.SingleTone
{
    public class SingleTone<T> : MonoBehaviour where T : SingleTone<T>
    {
        protected static T instance;
        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindFirstObjectByType<T>();
                    if (!instance)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                        DontDestroyOnLoad(obj);
                    }
                }
                return instance;
            }
        }
        protected virtual void Awake()
        {
            if (instance == null || instance == (T)this)
            {
                instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
