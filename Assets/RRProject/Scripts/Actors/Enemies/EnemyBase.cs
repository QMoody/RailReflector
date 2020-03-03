using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 5.0f;
    public float dodgeSpeed = 2.0f;

    private float _originalSpeed;

    private float _rayCheckTimer;
    private float _rayCheckRate = 0.65f;

    private bool _dodge = false;
    private float _dodgingTime = 0;
    private float _dodgeTime = 1;

    private void Awake()
    {
        _originalSpeed = speed;
        _rayCheckTimer = Random.Range(0.0f, 1.0f);
    }

    public void locomotion()
    {
        if(_dodge)
        {
            _dodgingTime += Time.deltaTime;
            if(_dodgingTime >= _dodgeTime)
            {
                _dodge = false;
            }
        }

        transform.Translate(new Vector3(_dodge ? dodgeSpeed * Time.deltaTime: 0, -speed * Time.deltaTime, 0));
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

    public void Dodge()
    {
        _dodge = true;
        _dodgingTime = 0;
    }
}
