using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;         // Enemy prefab
    public List<Transform> spawnPoints;    // Positions where enemies will appear
    public float dropHeight = 15f;         // How high above spawn point they drop from

    [Header("References")]
    public TimerScript timerScript;        // Reference to your timer script

    private bool enemiesSpawned = false;

    private void Update()
    {
        // When timer starts and enemies not yet spawned
        if (timerScript.StartTimer && !enemiesSpawned)
        {
            SpawnEnemies();
            enemiesSpawned = true;
        }
    }

    private void SpawnEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            Vector3 spawnPos = point.position + Vector3.up * dropHeight;
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Add gravity effect (so they fall down naturally)
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = enemy.AddComponent<Rigidbody>();
            }

            rb.useGravity = true;
        }
    }
}