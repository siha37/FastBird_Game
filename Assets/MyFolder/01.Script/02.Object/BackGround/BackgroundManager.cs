using UnityEngine;
using Assets.MyFolder._01.Script._02.Object.Object_Pooling;

namespace Assets.MyFolder._01.Script._02.Object.Background
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private float backgroundWidth = 30f;
        [SerializeField] private int initialBackgroundCount = 3;

        private void Start()
        {
            // 초기 배경 생성
            for (int i = 0; i < initialBackgroundCount; i++)
            {
                Vector3 position = new Vector3(i * backgroundWidth+ 5, 0, 0);
                GameObject background = BackgroundObjectPool.Instance.GetBackground(position, Quaternion.identity);
                if (background)
                {
                    background.transform.SetParent(transform);
                }
            }
        }

    }
} 