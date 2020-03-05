using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEnemyCannon : MonoBehaviour
{
    // stores the cannons target vector
    public GameObject target;
    // store the bullet prefab for this cannon
    public GameObject bullet;

    // stores this guns firerate
    public float fireRate = 10;
    float reFireTime;
    public int multishot = 1;

    // tracks whether or not the player is trying to fire
    public bool fire = false;
    bool firing = false;
    public float damage = 1.0f;

    // stores the relative position of the cannon to its target location
    public Vector2 relativePos;
    // stores the relative rotation of the cannon to its target location
    float relativeRotation;


    // Upgrades
    // tracks whether or not this is a lyuda bullet
    public bool lyuda = false;
    // tracks whether or not this bullet is explosive
    public bool explosive = false;
    // tracks whether or not this bullet has pen
    public bool pen = false;
    // tracks pen value
    public int penStr = 0;

    // track the owner of the projectile
    public string owner = "Player1";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        // calculate the rate of fire for this weapon
        reFireTime = 1 / fireRate;

        // make sure the cannon is pointing toward its target empty gObject
        point();

        // call fire every frame to update it
        fireState();
    }

    // rotates the cannon to face the target vector
    private void point()
    {
        // store the relative vector between the cannon and its target gameObject
        relativePos = target.transform.position - transform.position;

        // calculate the rotation between the x axis and the relative position
        relativeRotation = -(Mathf.Atan2(relativePos.x, relativePos.y) * Mathf.Rad2Deg);
        //relativeRotation = (Vector2.Angle(transform.position, target.transform.position) * Mathf.Rad2Deg);

        // rotates the cannon to face the target position
        transform.rotation = Quaternion.Euler(0, 0, relativeRotation);
    }

    // !! SET ENEMY FIRE CONDITIONS HERE !! 
    private void fireState()
    {
        /*
        // if the player is inputing to fire1 set firing to true
        // and start the firing coroutine
        if (Input.GetButton("Fire1"))
        {
            // set fire to true 
            fire = true;
            if (!firing)
            {
                // fire them bullets baby
                StartCoroutine(spawnBullet());
                firing = true;
            }
        }
        else
        {
            // set whether or not we are trying to fire to false
            fire = false;
            // set whether or not we are firing to be false
            firing = false;
        }
        */
    }

    // fires a bullet while the player is attempting to fire
    private IEnumerator spawnBullet()
    {
        // while the player is firing spawn a bullet, wait the refire time, and loop
        while (fire)
        {
            // spawn two bullets, each with a different set rotation relative to the initial bullet
            for (int i = 0; i < multishot; i++)
            {
                // calculate the offset z rotation of the multishot projectiles
                float tempZ = (transform.localEulerAngles.z) - (((15 * (multishot - 1)) / 2) - (15 * i));

                // create a new bullet that continues on with a 15 Degree positive offset
                GameObject nb1 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, tempZ));
                Bullet cBullet = nb1.GetComponent<Bullet>();

                // set that bullets new properties
                cBullet.owner = owner;
                cBullet.explosive = explosive;
                cBullet.pen = pen;
                cBullet.penStr = penStr;
                cBullet.lyuda = lyuda;
                cBullet.damage = damage;
            }

            // wait the cannons refire time before firing again
            yield return new WaitForSeconds(reFireTime);
        }
    }


    // sets the pen value for this bullet
    // tPenV is how many times this bullet will penetrate
    public void setPen(int tPenV)
    {
        pen = true;
        penStr = tPenV;
    }
    // runs without taking the penetration value and sets it to 1 by defualt
    public void setPen()
    {
        pen = true;
        penStr = 1;
    }

    // sets the bullet to be explosive
    public void setExplosive()
    {
        explosive = true;
    }

    // sets whether or not this bullet is a lyuda round
    public void setLyuda()
    {
        lyuda = true;
    }

}
