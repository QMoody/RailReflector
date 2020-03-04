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
    public bool mSpawn;

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
    public void spawnEnemy(string type)
    {
        // spawn the proper enemy depending on the passed enemy type
        switch (type)
        {
            case "tank":
                Instantiate(tank, transform.position, transform.rotation);
                break;
            case "normal":
                Instantiate(normal, transform.position, transform.rotation);
                break;
            case "kamikaze":
                Instantiate(kamikaze, transform.position, transform.rotation);
                break;
            case "cerberus":
                Instantiate(cerberus, transform.position, transform.rotation);
                break;
            case "mirror":
                Instantiate(cerberus, transform.position, transform.rotation);
                break;
        }
        // set this spawn as occupied
        open = false;
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
