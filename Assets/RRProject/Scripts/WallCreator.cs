using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public PlayerController pController;

    [Header("Gameobjects & Components")]
    public GameObject wallPrefab;
    public GameObject nodePrefab;
    public GameObject wallPremakePrefab;

    private GameObject[] wallArray;

    private GameObject wallPredictObj;
    private GameObject placerNode;

    [Header("Wall Variables")]
    public KeyCode createWall;
    public float energyBar;
    public float energyReRate;
    public float maxWallDis;
    public float wallPlaceOffset;
    public int wallNum;
    public int maxWalls;

    public bool canCreateWall;
    public bool createWallActive;

    [Header("Tmp UI")] //Tmp UI
    public GameObject energyBarUI;
    public GameObject rdy1;
    public GameObject rdy2;
    public GameObject rdy3;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        energyBar = 100;
        wallArray = new GameObject[20];
    }

    void Update()
    {
        WallActive();

        if (createWallActive == true)
            PredictWall(false);

        TmpUISend();
    }

    void WallActive()
    {
        if ((Input.GetKeyDown(createWall)) && canCreateWall == true)
        {
            pController.startInmortal();
            createWallActive = true;
            PlaceNode();
            PredictWall(true);
        }
        else if((Input.GetKeyUp(createWall) || wallNum == maxWalls || (energyBar < 33.33f)) && createWallActive == true)
        {
            pController.stopInmortal();
            createWallActive = false;
            if (wallNum < maxWalls)
                CreateNodeWall();

            wallNum = 0;
            Destroy(placerNode);
            Destroy(wallPredictObj);
        }

        if (createWallActive == true && canCreateWall == true && Vector2.Distance(placerNode.transform.position, this.transform.position + new Vector3(0, wallPlaceOffset, 0)) > maxWallDis)
        {
            CreateNodeWall();
            //energyBar -= 33.33f;
        }


        if (createWallActive == false && energyBar < 100)
            energyBar += energyReRate;

        if (energyBar <= 33)
            canCreateWall = false;
        else
            canCreateWall = true;
    }

    void PlaceNode()
    {
        placerNode = Instantiate(nodePrefab, transform.position + new Vector3(0, wallPlaceOffset, 0), transform.rotation);

    }

    void CreateNodeWall()
    {
        Vector3 offset = new Vector3(0, wallPlaceOffset, 0);

        //Set position and rotation before creating the wall
        float wallDis = Vector2.Distance(placerNode.transform.position, this.transform.position + offset);
        Vector3 wallPoint = (placerNode.transform.position - this.transform.position + offset) / 2 + this.transform.position;

        Vector3 rotDir = wallPoint - placerNode.transform.position;
        float wallAngle = Mathf.Atan2(rotDir.y, rotDir.x) * Mathf.Rad2Deg;
        Quaternion wallRot = Quaternion.AngleAxis(wallAngle, Vector3.forward);

        GameObject wall = Instantiate(wallPrefab, wallPoint, wallRot);
        wall.transform.localScale = new Vector2(wallDis, wall.transform.localScale.y);

        for (int i = 0; i < wallArray.Length; i++)
            if (wallArray[i] == null)
            {
                wallArray[i] = wall;
                break;
            }

        placerNode.transform.position = this.transform.position + offset;

        energyBar -= 33.33f;
        wallNum += 1;

        StartCoroutine(WallTimer(5, wall));
    }

    IEnumerator WallTimer(float waitTime, GameObject wall)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(wall);
    }

    void PredictWall(bool isFirst)
    {
        Vector3 offset = new Vector3(0, wallPlaceOffset, 0);

        if (isFirst == true)
        {
            wallPredictObj = Instantiate(wallPremakePrefab);
        }
        float wallDis = Vector2.Distance(placerNode.transform.position, this.transform.position + offset);
        Vector3 wallPoint = (placerNode.transform.position - this.transform.position + offset) / 2 + this.transform.position;
        wallPredictObj.transform.position = wallPoint;

        Vector3 rotDir = wallPoint - placerNode.transform.position;
        float wallAngle = Mathf.Atan2(rotDir.y, rotDir.x) * Mathf.Rad2Deg;
        Quaternion wallRot = Quaternion.AngleAxis(wallAngle, Vector3.forward);
        wallPredictObj.transform.rotation = wallRot;

        wallPredictObj.transform.localScale = new Vector2(wallDis, wallPredictObj.transform.localScale.y);
    }

    //Move this to another script later
    void TmpUISend()
    {
        energyBarUI.GetComponent<RectTransform>().sizeDelta = new Vector2(270 * energyBar / 100, 40);

        if (energyBar >= 100)
            rdy3.SetActive(true);
        else
            rdy3.SetActive(false);

        if (energyBar >= 66.66f)
            rdy2.SetActive(true);
        else
            rdy2.SetActive(false);

        if (energyBar >= 33.33f)
            rdy1.SetActive(true);
        else
            rdy1.SetActive(false);
    }
}
