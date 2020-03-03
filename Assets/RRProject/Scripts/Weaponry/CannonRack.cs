using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines a rack of player cannons for means of
// positioning multiple cannons, as well as upgrading cannons
public class CannonRack : MonoBehaviour
{

    // store the cannon object
    public GameObject cannon;

    // Starting cannon count
    public int startingCannonCount = 1;

    // spacing between the cannons
    public float cannonSpacing = 0.5f;

    // create an arraylist to store all the cannons in this cannon rack
    public List<GameObject> cannons = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        // create the cannon rack with the starting number of cannons
        addCannons(startingCannonCount);
    }

    // Update is called once per frame
    void Update()
    {
        // runs the cannon cheat inputs
        UpdateCannonCheats();
    }

    // adds more cannons to the existing Cannnon rack
    void addCannons(int cNum)
    {
        // add the specified number of cannons to the cannon rack
        for (int i = 0; i < cNum; i++)
        {
            // add a new cannon to the mech
            cannons.Add(Instantiate(cannon, transform.position, transform.rotation));
            // parent the last added cannon to the cannon rack
            cannons[(cannons.Count - 1)].transform.parent = gameObject.transform;
        }

        // sets the cannons positions
        positionCannons();
    }

    // adds 2 more cannons to the existing Cannnon rack
    void addCannons()
    {
        // add the specified number of cannons to the cannon rack
        for (int i = 0; i < 2; i++)
        {
            // add a new cannon to the mech
            cannons.Add(Instantiate(cannon, transform.position, transform.rotation));
            // parent the last added cannon to the cannon rack
            cannons[(cannons.Count - 1)].transform.parent = gameObject.transform;
        }

        // sets the cannons positions
        positionCannons();
    }

    // repositions the cannons within the cannon rack
    // only needs to be called when new cannons are added to the rack
    void positionCannons()
    {
        // if we have an even number of cannons arrange them accordingly
        // otherwise we have one and set it to 0, 0 in the center of the player
        if (cannons.Count > 1)
        {
            // arrange the cannons on either side of the mech
            for(int i = 0; i < cannons.Count; i++)
            {
                if ((i % 2) > 0) {
                    // if this is an odd cannon place it on the left hand side
                    cannons[i].transform.localPosition = new Vector2((cannonSpacing * i), 0);
                } else
                {
                    // if this is an even cnnon place it on the right hand side
                    cannons[i].transform.localPosition = new Vector2((- cannonSpacing * i), 0);
                }
            }
        }  else
        {

        }
    }

    // reangles the cannons to either foward fire or shotgun
    void setCannnonAngle()
    {


    }

    // runs the cheats for cannons
    void UpdateCannonCheats()
    {
        // if they are adding cannons add cannons dawg shit
        if(Input.GetButtonDown("AddCannon"))
        {
            addCannons();
        }
    }

}
