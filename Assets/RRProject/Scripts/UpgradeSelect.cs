using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelect : MonoBehaviour
{
    public GameObject lvlManager;
    public string owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            lvlManager.GetComponent<LevelXPScript>().LevelUp();
        }
    }
}
