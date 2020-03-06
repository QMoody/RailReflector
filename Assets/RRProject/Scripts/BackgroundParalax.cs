using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParalax : MonoBehaviour
{
    public float speed;

    public float resetPos;
    public float resetToPos;
    
    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.y <= resetPos)
        {
            transform.position = new Vector3(transform.position.x, resetToPos, transform.position.z);
        }
    }
}
