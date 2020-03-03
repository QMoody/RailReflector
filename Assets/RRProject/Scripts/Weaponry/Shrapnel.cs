using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : MonoBehaviour
{
    // stores the projectiles delta for this frame
    Vector2 delta;

    // stores the projectiles target path
    Vector2 fPath = new Vector2(0, 1);

    // set the maximum range this projectile can travel
    public float mRange = 6.0f;
    public float dTraveled = 0.0f;
    public float maxSpeed = 0.25f;

    // track how long this shrapnel is alive\
    public float tlifetime;
    public float mLifetime = 5.0f;

    // sets the damage value of bullets
    public float damage = 1.0f;

    // track the owner of the projectile
    public string owner = "Player1";

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
        tlifetime = tlifetime + Time.deltaTime;
        translate();
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
        // check if this projectile has traveled its max range
        if (dTraveled > mRange)
        {
            Destroy(gameObject);
        }

        // check if this projectile has been alive too long
        if(tlifetime > mLifetime)
        {
            Destroy(gameObject);
        }

    }
}
