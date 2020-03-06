using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour
{
    public GameObject shieldObject;
    Collider2D shieldCollider;
    public float curShieldCharge;
    public float shieldChargeRate;

    public GameObject shieldUIObj;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        shieldCollider = shieldObject.GetComponent<CircleCollider2D>();
        curShieldCharge = 255;
    }

    void Update()
    {
        ShieldCharge();
    }

    void ShieldCharge()
    {
        if (curShieldCharge < 255)
            curShieldCharge += shieldChargeRate;

        Color tmpClolour = shieldObject.GetComponent<SpriteRenderer>().color;

        if (curShieldCharge >= 255)
        {
            tmpClolour.a = 1;
            shieldUIObj.GetComponent<Image>().fillAmount = 1;
        }
        else if (curShieldCharge >= 200)
        {
            tmpClolour.a = 0.75f;
            shieldUIObj.GetComponent<Image>().fillAmount = 0.8f;
        }
        else if (curShieldCharge >= 135)
        {
            tmpClolour.a = 0.5f;
            shieldUIObj.GetComponent<Image>().fillAmount = 0.55f;
        }
        else
            shieldUIObj.GetComponent<Image>().fillAmount = 0;

        shieldObject.GetComponent<SpriteRenderer>().color = tmpClolour;

        if (curShieldCharge >= 135)
            shieldObject.SetActive(true);
        else if (curShieldCharge < 135)
            shieldObject.SetActive(false);
    }

    public void ShieldHit()
    {
        if (curShieldCharge < 200)
            curShieldCharge = 0;
        else if (curShieldCharge < 255)
            curShieldCharge = 135;
        else if (curShieldCharge >= 255)
            curShieldCharge = 200;
    }
}
