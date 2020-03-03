using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParticle : MonoBehaviour
{
    // OnHit is 1-3 parts
    // OnDeath is 25-50 parts

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Burst(25, 50);
        }
    }

    public void Burst(int Min, int Max)
    {
        gameObject.GetComponent<ParticleSystem>().Emit(Random.Range(Min, Max));
    }

}
