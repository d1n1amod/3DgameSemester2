using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [Tooltip("Drag Pig prefab here (the enemy model from Blender)")]
    public GameObject pigPrefab;

    [Header("Spawn Settings")]
    public float spawnDelayBetweenEnemies = 6f;
    public int numberOfEnemies = 6;

    [Header("Spawn Locations")]
    [Tooltip("Fixed spawn points — each pig will spawn at a unique one")]
    public Transform[] spawnPoints;

    [Tooltip("Radius used if no spawn points are given")]
    public float spawnRadius = 15f;

    private TimerScript timerScript;
    private AudioSource _audioSource;
    private int aliveEnemies = 0;
    private int spawnedCount = 0;
    private bool allSpawned = false;

    void Start()
    {
        timerScript = FindObjectOfType<TimerScript>();
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitAndSpawnAll());
    }

    IEnumerator WaitAndSpawnAll()
    {
        while (timerScript != null && !timerScript.StartTimer)
            yield return null;

        // Shuffle the spawn points so pigs spawn in random order
        List<Transform> shuffledPoints = new List<Transform>(spawnPoints);
        for (int i = 0; i < shuffledPoints.Count; i++)
        {
            Transform temp = shuffledPoints[i];
            int randomIndex = Random.Range(i, shuffledPoints.Count);
            shuffledPoints[i] = shuffledPoints[randomIndex];
            shuffledPoints[randomIndex] = temp;
        }

        // Spawn up to numberOfEnemies or number of spawn points (whichever is smaller)
        int spawnCount = Mathf.Min(numberOfEnemies, shuffledPoints.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = shuffledPoints[i].position;

            GameObject spawnedPig = Instantiate(pigPrefab, spawnPosition, Quaternion.identity);
            _audioSource.Play();

            EnemyHealth pigHealth = spawnedPig.GetComponent<EnemyHealth>();
            if (pigHealth != null)
                pigHealth.OnEnemyDied += OnEnemyDeath;

            aliveEnemies++;
            spawnedCount++;

            yield return new WaitForSeconds(spawnDelayBetweenEnemies);
        }

        // If there are more enemies than spawn points, spawn remaining ones randomly in the radius
        while (spawnedCount < numberOfEnemies)
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 randomSpawnPosition = new Vector3(
                transform.position.x + randomCircle.x,
                5,
                transform.position.z + randomCircle.y
            );

            GameObject spawnedPig = Instantiate(pigPrefab, randomSpawnPosition, Quaternion.identity);
            _audioSource.Play();

            EnemyHealth pigHealth = spawnedPig.GetComponent<EnemyHealth>();
            if (pigHealth != null)
                pigHealth.OnEnemyDied += OnEnemyDeath;

            aliveEnemies++;
            spawnedCount++;

            yield return new WaitForSeconds(spawnDelayBetweenEnemies);
        }

        allSpawned = true;
    }

    void OnEnemyDeath()
    {
        aliveEnemies--;

        if (allSpawned && aliveEnemies <= 0)
        {
            TimerScript timer = FindObjectOfType<TimerScript>();
            if (timer != null)
                timer.TriggerGameWin();
            _audioSource.Stop();
        }
    }
}