using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 5.0f;

    private float _originalSpeed;

    private float _rayCheckTimer;
    private float _rayCheckRate = 0.65f;

    private void Awake()
    {
        _originalSpeed = speed;
        _rayCheckTimer = Random.Range(0.0f, 1.0f);
    }

    public void locomotion()
    {
        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
    }

    private void LateUpdate()
    {
        _rayCheckTimer += Time.deltaTime;
        if(_rayCheckTimer > _rayCheckRate)
        {
            checkforEnemiesInfront();
        }
    }

    void checkforEnemiesInfront()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 2);

        if (hit.collider != null)
        {
            EnemyBase emy = hit.collider.GetComponent<EnemyThick>();
            if (emy != null)
            {
                if (emy.speed < speed)
                {
                    speed = emy.speed;
                }
            }
        }
        else
        {
            if(speed < _originalSpeed)
            {
                speed = _originalSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
