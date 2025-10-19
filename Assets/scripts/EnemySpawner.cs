using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Header("Enemy Prefab")]
    [Tooltip("Drag Pig prefab here (the enemy model from Blender)")]
    public GameObject pigPrefab;

    [Header("Spawn Settings")]
    public float spawnDelayBetweenEnemies = 5f; // 5 seconds between each spawn
    public float spawnRadius = 15f;
    public int numberOfEnemies = 4; // total enemies to spawn

    private TimerScript timerScript;
    private int aliveEnemies = 0;
    private int spawnedCount = 0; // track how many have been spawned
    private bool allSpawned = false; // track if all enemies have spawned

    void Start()
    {
        timerScript = FindObjectOfType<TimerScript>();
        StartCoroutine(WaitAndSpawnAll());
    }

    IEnumerator WaitAndSpawnAll()
    {
        // Wait until timer starts
        while (timerScript != null && !timerScript.StartTimer)
            yield return null;

        // Spawn each enemy with a delay, up to numberOfEnemies
        while (spawnedCount < numberOfEnemies)
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 randomSpawnPosition = new Vector3(
                transform.position.x + randomCircle.x,
                5,
                transform.position.z + randomCircle.y
            );

            GameObject spawnedPig = Instantiate(pigPrefab, randomSpawnPosition, Quaternion.identity);

            // Register death callback
            EnemyHealth pigHealth = spawnedPig.GetComponent<EnemyHealth>();
            if (pigHealth != null)
                pigHealth.OnEnemyDied += OnEnemyDeath;

            aliveEnemies++;
            spawnedCount++;

            // Wait before spawning the next enemy
            yield return new WaitForSeconds(spawnDelayBetweenEnemies);
        }

        allSpawned = true; // mark that all enemies have spawned
    }

    void OnEnemyDeath()
    {
        aliveEnemies--;

        // Trigger game win only if all enemies spawned AND all are dead
        if (allSpawned && aliveEnemies <= 0)
        {
            TimerScript timer = FindObjectOfType<TimerScript>();
            if (timer != null)
                timer.TriggerGameWin();
        }
    }
}