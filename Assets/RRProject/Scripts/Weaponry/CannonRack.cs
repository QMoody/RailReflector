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
    public int startingCannonCount = 2;

    // spacing between the cannons
    public float cannonSpacing = 0.5f;

    // create an arraylist to store all the cannons in this cannon rack
    public List<GameObject> cannons = new List<GameObject>();

    // track the owner of the projectile
    public string owner = "Player1";

    public KeyCode fireKey;

    public bool lyuda = false;
    public bool explosive = false;
    public bool wiggleShot = false;
    public int multishot = 1;


    // Start is called before the first frame update
    void Start()
    {
        if (owner == "player1")
        {
            startingCannonCount += 2;
        }

        // create the cannon rack with the starting number of cannons
        addCannons(startingCannonCount);

    }

    // Update is called once per frame
    void Update()
    {
        // runs the cannon cheat inputs
        UpdateCannonCheats();

        // update the cannoncs
        updateCannons();
    }

    // adds more cannons to the existing Cannnon rack
    void addCannons(int cNum)
    {
        // add the specified number of cannons to the cannon rack
        for (int i = 0; i < cNum; i++)
        {
            // add a new cannon to the mech
            cannons.Add(Instantiate(cannon, transform.localPosition, transform.rotation));
            // parent the last added cannon to the cannon rack
            cannons[(cannons.Count - 1)].transform.parent = gameObject.transform;
            cannons[(cannons.Count - 1)].transform.GetChild(0).GetComponent<SingleCannon>().owner = owner;
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
            cannons.Add(Instantiate(cannon, transform.localPosition, transform.rotation));
            // parent the last added cannon to the cannon rack
            cannons[(cannons.Count - 1)].transform.parent = gameObject.transform;
            cannons[(cannons.Count - 1)].transform.GetChild(0).GetComponent<SingleCannon>().owner = owner;
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
                }
                else
                {
                    // if this is an even cnnon place it on the right hand side
                    cannons[i].transform.localPosition = new Vector2((- cannonSpacing * (i + 1)), 0);
                }
            }
        }
    }

    //update the cannons
    void updateCannons()
    {
        for(int i = 0; i < cannons.Count; i++)
        {
            cannons[i].transform.GetChild(0).GetComponent<SingleCannon>().explosive = explosive;
            cannons[i].transform.GetChild(0).GetComponent<SingleCannon>().lyuda = lyuda;
            cannons[i].transform.GetChild(0).GetComponent<SingleCannon>().multishot = multishot;
            cannons[i].transform.GetChild(0).GetComponent<SingleCannon>().fireInput(fireKey);
            cannons[i].transform.GetChild(0).GetComponent<SingleCannon>().setWS(wiggleShot);
            cannons[i].transform.GetChild(0).GetComponent<SingleCannon>().assignOwnership(owner);
        }

    }

    // upgrade the cannons 
    public void upgrade(string upgrade1)
    {
        switch (upgrade1)
        {
            case "wiggle":
                wiggleShot = true;
                break;
            case "lyuda":
                lyuda = true;
                break;
            case "explosive":
                explosive = true;
                break;
            case "rof":
                for (int i = 0; i < cannons.Count; i++)
                {
                    cannons[i].GetComponent<SingleCannon>().boostRof();
                }
                break;
            case "pen":
                for (int i = 0; i < cannons.Count; i++)
                {
                    cannons[i].GetComponent<SingleCannon>().setPen();
                }
                break;
            case "akimbo":
                addCannons();
                break;
            case "damage":
                for (int i = 0; i < cannons.Count; i++)
                {
                    cannons[i].GetComponent<SingleCannon>().boostDamage();
                }
                break;
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

        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Z))
        {
            lyuda = true;
        }

        if(Input.GetKeyDown(KeyCode.K)|| Input.GetKeyDown(KeyCode.LeftShift))
        {
            explosive = true;
        }

        if (Input.GetKeyDown(KeyCode.J)|| Input.GetKeyDown(KeyCode.X))
        {
            multishot++;
        }
    }

}
