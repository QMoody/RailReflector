using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Transform _playerT;
    [SerializeField] private string HorizontalAxis;
    [SerializeField] private string VerticalAxis;
    //[SerializeField] private Transform _gunCannon;
    //public GameObject projPrefab;

    private Vector3 movement;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = _playerT.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        locomotion();
    }

    void locomotion()
    {
        movement = new Vector3(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis), 0);
        _rigidbody2D.MovePosition(transform.position + (movement * _speed * Time.deltaTime));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-19,19), Mathf.Clamp(transform.position.y, -13, 15), 0);
    }

    public void Respawn()
    {
        transform.position = _spawnPoint.position;
        StartCoroutine(SpawnBlink());
    }

    void Rotation()
    {
        int rot = 0;
        movement.Normalize();

        if(movement.y >= 0.5)
        {
            if (movement.x >= 0.5)
            {
                rot = -45;
            }
            else if (movement.x <= -0.5)
            {
                rot = 45;
            }
            else
            {
                rot = 0;
            }
        }
        else if(movement.y <= -0.5)
        {
            if (movement.x >= 0.5)
            {
                rot = 45;
            }
            else if (movement.x <= -0.5)
            {
                rot = -45;
            }
            //else
            //{
            //    rot = 180;
            //}
        }
        else
        {
            rot = 0;
        }

        _playerT.eulerAngles = new Vector3(0, 0, rot);
    }

    public void startInmortal()
    {
        _collider2D.enabled = false;
        StartCoroutine(Blink());
    }

    public void stopInmortal()
    {
        StopAllCoroutines();
        _collider2D.enabled = true;
        _spriteRenderer.enabled = true;
    }

    IEnumerator Blink()
    {
        int blinkCount = 0;
        _collider2D.enabled = false;

        while (true)
        {
            blinkCount++;
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator SpawnBlink()
    {
        int blinkCount = 0;
        _collider2D.enabled = false;

        while(blinkCount < 12)
        {
            blinkCount++;
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(0.25f);
        }

        _spriteRenderer.enabled = true;
        _collider2D.enabled = true;

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if(collision.collider.CompareTag("Enemy"))
        {
            Damageable dmg = GetComponent<Damageable>();
            if(dmg != null)
            {
                dmg.reciveDamage(1, "Bullet");
            }
        }
    }
}
