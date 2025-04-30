using UnityEngine;
using Assets.MyFolder._01.Script._02.Object.Object_Pooling;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.MyFolder._01.Script._02.Object.Background
{
    public class BackgroundSpawner : MonoBehaviour
    {
        [SerializeField] private int backgroundWidth = 30;
        [SerializeField] private int initialBackgroundCount = 3;

        private void Start()
        {
            // 초기 배경 생성
            List<BackgroundScroller> backgroundScrollers = new List<BackgroundScroller>();
            for(int i = 0; i < initialBackgroundCount; i++)
            {
                Vector3 position = new Vector3(i * backgroundWidth, 0, 0);
                GameObject background = BackgroundObjectPool.Instance.GetBackground(position, Quaternion.identity);
                if (background)
                {
                    background.transform.SetParent(transform);
                }
                backgroundScrollers.Add(background.GetComponent<BackgroundScroller>());
                backgroundScrollers[^1].Init();
            }
            for (int i = 0; i < initialBackgroundCount; i++)
            {
                if(i==0)
                {
                    backgroundScrollers[i].frontBackgroundSet(backgroundScrollers[^1].transform);
                }
                else
                {
                    backgroundScrollers[i].frontBackgroundSet(backgroundScrollers[i - 1].transform);
                }
            }
        }

    }
} 