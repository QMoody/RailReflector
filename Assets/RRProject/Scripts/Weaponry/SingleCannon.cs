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



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fire();
    }
     
    // rotates the cannon to face the target vector
    private void point()
    {
        // points the cannon to face the transform
        transform.LookAt(target.transform);
    }

    // fires a bullet toward the target vector 
    private void fire()
    {

    }

}
