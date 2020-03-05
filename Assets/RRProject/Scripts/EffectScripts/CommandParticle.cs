using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParticle : MonoBehaviour
{
    // OnHit is 1-3 parts
    // OnDeath is 25-50 parts
    public float time;
    public bool loop;

    private void Awake ()
    {
        if (!loop)
        {
            Destroy(gameObject, time);
        }
    }   
}
