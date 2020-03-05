using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeder : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase emy = collision.GetComponent<EnemyBase>();

        if(emy != null)
        {
            if(_speed > emy.speed)
            {
                emy.speed = _speed;
            }
        }
    }
}
