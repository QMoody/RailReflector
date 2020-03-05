using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// spawn manager communicates and coordinates spawns accross the spawngroups
public class SpawnManager : MonoBehaviour
{
    // stores the number of the wave we are on
    public int wave;
    public string e2s;

    // variables for controlling spawns over the course of a waves generation 
    public float spawnBurstDelay = 0.2f;
    public float bSpawnChance = 20;
    public float dTimer;
    public float sDelay;
    // tracks the delay for reset after a burst spawn
    public float aSDelay;
    public float waveLength;
    public float sdTimer;
    int activePlayers;
    int mBurstSize;
    int burstSize;

    // tracks whether or not we are spawning a tank
    public bool tSpawn = false;
    // track whether or not we have spawned a tank 
    bool bSpawn = false;

    bool previousSpawnTank = false;

    // track whether or not a wave has started whether or not we are imbetween waves currently
    public bool wInProgress = false;
    public bool waiting = true;
    public float timer = 0.0f;
    public float timeBetweenWaves = 5.0f;

    // variables for controlling infinite spawns


    // store the spawnGroupPrefab
    public GameObject spawnGroup;
    public List<SpawnGroup> sGScripts = new List<SpawnGroup>();
    public List<SpawnGroup> aSpawnScripts = new List<SpawnGroup>();


    // stores all premade waves
    public List<Wave> waves;

    // stores all the enemies in a wave
    public List<string> enemies = new List<string>();

    // Start is called before the first frame update
    void Start()
    {

        // set number of active players
        activePlayers = (LevelManager.Instance.player1Active ? 1 : 0) + (LevelManager.Instance.player2Active ? 1 : 0);

        wave = -1;

        // setup the spawners 
        for (int i = 0; i < 5; i++)
        {
            // track whether or not this spawn is on the left or the right, and scale i accordingly
            // because of spawn positioning inside of the away, spawns 0-2 are located in the center of the screen
            if (i % 2 > 0)
            {
                // create and position the spawn, then store its script for accessing later
                GameObject tSpawn = Instantiate(spawnGroup, transform.position, transform.rotation);
                // store a ref to this spawns script 
                sGScripts.Add(tSpawn.GetComponent<SpawnGroup>());
                tSpawn.transform.parent = gameObject.transform;
                tSpawn.transform.localPosition = new Vector2(((i + 1) * 3.75f), 0);
            }
            else
            {
                // create and position the spawn, then store its script for accessing later
                GameObject tSpawn = Instantiate(spawnGroup, transform.position, transform.rotation);
                // store a ref to this spawns script 
                sGScripts.Add(tSpawn.GetComponent<SpawnGroup>());
                tSpawn.transform.parent = gameObject.transform;
                tSpawn.transform.localPosition = new Vector2((-i * 3.75f), 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.player1Active || LevelManager.Instance.player2Active)
        {
            // check the current waves conditionals
            waveConditionals();

            // begin current frame spawn logic
            calcWaveSpawn();
        }
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
                    // set waiting to false as the wave has started
                    waiting = false;

                }
            }
            else
            {
                if (sdTimer >= sDelay)
                {
                    spawnWave();
                    // set waiting to false as the wave has started
                    waiting = false;
                }
            }
        }
        else if (!wInProgress && waiting)
        {
            // if this is beyond the final premade wave,
            // then construct the wave using infinite scaling
            if ((waves.Count - 1) >= wave)
            {
                wave++;
                structureWave();
                waiting = false;
            }
            else
            {
                wave++;
                infiniteStructure();
                waiting = false;
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
        e2s = enemies[0];
        enemies.RemoveAt(0);
        int ranSpam = 0;

        if (e2s == "tank")
        {
            tSpawn = true;
        }

        // controls tank spam spawn
        bool tank = false;

        // calculate if we are going to start a burst spawn
        // do not start a burst spawn during one, or during a tankspawn recusive spawn phase
        if ((bSpawnChance >= (int)Random.Range(0, 100)) && !bSpawn && !tSpawn)
        {
            bSpawn = true;
            sDelay = spawnBurstDelay;
        }

        // spawn an enemy
        // if tha enemy is a tank, do a tank spawn
        if (e2s == "tank")
        {

            // check and set spawn availability variables for this frame
            checkGroupSpawns();

            // spawn a tank at a random spawn with an available tank spawn
            if (activePlayers < 2)
            {
                ranSpam = (int)Random.Range(0, aSpawnScripts.Count - 0.01f); ;
                aSpawnScripts[ranSpam].spawnEnemy(e2s);
            }
            else
            {
                ranSpam = (int)Random.Range(0, aSpawnScripts.Count - 0.1f);
                aSpawnScripts[ranSpam].spawnEnemy(e2s);
            }

            // spawn additional units to support the currrent tank if the next unit is not a tank
            for(int i = 0; i < 3; i++)
            {
                if (enemies[0] != "tank")
                {
                    spawnBackUpUnits(ranSpam);
                } else
                {
                    break;
                }
            }
        }
        else
        {

            tSpawn = false;

            // check and set spawn availability variables for this frame
            checkGroupSpawns();

            // spawn a the enemy at a random available spawn
            if (activePlayers < 2)
            {
                ranSpam = (int)Random.Range(0, aSpawnScripts.Count - 0.01f); ;
                spawn(ranSpam, e2s);
            }
            else
            {
                ranSpam = (int)Random.Range(0, aSpawnScripts.Count - 0.1f);
                spawn(ranSpam, e2s);
            }
        }

        // reset e2s
        e2s = " ";
    }

    // back up units spawn behind the last tank to spawn
    void spawnBackUpUnits(int ranSpam)
    {
        // stores the first enemy to spawn, then removes it from the list
        e2s = enemies[0];
        enemies.RemoveAt(0);
        aSpawnScripts[ranSpam].spawnEnemy(e2s);
        sDelay += sDelay - spawnBurstDelay;
    }

    // spawns the enemy
    void spawn(int spawn, string type)
    {
        // spawns a new enemy
        aSpawnScripts[spawn].spawnEnemy(type);

        if (bSpawn & !tSpawn)
        {
            // ensure we scale up the next spawn delay by the difference between the standard
            // delay and the burst spawn delay
            sDelay += sDelay - spawnBurstDelay;
        }
        else
        {
            //if this was not a burst spawn ensure that sDelay is properly reset
            sDelay = aSDelay;
        }

        // if this is a burst spawn reduce the burstspawnenemies left count
        // if the count is 0 set bSpawn to false and reset the burstpsawn enemies count
        if (bSpawn)
        {
            burstSize--;
        }
        if (burstSize <= 0)
        {
            burstSize = mBurstSize;
            bSpawn = false;
        }

        // reset spawn timer
        sdTimer = 0;
    }

    // structures the next wave
    void structureWave()
    {
        //EnemyBulletManager.Instance.maxBullets = waves[wave].mBullets;

        // set the wave duration
        waveLength = waves[wave].waveDuration;

        bool singleTSpawn = false;

        // for every enemy inside of the wave add them to the wave list
        // do this once if there is one player, and do this twice if there are two players
        for (int j = 0; j < activePlayers; j++)
        {
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
                singleTSpawn = false;
            }
            else
            {
                for (int i = 0; i < waves[wave].cerberus; i++)
                {
                    enemies.Add("cerberus");
                }
                singleTSpawn = true;
            }

            // for boss style enemeies, when there is only one, ensure they are placed at the end of the wave
            if (waves[wave].tank == 1)
            {
                singleTSpawn = false;
            }
            else
            {
                for (int i = 0; i < waves[wave].tank; i++)
                {
                    enemies.Add("tank");
                }
                singleTSpawn = true;
            }

            // controls the delay between individual enemy spawns
            // optimal time between non burst spawns
            sDelay = waveLength / enemies.Count;
            // store the sDelay so it can get reset later
            aSDelay = sDelay;

            spawnBurstDelay = sDelay / 4;

            // max burst spawn unit density
            mBurstSize = (int)(enemies.Count / 10);
            burstSize = mBurstSize;
        }

        // randomize the list
        rollWave(enemies);

        // if we have not spawned bosses as this their first appearance, spawn them now at the end of the wave
        if (!singleTSpawn)
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

        // if a wave is in progress scale the time between spawn timer 
        if (wInProgress)
        {
            // burst spawn delay timer
            sdTimer += Time.deltaTime;
        }

        // check if a new player has joined
        if (activePlayers < 2)
        {
            activePlayers = (LevelManager.Instance.player1Active ? 1 : 0) + (LevelManager.Instance.player2Active ? 1 : 0);
        }

        // check if the wave is still in progress
        if (wInProgress && enemies.Count <= 0)
        {
            wInProgress = false;
        }

        // if we are imbetween waves increment the imbetween wave timer
        if (wInProgress == false && enemies.Count <= 0)
        {
            timer += Time.deltaTime;
            waiting = true;
        }

        // increment time
        if (wInProgress == false)
        {
            timer += Time.deltaTime;
        }

        // if time is up set the wave to start on the next frame
        if (timer >= timeBetweenWaves && !wInProgress)
        {
            wInProgress = true;
            timer = 0;
        }

        // clear the entire available spawn script list
        if (aSpawnScripts != null)
        {
            aSpawnScripts.Clear();
        }

    }

