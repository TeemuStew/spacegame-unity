using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Enemy prefabs
    public GameObject redEnemyPrefab;
    public GameObject yellowEnemyPrefab;
    public GameObject asteroidEnemyPrefab;

    // Spawn variables
    public Transform spawnPoint;
    public float spawnInterval = 2f; 

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Randomly choose an enemy type
            int enemyType = Random.Range(0, 3);

            GameObject enemyToSpawn = null;

            switch (enemyType)
            {
                case 0:
                    enemyToSpawn = redEnemyPrefab;
                    break;
                case 1:
                    enemyToSpawn = yellowEnemyPrefab;
                    break;
                case 2:
                    enemyToSpawn = asteroidEnemyPrefab;
                    break;
            }

            // Random spawn position
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, Random.Range(-4f, 4f), 0);

            // Spawn enemy
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
