using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletRadar : MonoBehaviour
{
    [SerializeField] private EnemyBase _enemyBase;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            Debug.Log("Bullet incoming outo");
            _enemyBase.Dodge();
        }
    }
}
