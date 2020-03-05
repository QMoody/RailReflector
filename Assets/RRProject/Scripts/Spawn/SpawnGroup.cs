using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour
{

    // store all the spawners
    // b - back spawn
    // m - middle row spawn
    // f = front row spawn
    public GameObject bSpawnL;
    public GameObject bSpawnM;
    public GameObject bSpawnR;
    public GameObject mSpawn;
    public GameObject fSpawnL;
    public GameObject fSpawnM;
    public GameObject fSpawnR;

    // track all avaible scripts
    Spawner bSLScript;
    Spawner bSMScript;
    Spawner bSRScript;
    Spawner mSScript;
    Spawner fSLScript;
    Spawner fSMScript;
    Spawner fSRScript;

    // store all spawners and spawns in lists
    List<Spawner> sScripts = new List<Spawner>();
    List<Spawner> aSScripts = new List<Spawner>();

    // tracks whether or not there is an open spawn
    // spawns overall
    public bool sAvail;

    // checks if a tank was spawned this frame
    bool tankSpawnedThisFrame = false;

    // stores if we can spawn a tank on this spawner
    public bool tankSpawnAvailable = true;

    // sotres if the backspawn is open
    public bool backSpawnOpen;

    // tracks the number of empty spawns
    int emptySpawns;

    public float healthScalar;

    // Start is called before the first frame update
    void Start()
    {
        // store the scripts in game objects so we dont have to repeatedly getComponenet on em bitches
        bSLScript = bSpawnL.GetComponent<Spawner>();
        bSMScript = bSpawnM.GetComponent<Spawner>();
        bSRScript = bSpawnR.GetComponent<Spawner>();
        mSScript = mSpawn.GetComponent<Spawner>();
        fSLScript = fSpawnL.GetComponent<Spawner>();
        fSMScript = fSpawnM.GetComponent<Spawner>();
        fSRScript = fSpawnR.GetComponent<Spawner>();

        // add all the scripts to the script array
        sScripts.Add(bSLScript);
        sScripts.Add(bSMScript);
        sScripts.Add(bSRScript);
        sScripts.Add(mSScript);
        sScripts.Add(fSLScript);
        sScripts.Add(fSMScript);
        sScripts.Add(fSRScript);

        // set the main spawner to the main spawner
        sScripts[3].mSpawn = true;

    }

    // Update is called once per frame
    void Update()
    {

        // clear the available spawns every frame
        aSScripts.Clear(); 

        // reset this value every frame
        tankSpawnedThisFrame = false;
        tankSpawnAvailable = false;

        // updates the spawn for this frame
        sAvail = checkSpawns();
    }

    // selects a spawner and spawns an enemy 
    public void spawnEnemy(string type)
    {
        // if we are spawning a tank enemy spawn it on the main spawn instantly and return
        if(type == "tank")
        {
            tankSpawnedThisFrame = true;
            sScripts[3].spawnEnemy(type);
            return;
        }

        // spawn here 
        if(tankSpawnedThisFrame)
        {
            // spawn to the back spawner on this spawngroup
            for (int i = 0; i < 3; i++)
            {
                if (sScripts[i].open)
                {
                    sScripts[i].spawnEnemy(type);
                    break;
                }
            }
            return;
        }

        int ranSpan = (int)Random.Range(0, aSScripts.Count - 0.01f);
        aSScripts[ranSpan].spawnEnemy(type);

        checkSpawns();
    }

    // check if any of the spawns in this spawngroup are open
    public bool checkSpawns()
    {
        int f = 0;
        bool z = false;

        // checks if the tank spawn is open
        if(sScripts[3].open)
        {
            tankSpawnAvailable = true;
        }

        // count the number of available spawners not including tank and back spawners 
        for (int i = 3; i < sScripts.Count; i++)
        {
            if (sScripts[i].open && !sScripts[i].mSpawn)
            {
                f++;
                // add the available spawner script to the spawn scripts
                aSScripts.Add(sScripts[i]);
            }
        }

        // check if the backspawns are available and set the conditional
        for (int i = 0; i < 3; i++)
        {
            if (sScripts[i].open)
            {
                z = true;
            }
        }

        backSpawnOpen = z;


        // counts the number of empty spawns
        emptySpawns = f;

        // if the number of available spawn is over 1 then a spawn is available
        if(f > 1)
        {
            return true;
        }

        // if there is not return false
        return false;
    }

    // checks to see if this groups large spawn is available 
    public bool checkMainSpawn()
    {
        return mSScript.open;
    }
}

