using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawn manager communicates and coordinates spawns accross the spawngroups
public class SpawnManager : MonoBehaviour
{
    // stores all premade waves
    public List<Wave> waves;
    int pWCount;

    // stores the number of the wave we are on
    int wave;

    // Start is called before the first frame update
    void Start()
    {
        // number of waves
        pWCount += waves.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // if this is beyond the final premade wave, construct the wave using infinite scaling
        if((waves.Count - 1) < wave)
        {
            structureWave();
        }
    }

    // spawns a new wave
    void spawnWave()
    {

    }

    // structures the next wave
    void structureWave()
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
