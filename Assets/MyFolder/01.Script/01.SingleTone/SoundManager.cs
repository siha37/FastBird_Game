using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace MyFolder._01.Script._01.SingleTone
{
    public struct MixerVloumeData
    {
        public MixerVloumeData(string name ,float volume)
        {
            Name = name;
            Volume = volume;
        }
        public string Name;
        public float Volume;
    }
    public class SoundManager : SingleTone<SoundManager>
    {
        public AudioClip bgm;
        AudioSource audioSource;
        [SerializeField] private AudioMixer mixer;
        private void Start()
        {
            if(!TryGetComponent(out audioSource))
                audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = bgm;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void SetMixer(params MixerVloumeData[] volumeName )
        {
            foreach (MixerVloumeData data in volumeName)
            {
                float volume = Mathf.Log10(data.Volume) * 20;
                mixer.SetFloat(data.Name, volume);
            }
        }
    }
}
