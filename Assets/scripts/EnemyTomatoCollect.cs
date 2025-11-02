using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;



public class EnemyTomatoCollect : MonoBehaviour
{
    [Header("Tomato Collection")]
    public float tomatoDetectionRadius = 10f;      // How far the pig can see tomatoes
    public float eatDistance = 2f;                // Distance to tomato to start eating
    public float eatingDuration = 4f;             // How long the pig eats before moving again
    public LayerMask tomatoLayer;                  // Layer mask for tomato objects

    private NavMeshAgent agent;
    private EnemyMovement enemyMovement;
    private Animator animator;

    private Transform targetTomato = null;
    private bool isEating = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();

        if (enemyMovement == null)
            Debug.LogWarning($"{name} is missing EnemyMovement script!");
        if (animator == null)
            Debug.LogWarning($"{name} is missing Animator component!");
    }

    private void Update()
    {
        if (isEating) return; // Don't do anything while eating

        // Find tomato if not currently targeting one
        if (targetTomato == null)
        {
            FindClosestTomato();
        }

        // If a tomato was found, go to it
        if (targetTomato != null)
        {
            float distance = Vector3.Distance(transform.position, targetTomato.position);
            agent.SetDestination(targetTomato.position);

            // Stop and eat when close enough
            if (distance <= eatDistance)
            {
                StartCoroutine(EatTomatoRoutine(targetTomato.gameObject));
            }

            // Disable wandering while going toward tomato
            if (enemyMovement != null && enemyMovement.enabled)
                enemyMovement.enabled = false;
        }
        else
        {
            // No tomato nearby ? enable wandering again
            if (enemyMovement != null && !enemyMovement.enabled && !isEating)
                enemyMovement.enabled = true;
        }
    }

    void FindClosestTomato()
    {
        Collider[] tomatoes = Physics.OverlapSphere(transform.position, tomatoDetectionRadius, tomatoLayer);
        float closestDistance = Mathf.Infinity;
        Transform closestTomato = null;

        foreach (Collider col in tomatoes)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTomato = col.transform;
            }
        }

        targetTomato = closestTomato;
    }

    IEnumerator EatTomatoRoutine(GameObject tomato)
    {
        isEating = true;
        targetTomato = null;
        agent.isStopped = true;

        // Play the eating animation
        if (animator != null)
            animator.SetTrigger("Eat");

        // Optional: make the pig face the tomato
        Vector3 lookPos = tomato.transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        // Wait while “eating”
        yield return new WaitForSeconds(eatingDuration);

        // Resume normal wandering
        agent.isStopped = false;
        isEating = false;

        // Optionally: if you want the tomato to disappear after eating
        // Destroy(tomato);

        if (enemyMovement != null)
            enemyMovement.enabled = true;
    }
}
