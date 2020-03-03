using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThick : EnemyBase
{
    [SerializeField] private GameObject _speeder;

    void Update()
    {
        locomotion();
    }
    
    public void CreateSpeeder()
    {
        Instantiate(_speeder, transform.position, Quaternion.identity);
    }
}
