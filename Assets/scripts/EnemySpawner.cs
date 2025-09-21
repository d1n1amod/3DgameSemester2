using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [Tooltip("Drag the enemy capsule prefab here")]
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float spawnDelay = 0f;    // delay before this spawner spawns
    public float spawnRadius = 15f;  // random spawn radius around the spawner

    private TimerScript timerScript;
    private bool hasSpawned = false;

    void Start()
    {
        timerScript = FindObjectOfType<TimerScript>();
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        // Wait until timer starts
        while (timerScript != null && !timerScript.StartTimer)
        {
            yield return null;
        }

        // Wait for this spawner's individual delay
        yield return new WaitForSeconds(spawnDelay);

        // Spawn the enemy if not already spawned
        if (!hasSpawned && enemyPrefab != null)
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 randomSpawnPosition = new Vector3(
                transform.position.x + randomCircle.x,
                5, // Y position
                transform.position.z + randomCircle.y
            );

            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
            hasSpawned = true; // ensure it only spawns once
        }
    }
}

