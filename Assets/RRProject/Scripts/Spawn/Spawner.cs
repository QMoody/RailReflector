using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // variables for storing all enemy spawn types
    public GameObject normal;
    public GameObject kamikaze;
    public GameObject tank;
    public GameObject cerberus;
    public GameObject mirror;


    // tracks whether or not this spawn can spawn tank enemies
    bool mSpawn;

    // tracks whether or not this spawn is occupied
    public bool open;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawns a single enemy of the passed type
    void spawnEnemy(string type)
    {

    }

    // tracks whether or not this spawn is occupied
    private void OnTriggerEnter2D(Collider2D collision)
    {
        open = false;
    }

    // while an enemy is in this spawn set it to false
    private void OnTriggerStay2D(Collider2D collision)
    {
        open = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        open = true;
    }
}
