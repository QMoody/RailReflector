using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionShield : MonoBehaviour
{
    public GameObject projectileTmp;
    private PlayerShield playShield;

    private void Start()
    {
        playShield = transform.parent.GetComponent<PlayerShield>();
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == projectileTmp)
        {
            Destroy(collision.gameObject);
            playShield.ShieldHit();

            Debug.Log("Hit shield");
        }

        
        if (collision.gameObject.GetComponent<Bullet>().owner == "Enemy")
        {
            Destroy(collision.gameObject);
            ShieldHit();
        }
        
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit shield");

        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            playShield.ShieldHit();

        }
    }
}
