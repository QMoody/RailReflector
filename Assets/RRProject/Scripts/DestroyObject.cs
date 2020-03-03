//-----------------------------------------------------------------------
// <author>
//      David de la Peña [david.dlpf@gmail.com]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy object after X number of seconds
/// </summary>
public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 2;
    [SerializeField] private bool _destroyOnStart = true;


    void Start()
    {
        if (_destroyOnStart)
        {
            DestroyObj();
        }
    }

    public void DestroyObj()
    {
        //Destroy Object after certain time
        Destroy(this.gameObject, _timeToDestroy);
    }
}
