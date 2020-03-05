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

    // tracks whether or not there is an open spawn
    // spawns overall
    bool sAvail;

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

    }

    // Update is called once per frame
    void Update()
    {
        // updates the spawn for this frame
        sAvail = checkSpawns();
    }

    // selects a spawner and spawns an enemy 
    public void spawnEnemy(string type)
    {
        // if we are spawning a tank enemy spawn it on the main spawn instantly and return
        if(type == "tank")
        {
            sScripts[3].spawnEnemy(type);
            return;
        }
        // if we are not spawning a tank spawn the enemy at 
        // the first available spawn going from front to back to avoid blocking the tanks
        for(int i = sScripts.Count; i >= 0; i--)
        {
            if (sScripts[i].open)
            {
                sScripts[i].spawnEnemy(type);
            }
        }
    }

    // check if any of the spawns in this spawngroup are open
    public bool checkSpawns()
    {
        int f = 0; 

        // count the number of available spawners not including tanks spawners 
        for (int i = 0; i < sScripts.Count; i++)
        {
            if (sScripts[i].open && !sScripts[i].mSpawn)
            {
                f++;
            }
        }

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

