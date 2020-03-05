using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWall : MonoBehaviour
{
    public Sprite wall1;
    public Sprite wall2;
    public Sprite wall3;
    public Sprite wall4;
    public Sprite wall5;

    public float wallHealth;
    public LoadScene lScene;


    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        wallHealth = 100;
    }

    void Update()
    {
        CheckWallState();
    }

    void CheckWallState()
    {
        if (wallHealth > 80)
            GetComponent<SpriteRenderer>().sprite = wall1; //.color = new Color(0, wallHealth / 100, 1);
        else if (wallHealth > 60)
            GetComponent<SpriteRenderer>().sprite = wall2;
        else if (wallHealth > 40)
            GetComponent<SpriteRenderer>().sprite = wall3;
        else if (wallHealth > 20)
            GetComponent<SpriteRenderer>().sprite = wall4;
        else if (wallHealth <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = wall5;
            //GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            lScene.LoadNewScene("Score");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Maybe check for enemy type that does more damage?

            wallHealth -= 5;
        }
    }
}
