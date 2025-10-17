using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    float spawnRange = 9f;
    public bool isGameOver = false;
    void Start()
    {
        NextWave();
    }

    void SpawnEnemyWave(int enemisToSpawn)
    {
        for (int i = 0; i < enemisToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(),
                enemyPrefab.transform.rotation);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            NextWave();
        }
    }
    
    void NextWave()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab,
            GenerateSpawnPosition(),
            powerupPrefab.transform.rotation);
    }
}
