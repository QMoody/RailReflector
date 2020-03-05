using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : Singletone<AudioManager>
{
    public float backgroundVolume = 1.0f;
    public float soundFxVolume = 1.0f;

    [System.Serializable]
    public class VolumeChangeEvent : UnityEvent<float> { }

    [HideInInspector] public VolumeChangeEvent onBackgroundVolumeChange = new VolumeChangeEvent();
    [HideInInspector] public VolumeChangeEvent onSoundFxVolumeChange = new VolumeChangeEvent();

    private void Awake()
    {
        //Handle Loading here

    }

    public void setBackgroundVolume(float volume)
    {
        if (volume > 1.0f)
        {
            volume = 1.0f;
        }
        else if (volume < 0.0f)
        {
            volume = 0.0f;
        }

        backgroundVolume = volume;
        onBackgroundVolumeChange.Invoke(backgroundVolume);
    }

    public void setSoundFxVolume(float volume)
    {
        if (volume > 1.0f)
        {
            volume = 1.0f;
        }
        else if (volume < 0.0f)
        {
            volume = 0.0f;
        }

        soundFxVolume = volume;
        onSoundFxVolumeChange.Invoke(soundFxVolume);
    }
}
