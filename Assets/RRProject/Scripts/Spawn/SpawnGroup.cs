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
    public GameObject bSpawnR;
    public GameObject mSpawn;
    public GameObject fSpawnL;
    public GameObject fSpawnR;

    // track all avaible scripts
    Spawner bSLScript;
    Spawner bSRScript;
    Spawner mSScript;
    Spawner fSLScript;
    Spawner fSRScript;

    // store all spawners and spawns in lists
    List<Spawner> sScripts = new List<Spawner>();
    List<GameObject> spawns = new List<GameObject>();

    // tracks whether or not there is an open spawn
    // spawns overall
    bool sAvail;

    // Start is called before the first frame update
    void Start()
    {
        // store the scripts in game objects so we dont have to repeatedly getComponenet on em bitches
        bSLScript = bSpawnL.GetComponent<Spawner>();
        bSRScript = bSpawnR.GetComponent<Spawner>();
        mSScript = mSpawn.GetComponent<Spawner>();
        fSLScript = fSpawnL.GetComponent<Spawner>();
        fSRScript = fSpawnR.GetComponent<Spawner>();

        // add all the scripts to the script array
        sScripts.Add(bSLScript);
        sScripts.Add(bSRScript);
        sScripts.Add(mSScript);
        sScripts.Add(fSLScript);
        sScripts.Add(fSRScript);

        // add all spawns to the spawn array
        spawns.Add(bSpawnL);
        spawns.Add(bSpawnR);
        spawns.Add(mSpawn);
        spawns.Add(fSpawnL);
        spawns.Add(fSpawnR);





    }

    // Update is called once per frame
    void Update()
    {
        // reset spawn availability



        // updates the spawn for this frame
        checkSpawns();
    }








    // selects a spawner and spawns an enemy 
    public void spawnEnemy(string type)
    {

    }








    // check if any of the spawns in this spawngroup are open
    public bool checkSpawns()
    {
        // check if there is an avaible spawner in this spawn group
        for (int i = 0; i < sScripts.Count; i++)
        {
            if (!sScripts[i].open)
            {
                return true;
            }
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

