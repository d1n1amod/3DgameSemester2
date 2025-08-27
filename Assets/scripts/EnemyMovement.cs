using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Player Detection")]
    public Transform target;               
    public float detectionRadius = 20f;    
    public float runDistance = 15f;        

    [Header("Wandering")]
    public float wanderRadius = 20f;       
    public float wanderInterval = 3f;     

    private NavMeshAgent agent;
    private Coroutine wanderRoutine;
    private bool isRunningAway = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Start wandering by default
        wanderRoutine = StartCoroutine(Wander());
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRadius && !isRunningAway)
        {
           
            if (wanderRoutine != null)
            {
                StopCoroutine(wanderRoutine);
                wanderRoutine = null;
            }

            StartCoroutine(RunAwayAndResume());
        }
    }

    private IEnumerator RunAwayAndResume()
    {
        isRunningAway = true;

        Vector3 dirToPlayer = transform.position - target.position; 
        Vector3 runPosition = transform.position + dirToPlayer.normalized * runDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(runPosition, out hit, runDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        // Wait until enemy reaches the run position (or almost there)
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        
        isRunningAway = false;
        if (wanderRoutine == null)
        {
            wanderRoutine = StartCoroutine(Wander());
        }
    }

    private IEnumerator Wander()
    {
        WaitForSeconds wait = new WaitForSeconds(wanderInterval);

        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

            yield return wait;
        }
    }
}