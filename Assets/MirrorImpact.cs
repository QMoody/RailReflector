using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorImpact : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Smack();
        } 
    }

    public void Smack()
    {

        //I WILL NEED TO CHANGE THE PARTICLESYSTEM'S ROTATION SO THAT IT FACES OPPOSITE THE MIRROR IT STRIKES.

        gameObject.GetComponent<ParticleSystem>().Emit(Random.Range(5, 10));
    }
}
