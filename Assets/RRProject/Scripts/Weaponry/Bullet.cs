﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basic bullet class
// functions off of recieving a target vector
// uses that target vector to translate its location
// a set distance every frame relative to the amount of time passed
public class Bullet : MonoBehaviour
{
    // stores the projectiles delta for this frame
    Vector2 delta;

    // stores the projectiles target path
    Vector2 fPath = new Vector2(0, 2);

    // set the maximum range this projectile can travel
    public float mRange = 25.0f;
    public float dTraveled = 0.0f;
    public float maxSpeed = 0.25f;

    // sets the damage value of bullets
    public float damage = 1.0f;

    // SPECIAL BULLET PROPERTIES
    // tracks whether or not this is a lyuda bullet
    public bool lyuda = false;
    public float lyudaRange = 4.0f;
    public int lyudaShot = 2;

    // tracks whether or not this bullet is explosive
    public bool explosive = false;
    public int shrapcount = 10;
    public float halfShrapCone = 35;
    public float shrapRange = 7;

    // tracks whether or not this bullet has pen
    public bool pen = false;
    public int penStr = 0;

    // stores a copy of the bullet prefab for creating additional projectiles
    public GameObject sBullet;
    public GameObject shrapnel;

    // track the owner of the projectile
    public string owner = "Player1";

    // Reflection Variables
    [Header("Reflector Variables")]
    public LayerMask layReflectMask;
    public float rayWallDis; //0.01?
    //public int reflectMax;
    //public int reflectNum;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        // check if this object should be culled this frame before we run hit detection
        cRange();
    }

    // Update is called once per frame
    private void Update()
    {
        translate();

        // activate lyuda if needed
        if (lyuda && dTraveled >= lyudaRange)
        {
            splitBullet(lyudaShot);
        }

        // activate lyuda if needed
        if (explosive && dTraveled >= lyudaRange * 3)
        {
            explosiveShot();
        }

        Reflect();
    }

    // moves the projectile for this frame 
    private void translate()
    {
        // scale this projectiles magnitude to be equal to its max speed this frame
        // calculated from its max speed per second multiplied by the time between frames
        delta = Vector2.ClampMagnitude(fPath, (maxSpeed * Time.deltaTime));
        dTraveled = dTraveled + delta.magnitude;
        transform.Translate(delta);
    }

    // checks to see if this projectile has traveled out of its max range
    private void cRange()
    {
        // if the distance this projectile has traveled is greater then its max range
        // destroy this projectile
        if(dTraveled > mRange)
        {
            Destroy(gameObject);
        }

    }

    // split bullet, splits the bullet into multiple projectiles which carry the properties
    // of the root bullet.
    private void splitBullet(int multishot)
    {
        // spawn two bullets, each with a different set rotation relative to the initial bullet
        for (int i = 0; i < multishot; i++)
        {
            // calculate the offset z rotation of the multishot projectiles
            float tempZ = (transform.localEulerAngles.z) - (((15 * (multishot - 1)) / 2) - (15 * i));

            // create a new bullet that continues on with a 15 Degree positive offset
            GameObject nb1 = Instantiate(sBullet, transform.position, Quaternion.Euler(0, 0, tempZ));
            Bullet cBullet = nb1.GetComponent<Bullet>();

            // set that bullets new properties
            cBullet.owner = owner;
            cBullet.explosive = explosive;
            cBullet.pen = pen;
            cBullet.penStr = penStr;

            // if this bullet is a lyuda bullet, set all bullets it creates to be non lyuda bullets
            if(lyuda)
            {
                cBullet.lyuda = false;
            }

            cBullet.damage = damage;
        }

        // destroy the current bullet
        Destroy(gameObject);
    }

    // explosive shot spawns shrapnel and deletes the bullet
    private void explosiveShot()
    {
        // spawn two bullets, each with a different set rotation relative to the initial bullet
        for (int i = 0; i < shrapcount; i++)
        {
            // calculate the offset z rotation of the multishot projectiles
            float tempZ = Random.Range(-halfShrapCone, halfShrapCone);

            // create a new bullet that continues on with a 15 Degree positive offset
            GameObject nb1 = Instantiate(shrapnel, transform.position, Quaternion.Euler(0, 0, tempZ));
            Shrapnel sShrap = nb1.GetComponent<Shrapnel>();

            // set the shrapnels properties
            sShrap.owner = owner;
            sShrap.mRange = Random.Range(0, shrapRange);
            sShrap.maxSpeed = Random.Range(0, maxSpeed * 2);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Debug.Log(collision.tag);
            damageable.reciveDamage(transform.forward, (int)damage, GetComponent<Collider2D>().tag);
        }
    }

    void Reflect()
    {
        //Chang this to a cricle collider raycast eventually
        RaycastHit2D hit = Physics2D.Raycast(transform.position, fPath, rayWallDis, layReflectMask);

        if (hit.collider != null)
        {
            fPath = Vector2.Reflect(fPath, hit.normal);
            /*
            reflectNum += 1;
            if (reflectNum >= reflectMax)
                Destroy(this.gameObject);
                
            Debug.DrawLine(hit.normal, hit.normal * 0.25f, Color.green);
            */
        }
    }
}
