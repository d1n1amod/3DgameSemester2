using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [Tooltip("Drag Pig prefab here (the enemy model from Blender)")]
    public GameObject pigPrefab;   


    [Header("Spawn Settings")]
    public float spawnDelay = 0f;    
    public float spawnRadius = 15f;  

    private TimerScript timerScript;
    private bool hasSpawned = false;

    void Start()
    {
        timerScript = FindObjectOfType<TimerScript>();
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        
        while (timerScript != null && !timerScript.StartTimer)
        {
            yield return null;
        }

        
        yield return new WaitForSeconds(spawnDelay);

       
        if (!hasSpawned && pigPrefab != null)
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 randomSpawnPosition = new Vector3(
                transform.position.x + randomCircle.x,
                5, 
                transform.position.z + randomCircle.y
            );

            GameObject spawnedPig = Instantiate(pigPrefab, randomSpawnPosition, Quaternion.identity);

            Animator pigAnimator = spawnedPig.GetComponent<Animator>();
            if (pigAnimator != null)
            {
                pigAnimator.SetBool("isMoving", true);
            }

            hasSpawned = true; 
        }
    }
}

