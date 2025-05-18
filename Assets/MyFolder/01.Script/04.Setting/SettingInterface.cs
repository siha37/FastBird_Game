using MyFolder._01.Script._01.SingleTone;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MyFolder._01.Script._04.Setting
{
    public class SettingInterface : MonoBehaviour
    { 
        private SoundManager soundManager;
        AudioMixer mixer;

        public bool motionBlur = true;

        [SerializeField] private Toggle bloomToggle;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private TextMeshProUGUI acountName;
        [SerializeField] private Image acountProfileImg;
        
        const string MIXER_BGM = SettingSaveManager.MIXER_BGM;
        const string MIXER_SFX = SettingSaveManager.MIXER_SFX;
        const string MIXER_UI = SettingSaveManager.MIXER_UI;
        
        
        void Awake()
        {
            bloomToggle?.onValueChanged.AddListener(SetBloom);
            bgmSlider?.onValueChanged.AddListener(SetBgmVolume);
            sfxSlider?.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnEnable()
        {
            bloomToggle.isOn = SettingSaveManager.Instance.settingData.bloom;
            bgmSlider.value = SettingSaveManager.Instance.settingData.bgmVolume;
            sfxSlider.value = SettingSaveManager.Instance.settingData.sfxVolume;
            acountName.text = SettingSaveManager.Instance.settingData.accountName;
            acountProfileImg.sprite = SettingSaveManager.Instance.acountImg;
        }

        void SetBgmVolume(float volume)
        {
            SettingSaveManager.Instance.settingData.bgmVolume = volume;
            SettingSaveManager.Instance.SaveSetting();
            SoundManager.Instance.SetMixer(new MixerVloumeData(MIXER_BGM,volume));
            SettingSaveManager.Instance.MixerCallBack?.Invoke();
        }

        void SetSfxVolume(float volume)
        {
            SettingSaveManager.Instance.settingData.sfxVolume = volume;
            SettingSaveManager.Instance.SaveSetting();
            SoundManager.Instance.SetMixer(new MixerVloumeData(MIXER_SFX,volume));
            SettingSaveManager.Instance.MixerCallBack?.Invoke();
        }

        void SetBloom(bool blur)
        {
            SettingSaveManager.Instance.settingData.bloom = blur;
            SettingSaveManager.Instance.SaveSetting();
            SettingSaveManager.Instance.PostProcessCallBack?.Invoke();
        }
    }
}
