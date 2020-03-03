using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5.0f;
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

    void locomotion()
    {
        transform.Translate(new Vector3(0, -1 * _speed * Time.deltaTime, 0));
    }

    void Shoot()
    {
        GameObject tmp = Instantiate(_bullet, _gunPivot.position, Quaternion.Euler(0, 0, 0));
        tmp.GetComponent<ReflectorProjectile>().refDir = -_gunPivot.up;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
