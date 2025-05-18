using System;
using MyFolder._01.Script._01.SingleTone;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MyFolder._01.Script._02.Object.PostProcessing
{
    public class PostProcessingSetting : MonoBehaviour
    {
        Volume postProcessing;
        
        Bloom bloom;

        private void Start()
        {
            postProcessing = GetComponent<Volume>();
            SettingSaveManager.Instance.PostProcessCallBack += Setting;
        }

        public void Setting()
        {
            if (postProcessing.profile.TryGet(out bloom))
            {
                bloom.active = SettingSaveManager.Instance.settingData.bloom;
            }
        }
    }
}
