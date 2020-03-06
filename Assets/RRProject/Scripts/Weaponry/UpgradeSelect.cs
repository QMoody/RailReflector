using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelect : MonoBehaviour
{
    // stores all premade waves
    public List<upgrade> upgrades;

    // the upgrade it infinitely offers
    public string infiniteUpgrade;

    // the cannon rack this upgrade select is linked to 
    public GameObject cannonRack;
    public CannonRack crScript;

    private void Start()
    {
        crScript = cannonRack.GetComponent<CannonRack>();   
    }
    private void Update()
    {
        updateUpgrade();
    }
    // selects this upgrade boxes upgrade and sends it to the correct player
    public void selectUpgrade()
    {
        string nextup;

        if (upgrades.Count > 0)
        {
            // select next upgrade
            nextup = upgrades[0].name;
            upgrades.RemoveAt(0);
        } else
        {
            nextup = infiniteUpgrade;
        }


        switch (nextup)
        {
            case "wiggle":
                crScript.setWS();
                break;
            case "lyuda":
                crScript.setLyuda();
                break;
            case "explosive":
                crScript.setExplosive();
                break;
            case "rof":
                crScript.rof();
                break;
            case "pen":

                break;
            case "akimbo":
                crScript.addCannons();
                break;
            case "dam":
                crScript.dam();
                break;
        }
    }

    // sets new displayed upgrade
    void updateUpgrade()
    {

    }

    [System.Serializable]
    public class upgrade
    {
        // store the number of enemies in this wave
        public string name;
    }
}
