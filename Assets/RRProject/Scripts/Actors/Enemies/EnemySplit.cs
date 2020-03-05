using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplit : EnemyBase
{
    [SerializeField] private GameObject _enemySplitChild;

    void Update()
    {
        locomotion();
    }

    public void Split()
    {
        EnemyBase emy = Instantiate(_enemySplitChild, transform.position, Quaternion.identity).GetComponent<EnemyBase>();

        if(emy != null)
        {
            emy.sideSpeed = 4.0f;
        }

        emy = Instantiate(_enemySplitChild, transform.position, Quaternion.identity).GetComponent<EnemyBase>();

        if (emy != null)
        {
            emy.sideSpeed = -4.0f;
        }
    }
}
