using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab; 
    public int waveNumber = 1;
    public int wavesForBoss = 3; // Number of waves before spawning a boss

    private float spawnRange = 9;
    public GameObject powerupPrefab;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

            // Check if it's time to spawn a boss
            if (waveNumber % wavesForBoss == 0)
            {
                // SpawnBossWave(); // Commented out to spawn boss on button press
            }
        }

        // Check for button press to manually spawn boss 
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        {
            SpawnBossWave();
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnBossWave()
    {
        // Spawn the boss
        Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);

        // Spawn 2 extra enemies
        for (int i = 0; i < 2; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}