    // checks which group spawns are available for spawning enemies
    void checkGroupSpawns()
    {
        // check which spawn groups are available 
        // then randomly select a spawn
        for (int i = 0; i < sGScripts.Count; i++)
        {
            // if one player is active then spawn on only the middle three spawns
            // otherwise spawn utilizing all spawns
            if (activePlayers < 2)
            {
                // cycle through the spawns and set the proper spawns to available or not
                for (int k = 0; k < 3; k++)
                {
                    // if it is a tank spawn check if a tank spawn is available 
                    if (tSpawn)
                    {
                        if (sGScripts[k].tankSpawnAvailable)
                        {
                            // add the spawn script to the available spawns list
                            aSpawnScripts.Add(sGScripts[k]);
                        }
                    }
                    else
                    {
                        if (sGScripts[k].sAvail)
                        {
                            // add the spawn script to the available spawns list
                            aSpawnScripts.Add(sGScripts[k]);
                        }
                    }
                }
            }
            else
            {
                // cycle through the spawns and set the proper spawns to available or not
                for (int k = 0; k < 5; k++)
                {
                    // if it is a tank spawn check if a tank spawn is available 
                    if (tSpawn)
                    {
                        if (sGScripts[k].tankSpawnAvailable)
                        {
                            // add the spawn script to the available spawns list
                            aSpawnScripts.Add(sGScripts[k]);
                        }
                    }
                    else
                    {
                        if (sGScripts[k].sAvail)
                        {
                            // add the spawn script to the available spawns list
                            aSpawnScripts.Add(sGScripts[k]);
                        }
                    }
                }
            }
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
        public int normal = 0;
        public int kamikaze = 0;
        public int tank = 0;
        public int cerberus = 0;
        public int mirror = 0;
        public int mBullets = 0;
        public int waveDuration = 0;
        // scalar that multiplies the enemies health
        public float healthScalar = 1;
    }

}
