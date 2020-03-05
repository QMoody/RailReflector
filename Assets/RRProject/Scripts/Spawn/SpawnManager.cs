using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawn manager communicates and coordinates spawns accross the spawngroups
public class SpawnManager : MonoBehaviour
{
    // store the spawnGroupPrefab
    public GameObject spawnGroup;
    List<SpawnGroup> sGScripts;

    // stores all premade waves
    public List<Wave> waves;

    // stores all the enemies in a wave
    public List<string> enemies = new List<string>();


    // stores the number of the wave we are on
    int wave;

    // track whether or not a wave has started whether or not we are imbetween waves currently
    bool wInProgress = false;
    bool waiting = true;
    float timer = 0.0f;
    public float timeBetweenWaves = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        wave = 0;

        // setup the spawners 
        for (int i = 0; i < 5; i++)
        {
            // track whether or not this spawn is on the left or the right, and scale i accordingly
            if (i % 2 > 0)
            {
                // create and position the spawn, then store its script for accessing later
                GameObject tSpawn = Instantiate(spawnGroup, transform.position, transform.rotation);
                tSpawn.transform.parent = gameObject.transform;
                tSpawn.transform.localPosition = new Vector2(((i + 1) * 3.75f), 0);
            }
            else
            {
                // create and position the spawn, then store its script for accessing later
                GameObject tSpawn = Instantiate(spawnGroup, transform.position, transform.rotation);
                tSpawn.transform.parent = gameObject.transform;
                tSpawn.transform.localPosition = new Vector2((-i * 3.75f), 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if we are imbetween waves increment the imbetween wave timer
        if (wInProgress == false)
        {
            timer = Time.deltaTime;
        }

        // if time is up set the wave to start on the next frame
        if (timer >= timeBetweenWaves)
        {
            wInProgress = true;
            timer = 0;
        }

        // if there is no active wave begin spawning and continue
        // if the wave is no longer progressing and has been completed structure a new one
        // if a wave has no enemies set it to completed
        if (wInProgress)
        {
            spawnWave();
            // set waiting to false as the wave has started
            waiting = false;
        }
        else if (!wInProgress && !waiting)
        {
            // if this is beyond the final premade wave,
            // then construct the wave using infinite scaling
            if ((waves.Count - 1) < wave)
            {
                wave++;
                structureWave();
                waiting = true;
            }
            else
            {
                wave++;
                infiniteStructure();
                waiting = true;
            }
        }
        else if (enemies.Count <= 0)
        {
            wInProgress = false;
        }
    }

    // spawns a new wave
    void spawnWave()
    {

    }

    // structures the next wave
    void structureWave()
    {
        // for every enemy inside of the wave add them to the wave list
        for(int i = 0; i < waves[wave].normal; i++)
        {
            
        }
        for (int i = 0; i < waves[wave].kamikaze; i++)
        {

        }
        for (int i = 0; i < waves[wave].tank; i++)
        {

        }
        for (int i = 0; i < waves[wave].cerberus; i++)
        {

        }
        for (int i = 0; i < waves[wave].mirror; i++)
        {

        }
    }

    // structure a wave with infinite scaling systems
    void infiniteStructure()
    {

    }

    // rolls the unit type for the next wave
    void rollWave()
    {

    }

    // !! WAVES !!
    // Wave 1
    [System.Serializable]
    public class Wave
    {
        // store the number of enemies in this wave
        public int normal;
        public int kamikaze;
        public int tank;
        public int cerberus;
        public int mirror;
        // scalar that multiplies the enemies health
        public float healthScalar;
    }

}
