using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelXPScript : MonoBehaviour
{
    [Header("XP Level Variables")]
    public int playerLevel;
    public float currentXP;
    public float xpToLevel;

    [Header("Tmp UI")] //Tmp UI
    public GameObject levelBar;
    public GameObject levelText;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    void Update()
    {
        UpdateXP();
    }

    void UpdateXP()
    {
        if (currentXP >= xpToLevel)
        {
            currentXP = 0;
            playerLevel += 1;
            levelText.GetComponent<Text>().text = "LVL " + playerLevel;
        }

        levelBar.GetComponent<RectTransform>().sizeDelta = new Vector2(30, currentXP * 2);
        float xpColour = currentXP / xpToLevel * 255;
        levelBar.GetComponent<Image>().color = new Color(xpColour, xpColour, xpColour);
    }

    public void PlayerKill(int enemyId) //Ref this function on the projectiles
    {
        if (enemyId == 0)
            currentXP += 20;

        //etc etc
    }
}
