using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.Audio;

namespace MyFolder._01.Script._01.SingleTone
{
    [Serializable]
    public class SettingData
    {
        public bool bloom;
        public int framerate;
        public string accountName;
        public string accountId;
        public float bgmVolume;
        public float sfxVolume;
    }
    public class SettingSaveManager : SingleTone<SettingSaveManager>
    {        
        private static string SavePath => Path.Combine(Application.persistentDataPath, "setting.json");

        public const string MIXER_BGM = "MusicVolume";
        public const string MIXER_SFX = "SfxVolume";
        public const string MIXER_UI = "UiVolume";
        
        public SettingData settingData;
        public string AcountImgUrl
        {
            set { StartCoroutine(nameof(DownloadImage), value); }
        }
        
        public Sprite acountImg;

        public delegate void VoidDle();
        public VoidDle PostProcessCallBack = null;
        public VoidDle MixerCallBack = null;
        
        
        private IEnumerator DownloadImage(string url)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("프로필 이미지 다운로드 실패: " + uwr.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    acountImg = Sprite.Create(texture, rect, pivot);
                }
            }
        }

        protected override void Awake()
        {
            settingData = LoadSetting();
            SoundManager.Instance.SetMixer(
                new MixerVloumeData(MIXER_BGM, settingData.bgmVolume),
                new MixerVloumeData(MIXER_SFX, settingData.bgmVolume),
                new MixerVloumeData(MIXER_UI, settingData.bgmVolume)
                );
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = settingData.framerate;
        base.Awake();
        }

        public void SaveSetting()
        {
            SaveSetting(settingData);
        }
        private void SaveSetting(SettingData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
        }

        private static SettingData LoadSetting()
        {
            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                return JsonUtility.FromJson<SettingData>(json);
            }

            // 기본값 반환
            return new SettingData { bgmVolume = 1.0f, bloom = true, framerate = 60 };
        }
    }
}