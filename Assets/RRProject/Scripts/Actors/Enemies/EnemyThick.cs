using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThick : EnemyBase
{
    [SerializeField] private GameObject _speeder;

    [Header("Shooting")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _gunPivot;
    [SerializeField] private float _shootRateMin;
    [SerializeField] private float _shootRateMax;

    private float _shootRate;
    private float _timeSinceShoot;

    private void Start()
    {
        _timeSinceShoot = 0;
        _shootRate = Random.Range(_shootRateMin, _shootRateMax);
    }

    void Update()
    {
        locomotion();

        _timeSinceShoot += Time.deltaTime;
        if (_timeSinceShoot >= _shootRate)
        {
            _timeSinceShoot = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        if (EnemyBulletManager.Instance.bullets.Count < EnemyBulletManager.Instance.maxBullets)
        {
            GameObject tmp = Instantiate(_bullet, _gunPivot.position, Quaternion.Euler(0, 0, 0));
            tmp.GetComponent<ReflectorProjectile>().refDir = -_gunPivot.up;
            EnemyBulletManager.Instance.addBullet(tmp);
        }
    }

    public void CreateSpeeder()
    {
        Instantiate(_speeder, transform.position, Quaternion.identity);
    }
}
