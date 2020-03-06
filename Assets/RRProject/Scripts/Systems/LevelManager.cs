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

    public bool player1Firing = false;
    public bool player2Firing = false;
    public float player1refiretime = .25f;
    public float player2refiretime = 1f;

    public GameObject Player1;
    public GameObject Player1UI;
    public GameObject Player2;
    public GameObject Player2UI;

    // Start is called before the first frame update
    void Awake()
    {
        player1Active = PlayerPrefs.GetInt("Player1") == 0 ? false : true;
        player2Active = PlayerPrefs.GetInt("Player2") == 0 ? false : true;

        if (!player1Active && !player2Active)
        {
            player1Active = true;
        }
        CreatePlayer();

        //StartCoroutine(gunNoises());
       // StartCoroutine(biggunnoises());
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
            if (player1Active)
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

    // updates which player is firing
    public void updatePlayerFiring(string i, bool shooty)
    {
        if (i == "player1")
        {
            player1Firing = shooty;
            Debug.Log("hello" + shooty + "hi");
        }
        else
        {
            player2Firing = shooty;
        }
    }

    private IEnumerator gunNoises()
    {
        int i;

        // while the gun be pew pew pew let it pew pew pew
        while (player1Firing)
        {
            i = (int)Random.Range(0, 2.99f);

            switch (i)
            {
                case 0:
                    AudioManager.PlaySFX("Machgun3");
                    break;
                case 1:
                    AudioManager.PlaySFX("Machgun2");
                    break;
                case 2:
                    AudioManager.PlaySFX("Machgun1");
                    break;
            }
            yield return new WaitForSeconds(player1refiretime);
        }
    }

    private IEnumerator biggunnoises()
    {
        int i;
        while (player2Firing)
        {
            i = (int)Random.Range(0, 1.99f);

            switch (i)
            {
                case 0:
                    AudioManager.PlaySFX("Shotgun1");
                    break;
                case 1:
                    AudioManager.PlaySFX("Shotgun2");
                    break;
            }



            yield return new WaitForSeconds(player2refiretime);
        }
    }
}
