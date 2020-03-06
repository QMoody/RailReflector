using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string music = "MainMusic";
    float time = 0;
    float mtime = 120;
    private void Start()
    {
        changeMusic();
        AudioManager.MusicPlay(music, true);
    }

    // rotate musical tracks
    private void Update()
    {

    }

    private void changeMusic()
    {
        
    }
}
