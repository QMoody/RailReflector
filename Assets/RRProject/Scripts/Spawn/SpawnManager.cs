using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// spawn manager communicates and coordinates spawns accross the spawngroups
public class SpawnManager : MonoBehaviour
{
    // stores the number of the wave we are on
    int wave;

    // variables for controlling spawns over the course of a waves generation 
    public float spawnBurstDelay = 0.2f;
    public float bSpawnChance;
    public float dTimer;
    public float sDelay;
    public float waveLength;
    public float sdTimer;
    int activePlayers;
    int mBurstSize;
    int BurstSize;
    // minimum increment of the dTimer when spawning an enemy
    public float dTMIncrement;
    // tracks whether or not we are spawning a tank
    public bool tSpawn = false;
    // track whether or not we have spawned a tank 
    bool bSpawn = false;


    // track whether or not a wave has started whether or not we are imbetween waves currently
    bool wInProgress = false;
    bool waiting = true;
    float timer = 0.0f;
    public float timeBetweenWaves = 5.0f;

    // variables for controlling infinite spawns


    // store the spawnGroupPrefab
    public GameObject spawnGroup;
    List<SpawnGroup> sGScripts;

    // stores all premade waves
    public List<Wave> waves;

    // stores all the enemies in a wave
    public List<string> enemies = new List<string>();

    // Start is called before the first frame update
    void Start()
    {

        // set number of active players
        activePlayers = 0.1f;

        wave = 0;

        // setup the spawners 
        for (int i = 0; i < 5; i++)
        {
            // track whether or not this spawn is on the left or the right, and scale i accordingly
            // because of spawn positioning inside of the away, spawns 0-2 are located in the center of the screen
            if (i % 2 > 0)
            {
                // create and position the spawn, then store its script for accessing later
                GameObject tSpawn = Instantiate(spawnGroup, transform.position, transform.rotation);
                tSpawn.transform.parent = gameObject.transform;
                tSpawn.transform.localPosition = new Vector2(((i + 1) * 3.75f), 0);
                // store a ref to this spawns script
                sGScripts.Add(tSpawn.GetComponent<SpawnGroup>());
            }
            else
            {
                // create and position the spawn, then store its script for accessing later
                GameObject tSpawn = Instantiate(spawnGroup, transform.position, transform.rotation);
                tSpawn.transform.parent = gameObject.transform;
                tSpawn.transform.localPosition = new Vector2((-i * 3.75f), 0);
                // store a ref to this spawns script 
                sGScripts.Add(tSpawn.GetComponent<SpawnGroup>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        // if a wave is in progress scale the time between spawn timer 
        if (wInProgress)
        {
            // burst spawn delay timer
            sdTimer += Time.deltaTime;
        }

        // begin current frame spawn logic
        calcWaveSpawn();
    }

    // calculates wave spawns
    void calcWaveSpawn()
    {
        // if there is no active wave begin spawning and continue
        // if the wave is no longer progressing and has been completed structure a new one
        // if a wave has no enemies set it to completed
        if (wInProgress)
        {
            // if this is a burst spawn check the burst spawn delay 
            // if this is not a burst spawn check the spawn delay timer
            if (bSpawn)
            {
                if (sdTimer >= spawnBurstDelay)
                {
                    spawnWave();
                    // reset spawn timer
                    sdTimer = 0;
                    // set waiting to false as the wave has started
                    waiting = false;
                }
            }
            else
            {
                if (sdTimer >= sDelay)
                {
                    spawnWave();
                    // reset spawn timer
                    sdTimer = 0;
                    // set waiting to false as the wave has started
                    waiting = false;
                }
            }
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
        // stores the first enemy to spawn, then removes it from the list
        string e2p = enemies[0];
        enemies.RemoveAt(0);

        // calculate if we are going to start a burst spawn
        if ((bSpawnChance == (int)Random.Range(0, 100)) && !bSpawn)
        {
            bSpawn = true;
        }

        // spawn an enemy
        // if tha enemy is a tank, do a tank spawn
        if (e2p == "tank")
        {
            tSpawn = true;
            // check which spawn groups are available 
            // then randomly select a spawn
            for(int i = 0; i < sGScripts.Count; i++)
            {
                // if one player is active then spawn on only the middle three spawns
                // otherwise spawn utilizing all spawns
                if()
                {

                } else
                {

                }
            }

        } else
        {

        }


    }

    // structures the next wave
    void structureWave()
    {
        EnemyBulletManager.Instance.maxBullets = waves[wave].mBullets;


        // controls the delay between individual enemy spawns
        // optimal time between non burst spawns
        sDelay = waveLength / enemies.Count;
        // max burst spawn unit density
        mBurstSize = (int)(enemies.Count / 10);

        // for every enemy inside of the wave add them to the wave list
        for (int i = 0; i < waves[wave].normal; i++)
        {
            enemies.Add("normal");
        }
        for (int i = 0; i < waves[wave].kamikaze; i++)
        {
            enemies.Add("kamikaze");
        }
        for (int i = 0; i < waves[wave].mirror; i++)
        {
            enemies.Add("mirror");
        }
        // for boss style enemeies, when there is only one, ensure they are placed at the end of the wave
        if (waves[wave].cerberus == 1)
        {
            bSpawn = false;
        }
        else
        {
            for (int i = 0; i < waves[wave].cerberus; i++)
            {
                enemies.Add("cerberus");
            }
            bSpawn = true;
        }

        // for boss style enemeies, when there is only one, ensure they are placed at the end of the wave
        if (waves[wave].tank == 1)
        {
            bSpawn = false;
        }
        else
        {
            for (int i = 0; i < waves[wave].tank; i++)
            {
                enemies.Add("tank");
            }
            bSpawn = true;
        }
        // randomize the list
        rollWave(enemies);

        // if we have not spawned bosses as this their first appearance, spawn them now at the end of the wave
        if (!bSpawn)
        {
            // check that the boss is supposed to spawn first then spawn the bosses
            if (waves[wave].cerberus == 1)
            {
                enemies.Add("cerberus");
            }

            if (waves[wave].tank == 1)
            {
                enemies.Add("tank");
            }
        }

    }

    // structure a wave with infinite scaling systems
    void infiniteStructure()
    {

    }


    // checks the current waves current state conditionals 
    void waveConditionals()
    {
        // check if the wave is still in progress
        if (wInProgress && enemies.Count <= 0)
        {
            wInProgress = false;
        }

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

    }

    // rolls the unit type for the next wave
    // SHUFFLE CODE PROVIDED BY GRENADE ON STACKOVERFLOW
    // https://stackoverflow.com/questions/273313/randomize-a-listt
    public static List<string> rollWave(List<string> sList)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = sList.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            string value = sList[k];
            sList[k] = sList[n];
            sList[n] = value;
        }

        // return the shuffled list
        return sList;
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
        public int mBullets;
        // scalar that multiplies the enemies health
        public float healthScalar;
    }

}
