using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    // tracks where the upgrades are displayed
    // script on an upgrade slot will select and display the correct upgrades
    public GameObject upgradeSlot1;
    public GameObject upgradeSlot2;

    // stores a notification for the player that they have an upgrade waiting
    public GameObject notification;

    public string owner;

    public bool upgradeReady;

    // counts the number of upgrades the player has
    public int upgradeCount;
    public float upgradePow;

    public float upgradeScalar = 2;

    // tracks how much the next upgrade costs in EXP
    public float upgradeCost = 75;

    // tracks your experience and gives you upgrades incrementally
    void Start()
    {
        
    }

    //
    void Update()
    {
        // gets the amount of EXP the player has this frame
        if (owner == "player1") {
            upgradePow = LevelManager.Instance.player1Exp;
        } else
        {
            upgradePow = LevelManager.Instance.player2Exp;
        }
        checkCurrentXP();
    }


    // check how much XP the player has
    void checkCurrentXP()
    {
        if(upgradePow >= upgradeCost)
        {
            upgradeReady = true;
            detectUpgradeInput();
        }
    }

    // checks which upgrade the player would like to take
    // calls the correct upgrade cell and subtracts the cost of upgrading from the player
    // and from the level manager
    void detectUpgradeInput()
    {
        if (owner == "player1")
        {
            // set the key down for player one 
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                upgradeSlot1.GetComponent<UpgradeSelect>().selectUpgrade();
                chargeUpgradeManager();
                upgradeCount++;
                upgradeCost = upgradeCost * upgradeScalar;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                upgradeSlot2.GetComponent<UpgradeSelect>().selectUpgrade();
                chargeUpgradeManager();
                upgradeCount++;
                upgradeCost = upgradeCost * upgradeScalar;
            }
        }
        else
        {
            // set the key down for player two
            if (Input.GetKeyDown(KeyCode.W))
            {
                upgradeSlot1.GetComponent<UpgradeSelect>().selectUpgrade();
                chargeUpgradeManager();
                upgradeCount++;
                upgradeCost = upgradeCost * upgradeScalar;
            }
            else if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                upgradeSlot2.GetComponent<UpgradeSelect>().selectUpgrade();
                chargeUpgradeManager();
                upgradeCount++;
                upgradeCost = upgradeCost * upgradeScalar;
            }
        }
    }

    // charges upgrade manager the upgrade cost
    void chargeUpgradeManager()
    {
        if (owner == "player1") {
            LevelManager.Instance.player1Exp = LevelManager.Instance.player1Exp - upgradeCost;
            upgradePow = LevelManager.Instance.player1Exp;
        } else {
            LevelManager.Instance.player2Exp = LevelManager.Instance.player1Exp - upgradeCost;
            upgradePow = LevelManager.Instance.player2Exp;
        }
    }
}
