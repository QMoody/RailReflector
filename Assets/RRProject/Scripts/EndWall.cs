using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWall : MonoBehaviour
{
    public float wallHealth;


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
        if (wallHealth > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, wallHealth / 100, 1);
        }
        else if (wallHealth <= 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
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
