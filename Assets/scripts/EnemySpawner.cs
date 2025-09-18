using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] capsulePrefabs; 
    public float spawnInterval = 2f;
    public Vector2 spawnRangeX = new Vector2(-10, 10);
    public Vector2 spawnRangeZ = new Vector2(-10, 10);
    public float spawnY = 5f;
    public int maxEnemies = 4;

    private int enemiesSpawned = 0;
    private Transform player;
    private TimerScript timerScript; 

    void Start()
    {
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        
        timerScript = FindObjectOfType<TimerScript>();

       
        StartCoroutine(WaitForTimerAndSpawn());
    }

    IEnumerator WaitForTimerAndSpawn()
    {
        
        while (timerScript != null && !timerScript.StartTimer)
        {
            yield return null; 
        }

        yield return StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < maxEnemies)
        {
            int randomIndex = Random.Range(0, capsulePrefabs.Length);

            Vector3 randomSpawnPosition = new Vector3(
                Random.Range(spawnRangeX.x, spawnRangeX.y),
                spawnY,
                Random.Range(spawnRangeZ.x, spawnRangeZ.y)
            );

            GameObject enemy = Instantiate(capsulePrefabs[randomIndex], randomSpawnPosition, Quaternion.identity);

          
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            if (movement != null && player != null)
            {
                movement.target = player;
            }

            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}