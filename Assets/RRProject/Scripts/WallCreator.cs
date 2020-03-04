using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [Header("Gameobjects & Components")]
    public GameObject wallPrefab;
    public GameObject nodePrefab;
    public GameObject wallPremakePrefab;

    private GameObject[] wallArray;

    private GameObject wallPredictObj;
    private GameObject placerNode;

    [Header("Wall Variables")]
    public float energyBar;
    public float energyReRate;
    public float maxWallDis;

    bool canCreateWall;
    bool createWallActive;

    [Header("Tmp UI")] //Tmp UI
    public GameObject energyBarUI;
    public GameObject rdy1;
    public GameObject rdy2;
    public GameObject rdy3;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        energyBar = 100;
        wallArray = new GameObject[3];
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
        if (Input.GetKeyDown("e") && canCreateWall == true)
        {
            createWallActive = true;
            PlaceNode();
            PredictWall(true);
        }
        else if(Input.GetKeyUp("e") && createWallActive == true)
        {
            createWallActive = false;
            CreateNodeWall();
            energyBar -= 33.33f;
        }
        else if (createWallActive == true && Vector2.Distance(placerNode.transform.position, this.transform.position) > maxWallDis) //wall gets too far away
        {
            createWallActive = false;
            CreateNodeWall();
            energyBar -= 33.33f;
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
        placerNode = Instantiate(nodePrefab, transform.position, transform.rotation);

    }

    void CreateNodeWall()
    {
        //Set position and rotation before creating the wall
        float wallDis = Vector2.Distance(placerNode.transform.position, this.transform.position);
        Vector3 wallPoint = (placerNode.transform.position - this.transform.position) / 2 + this.transform.position;

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

        Destroy(placerNode);
        Destroy(wallPredictObj);
        StartCoroutine(WallTimer(5, wall));
    }

    IEnumerator WallTimer(float waitTime, GameObject wall)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(wall);
    }

    void PredictWall(bool isFirst)
    {
        if (isFirst == true)
        {
            wallPredictObj = Instantiate(wallPremakePrefab);
        }
        float wallDis = Vector2.Distance(placerNode.transform.position, this.transform.position);
        Vector3 wallPoint = (placerNode.transform.position - this.transform.position) / 2 + this.transform.position;
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
