using UnityEngine;
using Assets.MyFolder._01.Script._02.Object.Object_Pooling;
using NUnit.Framework;
using System.Collections.Generic;
using MyFolder._01.Script._02.Object.BackGround;

namespace Assets.MyFolder._01.Script._02.Object.Background
{
    public class BackgroundSpawner : MonoBehaviour
    {
        [SerializeField] private int backgroundWidth = 30;
        [SerializeField] private int initialBackgroundCount = 3;

        private void Start()
        {
            BaseBackGroundSpawn();
            Layer0_BackGround();
        }

        private void BaseBackGroundSpawn()
        {            
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
                    backgroundScrollers[i].FrontBackgroundSet(backgroundScrollers[^1].transform);
                }
                else
                {
                    backgroundScrollers[i].FrontBackgroundSet(backgroundScrollers[i - 1].transform);
                }
            }
        }

        private void Layer0_BackGround()
        {
            List<BackgroundScroller> backgroundScrollers = new List<BackgroundScroller>();
            for (int i = 0; i < initialBackgroundCount; i++)
            {
                
            }
        }
    }
} 