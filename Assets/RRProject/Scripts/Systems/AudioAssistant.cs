using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioAssistant : MonoBehaviour
{
   
    public enum AudioType
    {
        SoundFx,
        Background
    };
    public AudioType audioType = AudioType.SoundFx;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        switch(audioType)
        {
            case AudioType.Background:
                AudioManager.Instance.onBackgroundVolumeChange.AddListener(onVolumeChange);
                audioSource.volume = AudioManager.Instance.backgroundVolume;
                break;

            case AudioType.SoundFx:
                AudioManager.Instance.onSoundFxVolumeChange.AddListener(onVolumeChange);
                audioSource.volume = AudioManager.Instance.soundFxVolume;
                break;
        }
    }

    void onVolumeChange(float volume)
    {
        audioSource.volume = volume;
    }

    private void OnDestroy()
    {
        switch (audioType)
        {
            case AudioType.Background:
                AudioManager.Instance.onBackgroundVolumeChange.RemoveListener(onVolumeChange);
                break;

            case AudioType.SoundFx:
                AudioManager.Instance.onSoundFxVolumeChange.RemoveListener(onVolumeChange);
                break;
        }
    }
}
