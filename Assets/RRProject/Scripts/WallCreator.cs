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
    int nodeCount;
    bool isFirstNode = true;
    bool nodeTimerRunning;
    bool wallTimerRunning;
    bool createWallActive;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        energyBar = 200;
        nodeArray = new GameObject[20];
        wallArray = new GameObject[20];
    }

    void Update()
    {
        WallActive();
        if (createWallActive == true && nodeTimerRunning == false)
            lastCor = StartCoroutine(NodeTimer(1.5f));
    }

    void WallActive()
    {
        if (Input.GetKeyDown("e") && createWallActive == false) //Change this to a get button down later
            createWallActive = true;
        else if(Input.GetKeyDown("e") && createWallActive == true)
        {
            createWallActive = false;
            StopCoroutine(lastCor);
            StartCoroutine(WallTimer(7.0f));
        }

        if (createWallActive == true)
            energyBar += -0.25f;
        else if (createWallActive == false && energyBar < 200)
            energyBar += 0.125f;

        Debug.Log("hit");

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
        for(int i = 0; i < nodeArray.Length; i++)
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
