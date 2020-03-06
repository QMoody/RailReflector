using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singletone<LevelManager>
{
    // global player public variables
    public bool player1Active = false;
    public bool player2Active = false;
    public float player1Exp;
    public float player2Exp;
    public int player1Score;
    public int player2Score;
    public float xpMultiplier = 1;
    public float healthScalar = 1;

    public GameObject Player1;
    public GameObject Player1UI;
    public GameObject Player2;
    public GameObject Player2UI;

    // Start is called before the first frame update
    void Awake()
    {
        player1Active = PlayerPrefs.GetInt("Player1") == 0 ? false : true;
        player2Active = PlayerPrefs.GetInt("Player2") == 0 ? false : true;

        if(!player1Active && !player2Active)
        {
            player1Active = true;
        }
        CreatePlayer();
    }

    void CreatePlayer()
    {
        if (player1Active)
        {
            Player1.SetActive(true);
            Player1UI.SetActive(true);
        }

        if (player2Active)
        {
            Player2.SetActive(true);
            Player2UI.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(player1Active)
            {
                return;
            }
            Player1.SetActive(true);
            Player1UI.SetActive(true);
            player1Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (player2Active)
            {
                return;
            }

            Player2.SetActive(true);
            Player2UI.SetActive(true);
            player2Active = true;
        }
    }
}
