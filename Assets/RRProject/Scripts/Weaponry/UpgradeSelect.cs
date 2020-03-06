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

    public GameObject wiggle;
    public GameObject pen;
    public GameObject lyuda;
    public GameObject explosive;
    public GameObject akimbo;
    public GameObject rof;
    public GameObject dam;

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
                crScript.setPen();
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
        string nextup;

        if (upgrades.Count > 0)
        {
            // select next upgrade
            nextup = upgrades[0].name;
            upgrades.RemoveAt(0);
        }
        else
        {
            nextup = infiniteUpgrade;
        }

        switch (nextup)
        {
            case "wiggle":
                wiggle.SetActive(true);
                wiggle.transform.localPosition.Set(0, 0, 0);
                break;
            case "lyuda":
                lyuda.SetActive(true);
                lyuda.transform.localPosition.Set(0, 0, 0);
                break;
            case "explosive":
                explosive.SetActive(true);
                explosive.transform.localPosition.Set(0, 0, 0);
                break;
            case "rof":
                rof.SetActive(true);
                rof.transform.localPosition.Set(0, 0, 0);
                break;
            case "pen":
                pen.SetActive(true);
                pen.transform.localPosition.Set(0, 0, 0);
                break;
            case "akimbo":
                akimbo.SetActive(true);
                akimbo.transform.localPosition.Set(0, 0, 0);
                break;
            case "dam":
                dam.SetActive(true);
                dam.transform.localPosition.Set(0, 0, 0);
                break;
        }
    }

    // resets the display state of upgrades
    void resetUpgrades()
    {
        wiggle.SetActive(false);
        lyuda.SetActive(false);
        explosive.SetActive(false);
        rof.SetActive(false);
        pen.SetActive(false);
        akimbo.SetActive(false);
        dam.SetActive(false);
    }

    [System.Serializable]
    public class upgrade
    {
        // store the number of enemies in this wave
        public string name;
    }
}
