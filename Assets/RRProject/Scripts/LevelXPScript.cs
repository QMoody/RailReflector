using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelXPScript : MonoBehaviour
{
    [Header("Upgrade Objects")]
    public GameObject upgradeSlot1;
    public GameObject upgradeSlot2;

    [Header("XP Level Variables")]
    public bool isRoundOver;
    public bool canLevelUp;
    public int playerLevel;
    public float currentXP;
    public float xpToLevel;

    [Header("Tmp UI")] //Tmp UI
    public GameObject levelBar;
    public GameObject levelText;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        upgradeSlot1.SetActive(false);
        upgradeSlot2.SetActive(false);
    }

    void Update()
    {
        UpdateXP();
        CheckForUpgrade();
    }

    void UpdateXP()
    {
        float tmpExp = Mathf.Clamp(currentXP, 0, xpToLevel);

        levelBar.GetComponent<RectTransform>().sizeDelta = new Vector2(30, tmpExp * 2);
        float xpColour = tmpExp / xpToLevel * 255;
        levelBar.GetComponent<Image>().color = new Color(xpColour, xpColour, xpColour);

        if (currentXP >= xpToLevel)
            canLevelUp = true;
        else
            canLevelUp = false;
    }

    void CheckForUpgrade()
    {
        if (isRoundOver == true && canLevelUp == true)
        {
            upgradeSlot1.SetActive(true);
            upgradeSlot2.SetActive(true);
        }
        else
        {
            upgradeSlot1.SetActive(false);
            upgradeSlot2.SetActive(false);
        }
    }

    public void LevelUp()
    {
        currentXP -= xpToLevel;
        playerLevel += 1;
        levelText.GetComponent<Text>().text = "LVL " + playerLevel;
    }

    public void PlayerKill(int enemyId) //Ref this function on the projectiles
    {
        if (enemyId == 0)
            currentXP += 20;

        //etc etc
    }
}
