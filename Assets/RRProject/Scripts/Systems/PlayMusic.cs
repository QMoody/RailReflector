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
    }

    // rotate musical tracks
    private void Update()
    {
        time = time + Time.deltaTime;

        if (time >= mtime)
        {

            AudioManager.MusicPlay(music, false);

            // swap songs randomly 
            int r = (int)Random.Range(0, 2.99f);
            if (r == 0)
            {
                music = "Battle Music Grimmish";
                changeMusic();
            } else if(r == 1)
            {
                music = "Battle Music Medium";
                changeMusic();
            } else if(r == 2)
            {
                music = "Battle Music Poppy";
                changeMusic();
            }
        }
    }

    private void changeMusic()
    {
        AudioManager.MusicPlay(music, true);
    }
}
