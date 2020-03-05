using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 5.0f;
    public float sideSpeed = 0.0f;
    public LayerMask layMask;

    private float _originalSpeed;

    private float _rayCheckTimer;
    private float _rayCheckRate = 0.05f;


    [Range(0,100)] public int dodgingChance = 25;
    public float _dodgeTime = 1;
    public float dodgeSpeed = 2.0f;

    [Header("Animation")]
    [SerializeField] private SpriteRenderer _sRenderer;
    [SerializeField] private List<Sprite> _walkSprites;
    [SerializeField] private List<Sprite> _damageSprites;
    [SerializeField] private List<Sprite> _deadSprites;
    [SerializeField] private float _timePerFrame;

    private List<Sprite> _currentSprites;
    private float _frameTime;
    private int _sIndex;
    private bool _dead = false;

    private bool _dodge = false;
    private bool _dodgeDirection = false;
    private float _dodgingTime = 0;

    public GameObject wallParticles;
    

    private Damageable damageable;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
        _originalSpeed = speed;
        _rayCheckTimer = Random.Range(0.0f, _rayCheckRate);
        _currentSprites = _walkSprites;
        _dead = false;
    }

    public void healthMultiplier(float multilier)
    {
        damageable.setHealth((int)((float)damageable._health * multilier));
    }

    void animate()
    {
        _frameTime += Time.deltaTime;
        
        if(_frameTime > _timePerFrame)
        {
            _frameTime = 0;
            _sIndex++;
            if(_sIndex >= _currentSprites.Count)
            {
                _sIndex = 0;
                if (_dead)
                {
                    _sIndex = _currentSprites.Count - 1;
                }
            }

            _sRenderer.sprite = _currentSprites[_sIndex];

            if(!_dead)
            {
                _currentSprites = _walkSprites;
            }
        }
    }

    public void locomotion()
    {
        if(_dead)
        {
            return;
        }

        if(_dodge)
        {
            _dodgingTime += Time.deltaTime;
            if(_dodgingTime >= _dodgeTime)
            {
                _dodge = false;
            }
        }

        transform.Translate(new Vector3((_dodge ? (_dodgeDirection ? -1 : 1)  * dodgeSpeed * Time.deltaTime : 0) + sideSpeed * Time.deltaTime, -speed * Time.deltaTime, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -19, 19), transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        animate();
        _rayCheckTimer += Time.deltaTime;
        if(_rayCheckTimer > _rayCheckRate)
        {
            checkforEnemiesInfront();
        }
    }

    void checkforEnemiesInfront()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 2, layMask);
        
        if (hit)
        {
            EnemyBase emy = hit.transform.GetComponent<EnemyBase>();
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
            if (speed < _originalSpeed)
            {
                speed = _originalSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            damageable.kill();
            Instantiate(wallParticles, transform.position, Quaternion.identity);
        }
    }

    public void Dodge()
    {
        if(_dodge)
        {
            return;
        }

        int rand = Random.Range(1, 100);

        if (rand <= dodgingChance)
        {
            _dodge = true;
            _dodgingTime = 0;

            rand = Random.Range(1, 100);
            if(rand <= 50)
            {
                _dodgeDirection = true;
            }
            else
            {
                _dodgeDirection = false;
            }
        }
    }

    public void damaged()
    {
        _currentSprites = _damageSprites;
    }

    public void dead()
    {
        Collider2D coll = GetComponent<Collider2D>();
        if(coll != null)
        {
            coll.enabled = false;
        }

        _frameTime = 0;
        _timePerFrame = 0.25f;
        _sIndex = 0;
        _currentSprites = _deadSprites;
        _dead = true;
        if(ScoreTracker.Instance != null)
            ScoreTracker.Instance.SpawnScore(transform.position);
    }
}
