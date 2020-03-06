using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWiggle : MonoBehaviour
{
    public GameObject wiggleObject;
    bool isWiggle;
    float wiggleValue;
    public float wiggleMax;
    public float wiggleSpeed;

    void Update()
    {
        if (isWiggle == true)
        {
            wiggleValue = Mathf.PingPong(Time.time * wiggleSpeed, wiggleMax) - wiggleMax / 2;
            wiggleObject.transform.localPosition = new Vector3(wiggleValue, wiggleObject.transform.localPosition.y, wiggleObject.transform.localPosition.z);
        }
    }
}
