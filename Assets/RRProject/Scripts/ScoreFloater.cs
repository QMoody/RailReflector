using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFloater : MonoBehaviour
{
    public ScoreTracker scoreTrack;
    public Vector3 scoreGoal;
    public int scoreValue;
    private float colourValue;

    public Color c1;
    public Color c2;

    void Update()
    {
        MoveToScore();
        FlashColour();
    }

    void MoveToScore()
    {
        float step = Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, scoreGoal, step);

        if (transform.position.x <= scoreGoal.x + 0.5f && transform.position.x >= scoreGoal.x - 0.5f && transform.position.y <= scoreGoal.y + 0.5f && transform.position.y >= scoreGoal.y - 0.5f)
        {
            scoreTrack.AddScore(scoreValue);
            Destroy(this.gameObject);
        }
    }

    void FlashColour()
    {
        colourValue = Mathf.PingPong(Time.time * 10, 1);

        if (colourValue <= 0.1f)
            GetComponent<Text>().color = c1;
        else if (colourValue >= 0.9f)
            GetComponent<Text>().color = c2;
    }
}
