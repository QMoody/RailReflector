using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    public static EnemyBulletManager Instance;

    public int maxBullets;

    public List<GameObject> bullets;
    
    void Awake()
    {
        Instance = this;
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
