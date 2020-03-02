using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCannon : MonoBehaviour
{
    // stores the cannons target vector
    public GameObject target;
    // stores this cannons origin point for bullets
    public GameObject origin;

    // stores this guns firerate
    public float fireRate = 10;

    // stores the relative position of the cannon to its target location
    public Vector2 relativePos;
    // stores the relative rotation of the cannon to its target location
    float relativeRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        point();

        fire();

        
    }
     
    // rotates the cannon to face the target vector
    private void point()
    {
        // store the relative vector between the cannon and its target gameObject
        relativePos = target.transform.position - transform.position;

        // calculate the rotation between the x axis and the relative position
        relativeRotation = - (Mathf.Atan2(relativePos.x, relativePos.y) * Mathf.Rad2Deg);
        //relativeRotation = (Vector2.Angle(transform.position, target.transform.position) * Mathf.Rad2Deg);

        // rotates the cannon to face the target position
        transform.rotation = Quaternion.Euler(0, 0, relativeRotation);
    }

    // fires a bullet toward the target vector 
    private void fire()
    {

    }

}
