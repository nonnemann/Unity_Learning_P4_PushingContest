using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private float spawnRange = 9.0f;

    private  int _enemyCount = 0;
    private int _waveNumber = 1;

    private void Start()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private void Update()
    {
        _enemyCount = FindObjectsOfType<Enemy>().Length;
        if (_enemyCount == 0)
        {
            SpawnEnemyWave(_waveNumber);
            _waveNumber++;
        }
    }

    private void SpawnEnemyWave(int enemiesPerWave)
    {
        for (int enemyCounter = 0; enemyCounter < enemiesPerWave; enemyCounter++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnPositionX, 0, spawnPositionZ);
        return spawnPosition;
    }
}
