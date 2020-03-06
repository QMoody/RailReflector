using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singletone<LevelManager>
{
    // global player public variables
    public bool player1Active = false;
    public bool player2Active = false;
    public int player1Exp;
    public int player2Exp;
    public int player1Score;
    public int player2Score;

    public GameObject Player1;
    public GameObject Player2;

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
            Instantiate(Player1, new Vector3(-10, -10, 0), Quaternion.identity);
        }

        if (player2Active)
        {
            Instantiate(Player2, new Vector3(10, -10, 0), Quaternion.identity);
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
            Instantiate(Player1, new Vector3(-10, -10, 0), Quaternion.identity);
            player1Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (player2Active)
            {
                return;
            }
            Instantiate(Player2, new Vector3(10, -10, 0), Quaternion.identity);
            player2Active = true;
        }
    }
}
