using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFloater : MonoBehaviour
{
    public ScoreTracker scoreTrack;
    public Vector3 scoreGoal;
    public int scoreValue;

    void Update()
    {
        MoveToScore();
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
}
