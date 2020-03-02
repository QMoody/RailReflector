using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basic bullet class
// functions off of recieving a target vector
// uses that target vector to translate its location
// a set distance every frame relative to the amount of time passed
public class Bullet : MonoBehaviour
{

    // used for storing and calculating the projectiles movement
    // every frame
    Vector2 delta;

    // stores the projectiles target path
    Vector2 fPath;

    // controls the max magnitude of the projectile
    public float maxSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        // by defualt projectiles just move straight ahead
        fPath.Set(0, 1);
    }
    
    // spawns a projectile with a set target vector
    void Start(GameObject target)
    {
        // store the target vecotr inside of flight path
        fPath = target.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // update and move the projectile for this frame
        translate();
    }

    // moves the projectile for this frame 
    private void translate()
    {
        // scale this projectiles magnitude to be equal to its max speed this frame
        // calculated from its max speed per second multiplied by the time between frames
        delta = Vector2.ClampMagnitude(fPath, (maxSpeed * Time.deltaTime));

        // rotates the bullet to face its trajectory
        // transform.LookAt(transform.position, new Vector3(delta.x + transform.position.x, delta.y + transform.position.y));

        // translate the projectile accross the screen
        transform.Translate(delta);
    }
}
