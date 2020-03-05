using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    public List<GameObject> particles; 

    public void Spawn(int index)
    {
        Debug.Log(transform.position);
        Instantiate(particles[index], transform.position, Quaternion.identity);
    }

    public void SpawnParent(int index)
    {
        Debug.Log(transform.position);
        GameObject obj = Instantiate(particles[index], transform.position, Quaternion.identity);
        obj.transform.parent = transform;
    }
}
