using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Background
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 5f;
        [SerializeField] private float resetPosition = -20f;
        [SerializeField] private float startPosition = 20f;

        private void Update()
        {
            // 배경을 왼쪽으로 이동
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            // 배경이 화면을 벗어나면 시작 위치로 이동
            if (transform.position.x <= resetPosition)
            {
                Vector3 newPosition = transform.position;
                newPosition.x = startPosition;
                transform.position = newPosition;
            }
        }
    }
} 