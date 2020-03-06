using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LoadScene loadScene;
    
    void Start()
    {
        PlayerPrefs.SetInt("Player1", 0);
        PlayerPrefs.SetInt("Player2", 0);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("Player1", 1);
            loadScene.LoadNewScene("Tutorial");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("Player2", 1);
            loadScene.LoadNewScene("Tutorial");
        }
    }
}
