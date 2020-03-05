using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : Singletone<EnemyBulletManager>
{

    public int maxBullets;

    public List<GameObject> bullets;
    
    void Awake()
    {
        bullets = new List<GameObject>();
    }

    public void addBullet(GameObject bull)
    {
        bullets.Add(bull);
    }

    public void removeBullet(GameObject bull)
    {
        if (bullets.Contains(bull))
        {
            bullets.Remove(bull);
        }
    }

}
