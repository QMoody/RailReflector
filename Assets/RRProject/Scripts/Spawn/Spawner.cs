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

    public float healthScalar;

    // Start is called before the first frame update
    void Start()
    {
        open = true;
    }

    // Update is called once per frame
    void Update()
    {
        healthScalar = LevelManager.Instance.healthScalar;
    }

    // spawns a single enemy of the passed type
    public void spawnEnemy(string type)
    {
        // stores enemy script for health multipler
        EnemyBase var; 

        // spawn the proper enemy depending on the passed enemy type
        switch (type)
        {
            case "tank":
                var = Instantiate(tank, transform.position, transform.rotation).GetComponent<EnemyBase>();
                var.healthMultiplier(healthScalar);
                break;
            case "normal":
                var = Instantiate(normal, transform.position, transform.rotation).GetComponent<EnemyBase>();
                var.healthMultiplier(healthScalar);
                break;
            case "kamikaze":
                var = Instantiate(kamikaze, transform.position, transform.rotation).GetComponent<EnemyBase>();
                var.healthMultiplier(healthScalar);
                break;
            case "cerberus":
                var = Instantiate(cerberus, transform.position, transform.rotation).GetComponent<EnemyBase>();
                var.healthMultiplier(healthScalar);
                break;
            case "mirror":
                var = Instantiate(cerberus, transform.position, transform.rotation).GetComponent<EnemyBase>();
                var.healthMultiplier(healthScalar);
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
