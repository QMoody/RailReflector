using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swell : MonoBehaviour
{
    public float Timetomax;
    public float Timetomin;
    public float maxscale;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log("AA");
        if (timer < Timetomax)
        {
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(0,0,0), new Vector3(maxscale,maxscale, 1), timer / Timetomax);
        }

        if(timer > Timetomax)
        {
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(maxscale,maxscale,1), new Vector3(0,0,0), timer-Timetomax/(Timetomin - Timetomax));
        }
    }
}
