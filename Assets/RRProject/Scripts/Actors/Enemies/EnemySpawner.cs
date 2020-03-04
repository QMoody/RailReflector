using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<GameObject> _enemys;

    [SerializeField] private float _spawnRateMin;
    [SerializeField] private float _spawnRateMax;

    private float _spawnRate;
    private float _timeSinceSpawn;
    
    void Start()
    {
        SpawnEnemy();
    }
    
    void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
        if(_timeSinceSpawn >= _spawnRate)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int spawnLocation = Random.Range(0, _spawnPoints.Count);
        Instantiate(_enemys[Random.Range(0, _enemys.Count)], _spawnPoints[spawnLocation].position, Quaternion.identity);
        _timeSinceSpawn = 0;
        _spawnRate = Random.Range(_spawnRateMin, _spawnRateMax);
    }
}
