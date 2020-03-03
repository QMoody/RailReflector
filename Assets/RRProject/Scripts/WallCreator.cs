using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject nodePrefab;
    private GameObject[] nodeArray;
    private GameObject[] wallArray;
    Coroutine lastCor;

    public float energyBar;
    public int nodeCount;
    bool isFirstNode = true;
    bool nodeTimerRunning;
    bool wallTimerRunning;
    bool createWallActive;
    bool startNewWall = true;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        energyBar = 100;
        nodeArray = new GameObject[30];
        wallArray = new GameObject[30];
    }

    void Update()
    {
        WallActive();
        if (createWallActive == true && nodeTimerRunning == false && isFirstNode == true)
            CreateNodeWalls();
        else if (createWallActive == true && nodeTimerRunning == false)
            lastCor = StartCoroutine(NodeTimer(0.5f));
    }

    void WallActive()
    {
        if (Input.GetKeyDown("e") && createWallActive == false && startNewWall == true) //Change this to a get button down later
        {
            createWallActive = true;
            startNewWall = false;
        }
        else if(Input.GetKeyDown("e") && createWallActive == true)
        {
            createWallActive = false;
            StopCoroutine(lastCor);
            StartCoroutine(WallTimer(7.0f));
        }

        if (createWallActive == true)
            energyBar += -0.25f;
        else if (createWallActive == false && energyBar < 100)
            energyBar += 0.125f;

        if (energyBar <= 0)
        {
            createWallActive = false;
            StartCoroutine(WallTimer(7.0f));
        }
    }

    void CreateNodeWalls()
    {
        GameObject node = Instantiate(nodePrefab, transform.position, transform.rotation);
        nodeArray[nodeCount] = node;

        if (isFirstNode == false)
        {
            //Set pos a rot of the walls
            float wallDis = Vector2.Distance(nodeArray[nodeCount].transform.position, nodeArray[nodeCount - 1].transform.position);
            Vector3 wallPoint = (nodeArray[nodeCount].transform.position - nodeArray[nodeCount - 1].transform.position) / 2 + nodeArray[nodeCount - 1].transform.position;
            Vector3 rotDir = wallPoint - nodeArray[nodeCount].transform.position;
            float wallAngle = Mathf.Atan2(rotDir.y, rotDir.x) * Mathf.Rad2Deg;
            Quaternion wallRot = Quaternion.AngleAxis(wallAngle, Vector3.forward);

            GameObject wall = Instantiate(wallPrefab, wallPoint, wallRot);
            wall.transform.localScale = new Vector2(wallDis, wall.transform.localScale.y);
            wallArray[nodeCount - 1] = wall;
        }

        nodeCount += 1;
        isFirstNode = false;
    }

    void DestoryNodeWalls()
    {
        nodeCount = 0;
        nodeTimerRunning = false;
        isFirstNode = true;

        for (int i = 0; i < nodeArray.Length; i++)
            if (nodeArray[i] != null)
            {
                Destroy(nodeArray[i]);
                nodeArray[i] = null;
            }

        for (int i = 0; i < wallArray.Length; i++)
            if (wallArray[i] != null)
            {
                Destroy(wallArray[i]);
                wallArray[i] = null;
            }

        startNewWall = true;
    }

    IEnumerator NodeTimer(float waitTime)
    {
        nodeTimerRunning = true;
        yield return new WaitForSeconds(waitTime);
        CreateNodeWalls();
        nodeTimerRunning = false;
    }

    IEnumerator WallTimer(float waitTime)
    {
        if (wallTimerRunning == false)
        {
            wallTimerRunning = true;
            yield return new WaitForSeconds(waitTime);
            DestoryNodeWalls();
            wallTimerRunning = false;
        }
    }
}
