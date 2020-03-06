using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelXPScript : MonoBehaviour
{
    [Header("Upgrade Objects")]
    public GameObject upgradeSlot1;
    public GameObject upgradeSlot2;
    public string upgrade1;
    public string upgrade2;

    // store the possible upgrades
    public GameObject lyuda;
    public GameObject explosive;
    public GameObject wiggle;
    public GameObject pen;
    public GameObject akimbo;
    public GameObject damage;
    public GameObject rof;

    [Header("XP Level Variables")]
    public bool isRoundOver;
    public bool canLevelUp;
    public int playerLevel;
    public float currentXP;
    public float xpToLevel;
    public float xpScalar;

    [Header("Tmp UI")] //Tmp UI
    public GameObject levelBar;
    public GameObject levelText;
    public GameObject upgradeTell;
    public Color c1;
    public Color c2;
    private float colourValue;

    public GameObject cannonRack;
    public bool pickedGun;
    public string owner;

    // tracks the last thing upgraded
    public int lastupgraded;

    public List<string> upgrades = new List<string>();

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        setTree();
        upgradeSlot1.SetActive(false);
        upgradeSlot2.SetActive(false);
    }

    void Update()
    {
        if (owner == "player1")
        {
            currentXP = LevelManager.Instance.player1Exp;
        }
        else
        {
            currentXP = LevelManager.Instance.player2Exp;
        }

        UpdateXP();
        CheckForUpgrade();

        displayUpgrades();
    }

    void UpdateXP()
    {

        float tmpExp;

        if (owner == "player1")
        {
            tmpExp = Mathf.Clamp(LevelManager.Instance.player1Exp, 0, xpToLevel);
        }
        else
        {
            tmpExp = Mathf.Clamp(LevelManager.Instance.player2Exp, 0, xpToLevel);
        }
        

        levelBar.GetComponent<Image>().fillAmount = tmpExp / 100;

        //levelBar.GetComponent<RectTransform>().sizeDelta = new Vector2(30, tmpExp * 2);
        //float xpColour = tmpExp / xpToLevel * 255;
        //levelBar.GetComponent<Image>().color = new Color(xpColour, xpColour, xpColour);

        if (owner == "player1")
        {
            if (LevelManager.Instance.player1Exp >= xpToLevel)
            {
                if (isRoundOver == true)
                {
                    colourValue = Mathf.PingPong(Time.time * 10, 1);
                    if (colourValue <= 0.1f)
                        upgradeTell.GetComponent<Text>().color = c1;
                    else if (colourValue >= 0.9f)
                        upgradeTell.GetComponent<Text>().color = c2;
                }

                canLevelUp = true;
            }
            else
            {
                canLevelUp = false;
                upgradeTell.GetComponent<Text>().color = c2;
            }
        }
        else
        {
            if (LevelManager.Instance.player2Exp >= xpToLevel)
            {
                if (isRoundOver == true)
                {
                    colourValue = Mathf.PingPong(Time.time * 10, 1);
                    if (colourValue <= 0.1f)
                        upgradeTell.GetComponent<Text>().color = c1;
                    else if (colourValue >= 0.9f)
                        upgradeTell.GetComponent<Text>().color = c2;
                }

                canLevelUp = true;
            }
            else
            {
                canLevelUp = false;
                upgradeTell.GetComponent<Text>().color = c2;
            }
        }


        
    }

    void CheckForUpgrade()
    {

        if (canLevelUp == true)
        {
            // set the upgrades
            upgrade1 = upgrades[1];
            upgrade2 = upgrades[2];
        }
    }

    void displayUpgrades()
    {
        // if the player can level up, display upgrades
        // otherwise ensure that they are hidden
        if (canLevelUp) {
            switch (upgrade1)
            {
                case "wiggle":
                    wiggle.transform.position = upgradeSlot1.transform.position;
                    wiggle.SetActive(true);
                    break;
                case "lyuda":
                    lyuda.transform.position = upgradeSlot1.transform.position;
                    lyuda.SetActive(true);
                    break;
                case "explosive":
                    explosive.transform.position = upgradeSlot1.transform.position;
                    explosive.SetActive(true);
                    break;
                case "rof":
                    rof.transform.position = upgradeSlot1.transform.position;
                    rof.SetActive(true);
                    break;
                case "pen":
                    pen.transform.position = upgradeSlot1.transform.position;
                    pen.SetActive(true);
                    break;
                case "akimbo":
                    akimbo.transform.position = upgradeSlot1.transform.position;
                    akimbo.SetActive(true);
                    break;
                case "damage":
                    damage.transform.position = upgradeSlot1.transform.position;
                    damage.SetActive(true);
                    break;
            }

            switch (upgrade2)
            {
                case "wiggle":
                    wiggle.transform.position = upgradeSlot2.transform.position;
                    wiggle.SetActive(false);
                    break;
                case "lyuda":
                    lyuda.transform.position = upgradeSlot2.transform.position;
                    lyuda.SetActive(false);
                    break;
                case "explosive":
                    explosive.transform.position = upgradeSlot2.transform.position;
                    explosive.SetActive(false);
                    break;
                case "rof":
                    rof.transform.position = upgradeSlot2.transform.position;
                    rof.SetActive(false);
                    break;
                case "pen":
                    pen.transform.position = upgradeSlot2.transform.position;
                    pen.SetActive(false);
                    break;
                case "akimbo":
                    akimbo.transform.position = upgradeSlot2.transform.position;
                    akimbo.SetActive(false);
                    break;
                case "damage":
                    damage.transform.position = upgradeSlot2.transform.position;
                    damage.SetActive(false);
                    break;
            }
        } else {
            switch (upgrade1)
            {
                case "wiggle":
                    wiggle.transform.position = upgradeSlot1.transform.position;
                    wiggle.SetActive(false);
                    break;
                case "lyuda":
                    lyuda.transform.position = upgradeSlot1.transform.position;
                    lyuda.SetActive(false);
                    break;
                case "explosive":
                    explosive.transform.position = upgradeSlot1.transform.position;
                    explosive.SetActive(false);
                    break;
                case "rof":
                    rof.transform.position = upgradeSlot1.transform.position;
                    rof.SetActive(false);
                    break;
                case "pen":
                    pen.transform.position = upgradeSlot1.transform.position;
                    pen.SetActive(false);
                    break;
                case "akimbo":
                    akimbo.transform.position = upgradeSlot1.transform.position;
                    akimbo.SetActive(false);
                    break;
                case "damage":
                    damage.transform.position = upgradeSlot1.transform.position;
                    damage.SetActive(false);
                    break;
            }

            switch (upgrade2)
            {
                case "wiggle":
                    wiggle.transform.position = upgradeSlot2.transform.position;
                    wiggle.SetActive(true);
                    break;
                case "lyuda":
                    lyuda.transform.position = upgradeSlot2.transform.position;
                    lyuda.SetActive(true);
                    break;
                case "explosive":
                    explosive.transform.position = upgradeSlot2.transform.position;
                    explosive.SetActive(true);
                    break;
                case "rof":
                    rof.transform.position = upgradeSlot2.transform.position;
                    rof.SetActive(true);
                    break;
                case "pen":
                    pen.transform.position = upgradeSlot2.transform.position;
                    pen.SetActive(true);
                    break;
                case "akimbo":
                    akimbo.transform.position = upgradeSlot2.transform.position;
                    akimbo.SetActive(true);
                    break;
                case "damage":
                    damage.transform.position = upgradeSlot2.transform.position;
                    damage.SetActive(true);
                    break;
            }
        }
    }

    public void LevelUp()
    {
        if (owner == "player1")
        {
            LevelManager.Instance.player1Exp -= xpToLevel;
        }
        else
        {
            LevelManager.Instance.player2Exp -= xpToLevel;
        }

        
        playerLevel += 1;
        levelText.GetComponent<Text>().text = "LVL " + playerLevel;
        xpToLevel = xpToLevel* xpScalar;

        
        if (lastupgraded == 1)
        {
            upgrades.Add(upgrade1);
            upgrade1 = "";
            upgrade2 = "";
        }
        else
        {
            upgrades.Add(upgrade2);
            upgrade1 = "";
            upgrade2 = "";
        }
    }

    // sests the tree for the weaponry
    public void setTree()
    {
        // set the skill tree
        if (owner == "player1")
        {
            upgrades.Add("akimbo");
            upgrades.Add("lyuda");
            upgrades.Add("akimbo");
            upgrades.Add("pen");
            upgrades.Add("akimbo");
            upgrades.Add("wiggle");
            upgrades.Add("explosive");
        }
        else
        {
            upgrades.Add("akimbo");
            upgrades.Add("lyuda");
            upgrades.Add("akimbo");
            upgrades.Add("pen");
            upgrades.Add("akimbo");
            upgrades.Add("wiggle");
            upgrades.Add("akimbo");
            upgrades.Add("explosive");

        }
    }

    // detect the players upgrade selections
    public void detectSelection()
    {
        if(canLevelUp)
        {
            if (owner == "player1")
            {
                // set the key down for player one 
                if (Input.GetKeyDown("LeftShift"))
                {
                    cannonRack.GetComponent<CannonRack>().upgrade(upgrade1);
                    LevelUp();
                    lastupgraded = 1; 
                } else if (Input.GetKeyDown("X"))
                {
                    cannonRack.GetComponent<CannonRack>().upgrade(upgrade2);
                    LevelUp();
                    lastupgraded = 2; 
                }               
            }
            else
            {
                // set the key down for player two
                if (Input.GetKeyDown("W"))
                {
                    cannonRack.GetComponent<CannonRack>().upgrade(upgrade1);
                    LevelUp();
                    lastupgraded = 1;
                }
                else if (Input.GetKeyDown("["))
                {
                    cannonRack.GetComponent<CannonRack>().upgrade(upgrade2);
                    LevelUp();
                    lastupgraded = 2;
                }
            }


        }
    }
}
