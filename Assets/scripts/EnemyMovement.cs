using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Player Detection")]
    [Tooltip("Assign the Player object here, or leave empty to auto-find by tag.")]
    public Transform target;               
    public float detectionRadius = 30f;    
    public float runDistance = 20f;        

    [Header("Wandering")]
    public float wanderRadius = 20f;       
    public float wanderInterval = 3f;     

    private NavMeshAgent agent;
    private Coroutine wanderRoutine;
    private bool isRunningAway = false;

    private Animator animator;

    private void Awake()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogWarning($"{name}: EnemyMovement has no Player target assigned and no GameObject with tag 'Player' found!");
            }
        }

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
       
        wanderRoutine = StartCoroutine(Wander());
    }

    private void Update()
    {
        if (target == null) return;
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

        if (animator != null)
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);

            // DEBUG: Add these lines temporarily
            Debug.Log("Velocity: " + agent.velocity.magnitude + ", isMoving: " + isMoving);
            Debug.Log("Animator isPlaying: " + animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"));
        }
        else
        {
            Debug.LogError("No Animator component found!");
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