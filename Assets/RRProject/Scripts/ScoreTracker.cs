using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;

    public GameObject canvas;
    public GameObject scoreText;
    public GameObject scorePrefab;
    public GameObject scoreGoal;

    public int scoreCount;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnScore(Vector3 scoreSpawnLoc)
    {
        GameObject scoreTmp = Instantiate(scorePrefab, scoreSpawnLoc, Quaternion.Euler(0, 0, 0));
        scoreTmp.transform.SetParent(canvas.transform);
        scoreTmp.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);

        ScoreFloater scoreSctipt = scoreTmp.GetComponent<ScoreFloater>();
        scoreSctipt.scoreTrack = this.GetComponent<ScoreTracker>();
        scoreSctipt.scoreGoal = scoreGoal.transform.position;
        scoreSctipt.scoreValue = 20;

        scoreTmp.GetComponent<Text>().text = scoreSctipt.scoreValue.ToString();
    }

    public void AddScore(int scoreValue)
    {
        scoreCount += scoreValue;
        scoreText.GetComponent<Text>().text = scoreCount.ToString();
    }
}
