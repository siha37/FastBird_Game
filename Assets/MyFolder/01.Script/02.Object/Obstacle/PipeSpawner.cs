using System.Collections.Generic;
using Assets.MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._02.Object.Object_Pooling;
using MyFolder._01.Script._02.Object.Player;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Obstacle
{
    public class PipeSpawner : MonoBehaviour
    {
        #region Struct
        [System.Serializable]
        private struct RandomSameHeight
        {
            public float probability;
            public int count;
        }
        #endregion

        #region  Variables
        [Header("Spawn Settings")]
        [SerializeField] private float baseSpawnInterval = 2f; // 기본 스폰 간격
        [SerializeField] private float minYPosition = -3f;
        [SerializeField] private float maxYPosition = 3f;
        [SerializeField] private float gapHeight = 4f;
        [SerializeField] private float maxHeightChange = 2f; // 이전 높이와의 최대 차이
        
        public float GetSetbaseSpawnInterval { get { return baseSpawnInterval; } set { baseSpawnInterval = value; } }
        public float GetSetGapHeight { get { return gapHeight; } set { gapHeight = value; } }
        public float GetSetMaxHeightChange { get { return maxHeightChange; } set { maxHeightChange = value; } }
        

        [Header("Random Height Settings")]
        [SerializeField]
        private List<RandomSameHeight> randomHeightList = new List<RandomSameHeight>()
        {
            new RandomSameHeight { probability = 0.1f, count = 4 },  // 10%
            new RandomSameHeight { probability = 0.15f, count = 3 }, // 15%
            new RandomSameHeight { probability = 0.25f, count = 2 }, // 25%
            new RandomSameHeight { probability = 0.5f, count = 1 }   // 50%
        };

        private float currentHeight;
        private float previousHeight; // 이전 높이 저장
        private int remainingSameHeightPipes = 0;
        private Camera mainCamera;
        private float spawnXPosition;
        private Transform lastSpawnXPosition;
        private PlayerController playerController;
        #endregion

        #region Private Methods
        private void Start()
        {
            mainCamera = Camera.main;
            if (!mainCamera)
            {
                Debug.LogError("Main Camera not found!");
                return;
            }

            // PlayerController 찾기
            playerController = FindFirstObjectByType<PlayerController>();
            if (!playerController)
            {
                Debug.LogError("PlayerController not found!");
                return;
            }

            Vector3 rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 0, 0));
            spawnXPosition = rightEdge.x;


            // PipeObjectPool 초기화 확인
            if (!PipeObjectPool.Instance)
            {
                Debug.LogError("PipeObjectPool is not initialized!");
                return;
            }

            // 초기 높이 설정
            currentHeight = Random.Range(minYPosition, maxYPosition);
            previousHeight = currentHeight;

            SpawnPipe();
        }

        private void Update()
        {
            if (!GameManager.Instance.IsGameRunning || !mainCamera ) return;

            if (Mathf.Abs(lastSpawnXPosition.position.x- spawnXPosition) >= baseSpawnInterval)
            {
                SpawnPipe();
            }
        }

        private void SpawnPipe()
        {
            if (remainingSameHeightPipes <= 0)
            {
                // 새로운 높이 계산
                float targetHeight = Random.Range(minYPosition, maxYPosition);
                
                // 이전 높이와의 차이를 제한
                float heightDifference = targetHeight - previousHeight;
                if (Mathf.Abs(heightDifference) > maxHeightChange)
                {
                    // 최대 차이를 넘으면 방향에 따라 제한된 높이로 설정
                    targetHeight = previousHeight + (Mathf.Sign(heightDifference) * maxHeightChange);
                }

                currentHeight = targetHeight;
                previousHeight = currentHeight;
                remainingSameHeightPipes = GetRandomSameHeightCount() - 1;
            }
            else
            {
                remainingSameHeightPipes--;
            }

            SpawnTopPipe();
            SpawnBottomPipe();
            SpawnCollisionPipe();
        }

        private void SpawnTopPipe()
        {
            Vector3 topPipePosition = new Vector3(spawnXPosition, currentHeight + gapHeight / 2, 0);
            GameObject topPipe = PipeObjectPool.Instance.GetPipe(topPipePosition, Quaternion.identity);
            if (!topPipe)
            {
                Debug.LogError("topPipe : NULL");
                return;
            }
            lastSpawnXPosition = topPipe.transform;
            topPipe.transform.localScale = new Vector3(1, 1, 1);
            topPipe.transform.SetParent(transform);
        }

        private void SpawnBottomPipe()
        {
            Vector3 bottomPipePosition = new Vector3(spawnXPosition, currentHeight - gapHeight / 2, 0);
            GameObject bottomPipe = PipeObjectPool.Instance.GetPipe(bottomPipePosition, Quaternion.identity);
            if (!bottomPipe)
            {
                Debug.LogError("bottomPipe : NULL");
                return;
            }
            bottomPipe.transform.localScale = new Vector3(1, -1, 1);
            bottomPipe.transform.SetParent(transform);
        }

        private void SpawnCollisionPipe()
        {
            Vector3 spawnPosition = new Vector3(spawnXPosition,currentHeight);
            GameObject collisionPipe = ScorePipeObjectPool.Instance.GetPipe(spawnPosition, Quaternion.identity);
            collisionPipe.transform.localScale = new Vector3(1, 1, 1);
            collisionPipe.transform.SetParent(transform);
        }

        private int GetRandomSameHeightCount()
        {
            float random = Random.value;
            float accumulatedProbability = 0f;

            for (int i = 0; i < randomHeightList.Count; i++)
            {
                accumulatedProbability += randomHeightList[i].probability;
                if (random < accumulatedProbability)
                {
                    return randomHeightList[i].count;
                }
            }

            return randomHeightList[randomHeightList.Count - 1].count;
        }
        #endregion
    }
} 